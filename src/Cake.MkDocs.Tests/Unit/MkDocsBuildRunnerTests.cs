using Cake.MkDocs.Build;
using Cake.MkDocs.Tests.Fixtures.Build;
using Xunit;

namespace Cake.MkDocs.Tests.Unit
{
    public sealed class MkDocsBuildRunnerTests
    {
        public sealed class TheBuildMethod
            : MkDocsToolTests<MkDocsBuildRunnerFixture, MkDocsBuildSettings>
        {
            [Fact]
            public void Should_Add_Build_Command()
            {
                // Given
                var fixture = new MkDocsBuildRunnerFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("build", result.Args);
            }
        }
    }
}
