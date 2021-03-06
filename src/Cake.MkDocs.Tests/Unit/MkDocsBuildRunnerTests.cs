﻿using System;
using Cake.MkDocs.Build;
using Cake.MkDocs.Tests.Fixtures;
using Cake.MkDocs.Tests.Fixtures.Build;
using Xunit;

namespace Cake.MkDocs.Tests.Unit
{
    public sealed class MkDocsBuildRunnerTests
    {
        public abstract class BaseMkDocsBuildTests<TFixture>
            : MkDocsToolTests<TFixture, MkDocsBuildSettings>
            where TFixture : MkDocsFixture<MkDocsBuildSettings>, new()
        {
            [Fact]
            public void Should_Add_Build_Command()
            {
                // Given
                var fixture = new TFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("build", result.Args);
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
                var fixture = new TFixture();
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

            [Theory]
            [InlineData("./project/site", "/Working/project/site")]
            [InlineData("/project the second/site", "/project the second/site")]
            public void Should_Add_Site_Dir_Argument_If_Defined(string siteDir, string expected)
            {
                // Given
                var fixture = new TFixture();
                fixture.Settings.SiteDir = siteDir;

                // When
                var result = fixture.Run();

                // Then
                Assert.Contains($"--site-dir \"{expected}\"", result.Args);
            }

            [Fact]
            public void Should_Not_Add_Site_Dir_Argument_If_Not_Defined()
            {
                // Given
                var fixture = new TFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.DoesNotContain("--site-dir", result.Args);
            }
        }

        public sealed class TheBuildInWorkingDirMethod
            : BaseMkDocsBuildTests<MkDocsBuildRunnerWorkingDirFixture>
        {
            [Theory]
            [InlineData("/Working")]
            public void Should_Not_Change_Process_Working_Directory(string expected)
            {
                // Given
                var fixture = new MkDocsBuildRunnerWorkingDirFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal(expected, result.Process.WorkingDirectory.FullPath);
            }
        }

        public sealed class TheBuildMethod
            : BaseMkDocsBuildTests<MkDocsBuildRunnerWorkingDirFixture>
        {
            [Theory]
            [InlineData("./project", "/Working/project")]
            [InlineData("/project the second/", "/project the second")]
            public void Should_Change_Process_Working_Directory(string dir, string expected)
            {
                // Given
                var fixture = new MkDocsBuildRunnerFixture();
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
                var fixture = new MkDocsBuildRunnerFixture();
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
