using Cake.MkDocs.Serve;

namespace Cake.MkDocs.Tests.Fixtures.Serve
{
    public sealed class MkDocsServeRunnerFixture : MkDocsFixture<MkDocsServeSettings>
    {
        protected override void RunTool()
        {
            var tool = new MkDocsServeRunner(FileSystem, Environment, ProcessRunner, Tools);
            tool.Serve(Settings);
        }
    }
}
