using System.Threading.Tasks;
using Cake.MkDocs.Serve;

namespace Cake.MkDocs.Tests.Fixtures.Serve
{
    public sealed class MkDocsServeRunnerWorkingDirFixture : MkDocsAsyncFixture<MkDocsServeSettings>
    {
        protected override Task RunTool()
        {
            var tool = new MkDocsServeRunner(FileSystem, Environment, ProcessRunner, Tools);
            return tool.Serve(Settings);
        }
    }
}
