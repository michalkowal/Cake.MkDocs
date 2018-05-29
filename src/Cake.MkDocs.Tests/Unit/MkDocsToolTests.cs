using System;
using Cake.Core;
using Cake.MkDocs.Tests.Fixtures;
using Cake.Testing;
using Xunit;

namespace Cake.MkDocs.Tests.Unit
{
    public abstract class MkDocsToolTests<TFixture, TSettings>
        where TFixture : MkDocsFixture<TSettings>, new()
        where TSettings : MkDocsSettings, new()
    {
        [Fact]
        public void Should_Throw_If_Settings_Are_Null()
        {
            // Given
            var fixture = new TFixture();
            fixture.Settings = null;

            // When
            var result = Record.Exception(() => fixture.Run());

            // Then
            Assert.IsType<ArgumentNullException>(result);
            Assert.Equal("settings", ((ArgumentNullException)result).ParamName);
        }

        [Fact]
        public void Should_Throw_If_MkDocs_Executable_Was_Not_Found()
        {
            // Given
            var fixture = new TFixture();
            fixture.GivenDefaultToolDoNotExist();

            // When
            var result = Record.Exception(() => fixture.Run());

            // Then
            Assert.IsType<CakeException>(result);
            Assert.Equal("MkDocs: Could not locate executable.", ((CakeException)result).Message);
        }

        [Theory]
        [InlineData("/bin/python/Scripts/mkdocs.exe", "/bin/python/Scripts/mkdocs.exe")]
        [InlineData("./tools/python/Scripts/mkdocs.exe", "/Working/tools/python/Scripts/mkdocs.exe")]
        public void Should_Use_MkDocs_Executable_From_Tool_Path_If_Provided(string toolPath, string expected)
        {
            // Given
            var fixture = new TFixture();
            fixture.Settings.ToolPath = toolPath;
            fixture.GivenSettingsToolPathExist();

            // When
            var result = fixture.Run();

            // Then
            Assert.Equal(expected, result.Path.FullPath);
        }

        [Fact]
        public void Should_Find_MkDocs_Executable_If_Tool_Path_Not_Provided()
        {
            // Given
            var fixture = new TFixture();

            // When
            var result = fixture.Run();

            // Then
            Assert.Equal("/Working/tools/mkdocs", result.Path.FullPath);
        }

        [Theory]
        [InlineData("/python/Scripts", "/python/Scripts/mkdocs.exe")]
        public void Should_Find_MkDocs_Executable_From_Environment_Path_If_Tool_Path_Not_Provided(string environmentPath, string expected)
        {
            // Given
            var fixture = new TFixture();
            fixture.GivenDefaultToolDoNotExist();
            fixture.GivenEnvironmentPath(environmentPath);

            // When
            var result = fixture.Run();

            // Then
            Assert.Equal(expected, result.Path.FullPath);
        }

        [Theory]
        [InlineData("/python/Scripts", "/python/Scripts/mkdocs.exe")]
        public void Should_Find_MkDocs_Executable_From_Registry_If_Tool_Path_Not_Provided(string environmentPath, string expected)
        {
            // Given
            var fixture = new TFixture();
            fixture.GivenDefaultToolDoNotExist();
            fixture.GivenRegistryFile(environmentPath);

            // When
            var result = fixture.Run();

            // Then
            Assert.Equal(expected, result.Path.FullPath);
        }

        [Fact]
        public void Should_Throw_If_Process_Was_Not_Started()
        {
            // Given
            var fixture = new TFixture();
            fixture.GivenProcessCannotStart();

            // When
            var result = Record.Exception(() => fixture.Run());

            // Then
            Assert.IsType<CakeException>(result);
            Assert.Equal("MkDocs: Process was not started.", ((CakeException)result).Message);
        }

        [Fact]
        public void Should_Throw_If_Process_Has_A_Non_Zero_Exit_Code()
        {
            // Given
            var fixture = new TFixture();
            fixture.GivenProcessExitsWithCode(1);

            // When
            var result = Record.Exception(() => fixture.Run());

            // Then
            Assert.IsType<CakeException>(result);
            Assert.Equal("MkDocs: Process returned an error (exit code 1).", ((CakeException)result).Message);
        }

        [Fact]
        public void Should_Add_Quiet_Argument_If_Defined()
        {
            // Given
            var fixture = new TFixture();
            fixture.Settings.Quiet = true;

            // When
            var result = fixture.Run();

            // Then
            Assert.Contains("--quiet", result.Args);
        }

        [Fact]
        public void Should_Not_Add_Quiet_Argument_If_Not_Defined()
        {
            // Given
            var fixture = new TFixture();

            // When
            var result = fixture.Run();

            // Then
            Assert.DoesNotContain("--quiet", result.Args);
        }

        [Fact]
        public void Should_Add_Verbose_Argument_If_Defined()
        {
            // Given
            var fixture = new TFixture();
            fixture.Settings.Verbose = true;

            // When
            var result = fixture.Run();

            // Then
            Assert.Contains("--verbose", result.Args);
        }

        [Fact]
        public void Should_Not_Add_Verbose_Argument_If_Not_Defined()
        {
            // Given
            var fixture = new TFixture();

            // When
            var result = fixture.Run();

            // Then
            Assert.DoesNotContain("--verbose", result.Args);
        }
    }
}
