using Cake.MkDocs.Serve;
using Cake.MkDocs.Tests.Fixtures.Serve;
using Xunit;

namespace Cake.MkDocs.Tests.Unit
{
    public sealed class MkDocsServeRunnerTests
    {
        public sealed class TheServeMethod
            : MkDocsToolTests<MkDocsServeRunnerFixture, MkDocsServeSettings>
        {
            [Fact]
            public void Should_Add_Serve_Command()
            {
                // Given
                var fixture = new MkDocsServeRunnerFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("serve", result.Args);
            }
        }
    }
}
