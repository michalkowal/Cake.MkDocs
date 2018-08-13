using Cake.Core;
using Cake.MkDocs.Tests.Fixtures.Version;
using Cake.MkDocs.Version;
using Xunit;

namespace Cake.MkDocs.Tests.Unit
{
    public sealed class MkDocsVersionRunnerTests
    {
        public abstract class BaseMkDocsVersionTests<TFixture>
            : MkDocsToolTests<TFixture, MkDocsVersionSettings>
            where TFixture : BaseMkDocsVersionRunnerFixture, new()
        {
            [Fact]
            public void Should_Add_Version_Argument()
            {
                // Given
                var fixture = new TFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("--version", result.Args);
            }

            [Fact]
            public void Should_Throw_If_Tool_Return_Incorrect_Version()
            {
                // Given
                var fixture = new TFixture();
                fixture.GivenMkDocsVersion("alpha4");

                // When
                var result = Record.Exception(() => fixture.Run());

                // Then
                Assert.IsType<CakeException>(result);
                Assert.Equal("Invalid version.", result.Message);
            }

            [Fact]
            public void Should_Throw_If_Tool_Not_Return_Output()
            {
                // Given
                var fixture = new TFixture();
                fixture.GivenNoOutput();

                // When
                var result = Record.Exception(() => fixture.Run());

                // Then
                Assert.IsType<CakeException>(result);
                Assert.Equal("Invalid version.", result.Message);
            }
        }

        public sealed class TheVersionMethod
            : BaseMkDocsVersionTests<MkDocsVersionRunnerFixture>
        {
            [Fact]
            public void Should_Return_Correct_Version()
            {
                // Given
                var fixture = new MkDocsVersionRunnerFixture();
                fixture.GivenMkDocsVersion("0.17.3");

                // When
                fixture.Run();

                // Then
                Assert.Equal(new System.Version(0, 17, 3), fixture.Result);
            }

            [Fact]
            public void Should_Return_Simple_Version_If_Prerelease()
            {
                // Given
                var fixture = new MkDocsVersionRunnerFixture();
                fixture.GivenMkDocsVersion("0.17.3-beta.4");

                // When
                fixture.Run();

                // Then
                Assert.Equal(new System.Version(0, 17, 3), fixture.Result);
            }
        }

        public sealed class TheIsSupportedVersionMethod
            : BaseMkDocsVersionTests<MkDocsVersionRunnerFixture>
        {
            [Theory]
            [InlineData("1.0.0")]
            [InlineData("1.0.2")]
            [InlineData("1.1.0")]
            [InlineData("1.1.0-beta.2")]
            public void Should_Return_True(string toolVersion)
            {
                // Given
                var fixture = new MkDocsVersionRunnerIsSupportedFixture();
                fixture.GivenMkDocsVersion(toolVersion);

                // When
                fixture.Run();

                // Then
                Assert.True(fixture.Result);
            }

            [Theory]
            [InlineData("2.0.0")]
            [InlineData("0.16.0")]
            [InlineData("2.0.0-beta.4")]
            [InlineData("0.16.0-beta.1")]
            public void Should_Return_False(string toolVersion)
            {
                // Given
                var fixture = new MkDocsVersionRunnerIsSupportedFixture();
                fixture.GivenMkDocsVersion(toolVersion);

                // When
                fixture.Run();

                // Then
                Assert.False(fixture.Result);
            }
        }
    }
}
