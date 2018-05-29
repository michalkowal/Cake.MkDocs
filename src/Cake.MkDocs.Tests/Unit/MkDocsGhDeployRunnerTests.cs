using Cake.MkDocs.GhDeploy;
using Cake.MkDocs.Tests.Fixtures.GhDeploy;
using Xunit;

namespace Cake.MkDocs.Tests.Unit
{
    public sealed class MkDocsGhDeployRunnerTests
    {
        public sealed class TheGhDeployMethod
            : MkDocsToolTests<MkDocsGhDeployRunnerFixture, MkDocsGhDeploySettings>
        {
            [Fact]
            public void Should_Add_GhDeploy_Command()
            {
                // Given
                var fixture = new MkDocsGhDeployRunnerFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("gh-deploy", result.Args);
            }

            [Fact]
            public void Should_Add_Clean_Argument_If_Defined()
            {
                // Given
                var fixture = new MkDocsGhDeployRunnerFixture();
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
                var fixture = new MkDocsGhDeployRunnerFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.DoesNotContain("--clean", result.Args);
            }

            [Fact]
            public void Should_Add_Dirty_Argument_If_Defined()
            {
                // Given
                var fixture = new MkDocsGhDeployRunnerFixture();
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
                var fixture = new MkDocsGhDeployRunnerFixture();

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
                var fixture = new MkDocsGhDeployRunnerFixture();
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
                var fixture = new MkDocsGhDeployRunnerFixture();

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
                var fixture = new MkDocsGhDeployRunnerFixture();
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
                var fixture = new MkDocsGhDeployRunnerFixture();

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
                var fixture = new MkDocsGhDeployRunnerFixture();
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
                var fixture = new MkDocsGhDeployRunnerFixture();

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
                var fixture = new MkDocsGhDeployRunnerFixture();
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
                var fixture = new MkDocsGhDeployRunnerFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.DoesNotContain("--remote-name", result.Args);
            }

            [Fact]
            public void Should_Add_Force_Argument_If_Defined()
            {
                // Given
                var fixture = new MkDocsGhDeployRunnerFixture();
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
                var fixture = new MkDocsGhDeployRunnerFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.DoesNotContain("--force", result.Args);
            }
        }
    }
}
