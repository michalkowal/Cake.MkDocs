using System;
using Cake.MkDocs.GhDeploy;
using Cake.MkDocs.Tests.Fixtures;
using Cake.MkDocs.Tests.Fixtures.GhDeploy;
using Xunit;

namespace Cake.MkDocs.Tests.Unit
{
    public sealed class MkDocsGhDeployRunnerTests
    {
        public abstract class BaseMkDocsGhDeployTests<TFixture>
            : MkDocsToolTests<TFixture, MkDocsGhDeploySettings>
            where TFixture : MkDocsFixture<MkDocsGhDeploySettings>, new()
        {
            [Fact]
            public void Should_Add_GhDeploy_Command()
            {
                // Given
                var fixture = new TFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("gh-deploy", result.Args);
            }

            [Fact]
            public void Should_Add_Clean_Argument_If_Defined()
            {
                // Given
                var fixture = new TFixture();
                fixture.Settings.Clean = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Contains("--clean", result.Args);
            }

            [Fact]
            public void Should_Not_Add_Clean_Argument_If_Not_Defined()
            {
                // Given
                var fixture = new TFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.DoesNotContain("--clean", result.Args);
            }

            [Fact]
            public void Should_Add_Dirty_Argument_If_Defined()
            {
                // Given
                var fixture = new MkDocsGhDeployRunnerWorkingDirFixture();
                fixture.Settings.Dirty = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Contains("--dirty", result.Args);
            }

            [Fact]
            public void Should_Not_Add_Dirty_Argument_If_Not_Defined()
            {
                // Given
                var fixture = new TFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.DoesNotContain("--dirty", result.Args);
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
            [InlineData("commit message")]
            [InlineData("Commit message\nCloses #1256")]
            public void Should_Add_Message_Argument_If_Defined(string message)
            {
                // Given
                var fixture = new TFixture();
                fixture.Settings.Message = message;

                // When
                var result = fixture.Run();

                // Then
                Assert.Contains($"--message \"{message}\"", result.Args);
            }

            [Fact]
            public void Should_Not_Add_Message_Argument_If_Not_Defined()
            {
                // Given
                var fixture = new TFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.DoesNotContain("--message", result.Args);
            }

            [Theory]
            [InlineData("develop")]
            [InlineData("feature-branch-name")]
            [InlineData("release/1.2.0")]
            public void Should_Add_Remote_Branch_Argument_If_Defined(string remoteBranch)
            {
                // Given
                var fixture = new TFixture();
                fixture.Settings.RemoteBranch = remoteBranch;

                // When
                var result = fixture.Run();

                // Then
                Assert.Contains($"--remote-branch \"{remoteBranch}\"", result.Args);
            }

            [Fact]
            public void Should_Not_Add_Remote_Branch_Argument_If_Not_Defined()
            {
                // Given
                var fixture = new TFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.DoesNotContain("--remote-branch", result.Args);
            }

            [Theory]
            [InlineData("origin")]
            public void Should_Add_Remote_Name_Argument_If_Defined(string remoteName)
            {
                // Given
                var fixture = new TFixture();
                fixture.Settings.RemoteName = remoteName;

                // When
                var result = fixture.Run();

                // Then
                Assert.Contains($"--remote-name \"{remoteName}\"", result.Args);
            }

            [Fact]
            public void Should_Not_Add_Remote_Name_Argument_If_Not_Defined()
            {
                // Given
                var fixture = new TFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.DoesNotContain("--remote-name", result.Args);
            }

            [Fact]
            public void Should_Add_Force_Argument_If_Defined()
            {
                // Given
                var fixture = new TFixture();
                fixture.Settings.Force = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Contains("--force", result.Args);
            }

            [Fact]
            public void Should_Not_Add_Force_Argument_If_Not_Defined()
            {
                // Given
                var fixture = new TFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.DoesNotContain("--force", result.Args);
            }

            [Fact]
            public void Should_Add_IgnoreVersion_Argument_If_Defined()
            {
                // Given
                var fixture = new TFixture();
                fixture.Settings.IgnoreVersion = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Contains("--ignore-version", result.Args);
            }

            [Fact]
            public void Should_Not_Add_IgnoreVersion_Argument_If_Not_Defined()
            {
                // Given
                var fixture = new TFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.DoesNotContain("--ignore-version", result.Args);
            }
        }

        public sealed class TheGhDeployInWorkingDirMethod
            : BaseMkDocsGhDeployTests<MkDocsGhDeployRunnerWorkingDirFixture>
        {
            [Theory]
            [InlineData("/Working")]
            public void Should_Not_Change_Process_Working_Directory(string expected)
            {
                // Given
                var fixture = new MkDocsGhDeployRunnerWorkingDirFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal(expected, result.Process.WorkingDirectory.FullPath);
            }
        }

        public sealed class TheGhDeployMethod
            : BaseMkDocsGhDeployTests<MkDocsGhDeployRunnerWorkingDirFixture>
        {
            [Theory]
            [InlineData("./project", "/Working/project")]
            [InlineData("/project the second/", "/project the second")]
            public void Should_Change_Process_Working_Directory(string dir, string expected)
            {
                // Given
                var fixture = new MkDocsGhDeployRunnerFixture();
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
                var fixture = new MkDocsGhDeployRunnerFixture();
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
