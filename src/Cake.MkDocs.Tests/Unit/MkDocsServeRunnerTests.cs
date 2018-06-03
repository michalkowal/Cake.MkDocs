using System;
using System.Threading;
using System.Threading.Tasks;
using Cake.Core;
using Cake.MkDocs.Serve;
using Cake.MkDocs.Tests.Fixtures;
using Cake.MkDocs.Tests.Fixtures.Serve;
using Xunit;

namespace Cake.MkDocs.Tests.Unit
{
    public sealed class MkDocsServeRunnerTests
    {
        public abstract class BaseMkDocsServeTests<TFixture>
            : MkDocsToolTests<TFixture, MkDocsServeSettings>
            where TFixture : MkDocsFixture<MkDocsServeSettings>, new()
        {
            [Fact]
            public void Should_Add_Serve_Command()
            {
                // Given
                var fixture = new TFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("serve", result.Args);
            }

            [Theory]
            [InlineData("./project/mkdocs.yml", "/Working/project/mkdocs.yml")]
            [InlineData("/project the second/mkdocs.yml", "/project the second/mkdocs.yml")]
            public void Should_Add_Config_File_Argument_If_Defined(string configFile, string expected)
            {
                // Given
                var fixture = new TFixture();
                fixture.Settings.ConfigFile = configFile;

                // When
                var result = fixture.Run();

                // Then
                Assert.Contains($"--config-file \"{expected}\"", result.Args);
            }

            [Fact]
            public void Should_Not_Add_Config_File_Argument_If_Not_Defined()
            {
                // Given
                var fixture = new TFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.DoesNotContain("--config-file", result.Args);
            }

            [Theory]
            [InlineData("127.0.0.1", 8000, "127.0.0.1:8000")]
            [InlineData("localhost", 8080, "localhost:8080")]
            public void Should_Add_Dev_Addr_Argument_If_Defined(string ip, int port, string expected)
            {
                // Given
                var fixture = new TFixture();
                fixture.Settings.DevAddr = new MkDocsAddress(ip, port);

                // When
                var result = fixture.Run();

                // Then
                Assert.Contains($"--dev-addr {expected}", result.Args);
            }

            [Fact]
            public void Should_Not_Add_Dev_Addr_Argument_If_Not_Defined()
            {
                // Given
                var fixture = new TFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.DoesNotContain("--dev-addr", result.Args);
            }

            [Fact]
            public void Should_Add_Strict_Argument_If_Defined()
            {
                // Given
                var fixture = new TFixture();
                fixture.Settings.Strict = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Contains("--strict", result.Args);
            }

            [Fact]
            public void Should_Not_Add_Strict_Argument_If_Not_Defined()
            {
                // Given
                var fixture = new TFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.DoesNotContain("--strict", result.Args);
            }

            [Theory]
            [InlineData(MkDocsTheme.MkDocs, "mkdocs")]
            [InlineData(MkDocsTheme.ReadTheDocs, "readthedocs")]
            public void Should_Add_Theme_Argument_If_Defined(MkDocsTheme theme, string expected)
            {
                // Given
                var fixture = new TFixture();
                fixture.Settings.Theme = theme;

                // When
                var result = fixture.Run();

                // Then
                Assert.Contains($"--theme {expected}", result.Args);
            }

            [Fact]
            public void Should_Not_Add_Theme_Argument_If_Not_Defined()
            {
                // Given
                var fixture = new TFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.DoesNotContain("--theme", result.Args);
            }

            [Theory]
            [InlineData("./project/theme", "/Working/project/theme")]
            [InlineData("/project the second/theme", "/project the second/theme")]
            public void Should_Add_Theme_Dir_Argument_If_Defined(string themeDir, string expected)
            {
                // Given
                var fixture = new TFixture();
                fixture.Settings.ThemeDir = themeDir;

                // When
                var result = fixture.Run();

                // Then
                Assert.Contains($"--theme-dir \"{expected}\"", result.Args);
            }

            [Fact]
            public void Should_Not_Add_Theme_Dir_Argument_If_Not_Defined()
            {
                // Given
                var fixture = new TFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.DoesNotContain("--theme-dir", result.Args);
            }

            [Fact]
            public void Should_Add_Live_Reload_Argument_If_Defined()
            {
                // Given
                var fixture = new TFixture();
                fixture.Settings.LiveReload = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Contains("--livereload", result.Args);
            }

            [Fact]
            public void Should_Not_Add_Live_Reload_Argument_If_Not_Defined()
            {
                // Given
                var fixture = new TFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.DoesNotContain("--livereload", result.Args);
            }

            [Fact]
            public void Should_Add_No_Live_Reload_Argument_If_Defined()
            {
                // Given
                var fixture = new TFixture();
                fixture.Settings.NoLiveReload = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Contains("--no-livereload", result.Args);
            }

            [Fact]
            public void Should_Not_Add_No_Live_Reload_Argument_If_Not_Defined()
            {
                // Given
                var fixture = new TFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.DoesNotContain("--no-livereload", result.Args);
            }

            [Fact]
            public void Should_Add_Dirty_Reload_Argument_If_Defined()
            {
                // Given
                var fixture = new TFixture();
                fixture.Settings.DirtyReload = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Contains("--dirtyreload", result.Args);
            }

            [Fact]
            public void Should_Not_Add_Dirty_Reload_Argument_If_Not_Defined()
            {
                // Given
                var fixture = new TFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.DoesNotContain("--dirtyreload", result.Args);
            }
        }

        public sealed class TheServeInWorkingDirMethod
            : BaseMkDocsServeTests<MkDocsServeRunnerWorkingDirFixture>
        {
            [Theory]
            [InlineData("/Working")]
            public void Should_Not_Change_Process_Working_Directory(string expected)
            {
                // Given
                var fixture = new MkDocsServeRunnerWorkingDirFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal(expected, result.Process.WorkingDirectory.FullPath);
            }
        }

        public sealed class TheServeMethod
            : BaseMkDocsServeTests<MkDocsServeRunnerWorkingDirFixture>
        {
            [Theory]
            [InlineData("./project", "/Working/project")]
            [InlineData("/project the second/", "/project the second")]
            public void Should_Change_Process_Working_Directory(string dir, string expected)
            {
                // Given
                var fixture = new MkDocsServeRunnerFixture();
                fixture.GivenProjectDirectory(dir);

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal(expected, result.Process.WorkingDirectory.FullPath);
            }

            [Fact]
            public void Should_Throw_For_Empty_Project_Dir()
            {
                // Given
                var fixture = new MkDocsServeRunnerFixture();
                fixture.GivenProjectDirectory(null);

                // When
                var result = Record.Exception(() => fixture.Run());

                // Then
                Assert.IsType<ArgumentNullException>(result);
                Assert.Equal("projectDirectory", ((ArgumentNullException)result).ParamName);
            }
        }

        public abstract class BaseMkDocsServeAsyncTests<TFixture>
            where TFixture : BaseMkDocsServeAsyncFixture, new()
        {
            [Fact]
            public async void Should_Throw_If_Settings_Are_Null()
            {
                // Given
                var fixture = new TFixture();
                fixture.Settings = null;

                // When
                var result = await (Record.ExceptionAsync(() => fixture.Run()) ?? Task.FromResult<Exception>(null));

                // Then
                Assert.IsType<ArgumentNullException>(result);
                Assert.Equal("settings", ((ArgumentNullException)result).ParamName);
            }

            [Fact]
            public async void Should_Throw_If_MkDocs_Executable_Was_Not_Found()
            {
                // Given
                var fixture = new TFixture();
                fixture.GivenDefaultToolDoNotExist();

                // When
                var result = await (Record.ExceptionAsync(() => fixture.Run()) ?? Task.FromResult<Exception>(null));

                // Then
                Assert.IsType<CakeException>(result);
                Assert.Equal("MkDocs: Could not locate executable.", ((CakeException)result).Message);
            }

            [Theory]
            [InlineData("/bin/python/Scripts/mkdocs.exe", "/bin/python/Scripts/mkdocs.exe")]
            [InlineData("./tools/python/Scripts/mkdocs.exe", "/Working/tools/python/Scripts/mkdocs.exe")]
            public async void Should_Use_MkDocs_Executable_From_Tool_Path_If_Provided(string toolPath, string expected)
            {
                // Given
                var fixture = new TFixture();
                fixture.Settings.ToolPath = toolPath;
                fixture.GivenSettingsToolPathExist();

                // When
                var result = await fixture.Run();

                // Then
                Assert.Equal(expected, result.Path.FullPath);
            }

            [Fact]
            public async void Should_Find_MkDocs_Executable_If_Tool_Path_Not_Provided()
            {
                // Given
                var fixture = new TFixture();

                // When
                var result = await fixture.Run();

                // Then
                Assert.Equal("/Working/tools/mkdocs", result.Path.FullPath);
            }

            [Theory]
            [InlineData("/python/Scripts", "/python/Scripts/mkdocs.exe")]
            public async void Should_Find_MkDocs_Executable_From_Environment_Path_If_Tool_Path_Not_Provided(string environmentPath, string expected)
            {
                // Given
                var fixture = new TFixture();
                fixture.GivenDefaultToolDoNotExist();
                fixture.GivenEnvironmentPath(environmentPath);

                // When
                var result = await fixture.Run();

                // Then
                Assert.Equal(expected, result.Path.FullPath);
            }

            [Theory]
            [InlineData("/python/Scripts", "/python/Scripts/mkdocs.exe")]
            public async void Should_Find_MkDocs_Executable_From_Registry_If_Tool_Path_Not_Provided(string environmentPath, string expected)
            {
                // Given
                var fixture = new TFixture();
                fixture.GivenDefaultToolDoNotExist();
                fixture.GivenRegistryFile(environmentPath);

                // When
                var result = await fixture.Run();

                // Then
                Assert.Equal(expected, result.Path.FullPath);
            }

            [Fact]
            public async void Should_Throw_If_Process_Was_Not_Started()
            {
                // Given
                var fixture = new TFixture();
                fixture.GivenProcessCannotStart();

                // When
                var result = await (Record.ExceptionAsync(() => fixture.Run()) ?? Task.FromResult<Exception>(null));

                // Then
                Assert.IsType<CakeException>(result);
                Assert.Equal("MkDocs: Process was not started.", ((CakeException)result).Message);
            }

            [Fact]
            public async void Should_Throw_If_Process_Has_A_Non_Zero_Exit_Code()
            {
                // Given
                var fixture = new TFixture();
                fixture.GivenProcessExitsWithCode(1);

                // When
                var result = await (Record.ExceptionAsync(() => fixture.Run()) ?? Task.FromResult<Exception>(null));

                // Then
                Assert.IsType<CakeException>(result);
                Assert.Equal("MkDocs: Process returned an error (exit code 1).", ((CakeException)result).Message);
            }

            [Fact]
            public async void Should_Add_Quiet_Argument_If_Defined()
            {
                // Given
                var fixture = new TFixture();
                fixture.Settings.Quiet = true;

                // When
                var result = await fixture.Run();

                // Then
                Assert.Contains("--quiet", result.Args);
            }

            [Fact]
            public async void Should_Not_Add_Quiet_Argument_If_Not_Defined()
            {
                // Given
                var fixture = new TFixture();

                // When
                var result = await fixture.Run();

                // Then
                Assert.DoesNotContain("--quiet", result.Args);
            }

            [Fact]
            public async void Should_Add_Verbose_Argument_If_Defined()
            {
                // Given
                var fixture = new TFixture();
                fixture.Settings.Verbose = true;

                // When
                var result = await fixture.Run();

                // Then
                Assert.Contains("--verbose", result.Args);
            }

            [Fact]
            public async void Should_Not_Add_Verbose_Argument_If_Not_Defined()
            {
                // Given
                var fixture = new TFixture();

                // When
                var result = await fixture.Run();

                // Then
                Assert.DoesNotContain("--verbose", result.Args);
            }

            [Fact]
            public async void Should_Throw_For_Already_Cancelled_Token()
            {
                // Given
                var fixture = new TFixture();
                fixture.GivenCancellationToken(new CancellationToken(true));

                // When
                var result = await (Record.ExceptionAsync(() => fixture.Run()) ?? Task.FromResult<Exception>(null));

                // Then
                Assert.IsType<TaskCanceledException>(result);
            }

            [Fact]
            public async void Should_Throw_For_Cancellation_Token()
            {
                // Given
                using (var tokenSource = new CancellationTokenSource())
                {
                    var fixture = new TFixture();
                    fixture.GivenCancellationToken(tokenSource.Token);

                    // When
                    var task = Record.ExceptionAsync(() => fixture.Run()) ?? Task.FromResult<Exception>(null);

                    Thread.Sleep(100);
                    tokenSource.Cancel();

                    var result = await task;

                    // Then
                    Assert.IsType<OperationCanceledException>(result);
                }
            }

            [Fact]
            public async void Should_Throw_Timeout_Exception()
            {
                // Given
                var fixture = new TFixture();
                fixture.Settings.ToolTimeout = new TimeSpan(0, 0, 0, 0, 100);
                fixture.GivenCancellationToken(CancellationToken.None);

                // When
                var result = await (Record.ExceptionAsync(() => fixture.Run()) ?? Task.FromResult<Exception>(null));

                // Then
                Assert.IsType<TimeoutException>(result);
            }

            [Fact]
            public async void Should_Add_Serve_Command()
            {
                // Given
                var fixture = new TFixture();

                // When
                var result = await fixture.Run();

                // Then
                Assert.Equal("serve", result.Args);
            }

            [Theory]
            [InlineData("./project/mkdocs.yml", "/Working/project/mkdocs.yml")]
            [InlineData("/project the second/mkdocs.yml", "/project the second/mkdocs.yml")]
            public async void Should_Add_Config_File_Argument_If_Defined(string configFile, string expected)
            {
                // Given
                var fixture = new TFixture();
                fixture.Settings.ConfigFile = configFile;

                // When
                var result = await fixture.Run();

                // Then
                Assert.Contains($"--config-file \"{expected}\"", result.Args);
            }

            [Fact]
            public async void Should_Not_Add_Config_File_Argument_If_Not_Defined()
            {
                // Given
                var fixture = new TFixture();

                // When
                var result = await fixture.Run();

                // Then
                Assert.DoesNotContain("--config-file", result.Args);
            }

            [Theory]
            [InlineData("127.0.0.1", 8000, "127.0.0.1:8000")]
            [InlineData("localhost", 8080, "localhost:8080")]
            public async void Should_Add_Dev_Addr_Argument_If_Defined(string ip, int port, string expected)
            {
                // Given
                var fixture = new TFixture();
                fixture.Settings.DevAddr = new MkDocsAddress(ip, port);

                // When
                var result = await fixture.Run();

                // Then
                Assert.Contains($"--dev-addr {expected}", result.Args);
            }

            [Fact]
            public async void Should_Not_Add_Dev_Addr_Argument_If_Not_Defined()
            {
                // Given
                var fixture = new TFixture();

                // When
                var result = await fixture.Run();

                // Then
                Assert.DoesNotContain("--dev-addr", result.Args);
            }

            [Fact]
            public async void Should_Add_Strict_Argument_If_Defined()
            {
                // Given
                var fixture = new TFixture();
                fixture.Settings.Strict = true;

                // When
                var result = await fixture.Run();

                // Then
                Assert.Contains("--strict", result.Args);
            }

            [Fact]
            public async void Should_Not_Add_Strict_Argument_If_Not_Defined()
            {
                // Given
                var fixture = new TFixture();

                // When
                var result = await fixture.Run();

                // Then
                Assert.DoesNotContain("--strict", result.Args);
            }

            [Theory]
            [InlineData(MkDocsTheme.MkDocs, "mkdocs")]
            [InlineData(MkDocsTheme.ReadTheDocs, "readthedocs")]
            public async void Should_Add_Theme_Argument_If_Defined(MkDocsTheme theme, string expected)
            {
                // Given
                var fixture = new TFixture();
                fixture.Settings.Theme = theme;

                // When
                var result = await fixture.Run();

                // Then
                Assert.Contains($"--theme {expected}", result.Args);
            }

            [Fact]
            public async void Should_Not_Add_Theme_Argument_If_Not_Defined()
            {
                // Given
                var fixture = new TFixture();

                // When
                var result = await fixture.Run();

                // Then
                Assert.DoesNotContain("--theme", result.Args);
            }

            [Theory]
            [InlineData("./project/theme", "/Working/project/theme")]
            [InlineData("/project the second/theme", "/project the second/theme")]
            public async void Should_Add_Theme_Dir_Argument_If_Defined(string themeDir, string expected)
            {
                // Given
                var fixture = new TFixture();
                fixture.Settings.ThemeDir = themeDir;

                // When
                var result = await fixture.Run();

                // Then
                Assert.Contains($"--theme-dir \"{expected}\"", result.Args);
            }

            [Fact]
            public async void Should_Not_Add_Theme_Dir_Argument_If_Not_Defined()
            {
                // Given
                var fixture = new TFixture();

                // When
                var result = await fixture.Run();

                // Then
                Assert.DoesNotContain("--theme-dir", result.Args);
            }

            [Fact]
            public async void Should_Add_Live_Reload_Argument_If_Defined()
            {
                // Given
                var fixture = new TFixture();
                fixture.Settings.LiveReload = true;

                // When
                var result = await fixture.Run();

                // Then
                Assert.Contains("--livereload", result.Args);
            }

            [Fact]
            public async void Should_Not_Add_Live_Reload_Argument_If_Not_Defined()
            {
                // Given
                var fixture = new TFixture();

                // When
                var result = await fixture.Run();

                // Then
                Assert.DoesNotContain("--livereload", result.Args);
            }

            [Fact]
            public async void Should_Add_No_Live_Reload_Argument_If_Defined()
            {
                // Given
                var fixture = new TFixture();
                fixture.Settings.NoLiveReload = true;

                // When
                var result = await fixture.Run();

                // Then
                Assert.Contains("--no-livereload", result.Args);
            }

            [Fact]
            public async void Should_Not_Add_No_Live_Reload_Argument_If_Not_Defined()
            {
                // Given
                var fixture = new TFixture();

                // When
                var result = await fixture.Run();

                // Then
                Assert.DoesNotContain("--no-livereload", result.Args);
            }

            [Fact]
            public async void Should_Add_Dirty_Reload_Argument_If_Defined()
            {
                // Given
                var fixture = new TFixture();
                fixture.Settings.DirtyReload = true;

                // When
                var result = await fixture.Run();

                // Then
                Assert.Contains("--dirtyreload", result.Args);
            }

            [Fact]
            public async void Should_Not_Add_Dirty_Reload_Argument_If_Not_Defined()
            {
                // Given
                var fixture = new TFixture();

                // When
                var result = await fixture.Run();

                // Then
                Assert.DoesNotContain("--dirtyreload", result.Args);
            }
        }

        public sealed class TheServeAsyncInWorkingDirMethod
            : BaseMkDocsServeAsyncTests<MkDocsServeAsyncRunnerWorkingDirFixture>
        {
            [Theory]
            [InlineData("/Working")]
            public async void Should_Not_Change_Process_Working_Directory(string expected)
            {
                // Given
                var fixture = new MkDocsServeAsyncRunnerWorkingDirFixture();

                // When
                var result = await fixture.Run();

                // Then
                Assert.Equal(expected, result.Process.WorkingDirectory.FullPath);
            }
        }

        public sealed class TheServeAsyncMethod
            : BaseMkDocsServeAsyncTests<MkDocsServeAsyncRunnerWorkingDirFixture>
        {
            [Theory]
            [InlineData("./project", "/Working/project")]
            [InlineData("/project the second/", "/project the second")]
            public async void Should_Change_Process_Working_Directory(string dir, string expected)
            {
                // Given
                var fixture = new MkDocsServeAsyncRunnerFixture();
                fixture.GivenProjectDirectory(dir);

                // When
                var result = await fixture.Run();

                // Then
                Assert.Equal(expected, result.Process.WorkingDirectory.FullPath);
            }

            [Fact]
            public async void Should_Throw_For_Empty_Project_Dir()
            {
                // Given
                var fixture = new MkDocsServeAsyncRunnerFixture();
                fixture.GivenProjectDirectory(null);

                // When
                var result = await (Record.ExceptionAsync(() => fixture.Run()) ?? Task.FromResult<Exception>(null));

                // Then
                Assert.IsType<ArgumentNullException>(result);
                Assert.Equal("projectDirectory", ((ArgumentNullException)result).ParamName);
            }
        }
    }
}
