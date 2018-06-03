using System.Threading.Tasks;
using Cake.MkDocs.Serve;

namespace Cake.MkDocs.Tests.Fixtures.Serve
{
    public sealed class MkDocsServeAsyncRunnerWorkingDirFixture : BaseMkDocsServeAsyncFixture
    {
        protected override Task RunTool()
        {
            var tool = new MkDocsServeAsyncRunner(FileSystem, Environment, ProcessRunner, Tools);
            return tool.ServeAsync(Settings);
        }
    }
}
