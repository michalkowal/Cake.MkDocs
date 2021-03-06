﻿using System;
using Cake.MkDocs.New;
using Cake.MkDocs.Tests.Fixtures.New;
using Xunit;

namespace Cake.MkDocs.Tests.Unit
{
    public sealed class MkDocsNewRunnerTests
    {
        public sealed class TheNewInWorkingDirMethod
            : MkDocsToolTests<MkDocsNewRunnerWorkingDirFixture, MkDocsNewSettings>
        {
            [Theory]
            [InlineData("/Working")]
            public void Should_Add_New_Command(string expected)
            {
                // Given
                var fixture = new MkDocsNewRunnerWorkingDirFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"new \"{expected}\"", result.Args);
            }
        }

        public sealed class TheNewMethod
            : MkDocsToolTests<MkDocsNewRunnerFixture, MkDocsNewSettings>
        {
            [Theory]
            [InlineData("./project", "/Working/project")]
            [InlineData("/project the second/", "/project the second")]
            public void Should_Add_New_Command(string dir, string expected)
            {
                // Given
                var fixture = new MkDocsNewRunnerFixture();
                fixture.GivenProjectDirectory(dir);

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"new \"{expected}\"", result.Args);
            }

            [Fact]
            public void Should_Throw_For_Empty_Project_Dir()
            {
                // Given
                var fixture = new MkDocsNewRunnerFixture();
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
