using System;
using Cake.MkDocs.New;
using Cake.MkDocs.Tests.Fixtures.New;
using Xunit;

namespace Cake.MkDocs.Tests.Unit
{
    public sealed class MkDocsNewRunnerTests
    {
        public sealed class TheNewMethod
            : MkDocsToolTests<MkDocsNewRunnerFixture, MkDocsNewSettings>
        {
            [Theory]
            [InlineData("./project")]
            [InlineData("/project the second/")]
            public void Should_Add_New_Command(string dir)
            {
                // Given
                var fixture = new MkDocsNewRunnerFixture();
                fixture.GivenProjectDirectory(dir);

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"new \"{dir}\"", result.Args);
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
