using System;
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
    }
}
