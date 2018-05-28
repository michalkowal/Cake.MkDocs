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
        }
    }
}
