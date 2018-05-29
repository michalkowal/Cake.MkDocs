using Cake.MkDocs.GhDeploy;

namespace Cake.MkDocs.Tests.Fixtures.GhDeploy
{
    public sealed class MkDocsGhDeployRunnerWorkingDirFixture : MkDocsFixture<MkDocsGhDeploySettings>
    {
        protected override void RunTool()
        {
            var tool = new MkDocsGhDeployRunner(FileSystem, Environment, ProcessRunner, Tools);
            tool.GhDeploy(Settings);
        }
    }
}
