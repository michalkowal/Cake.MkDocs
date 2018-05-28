using Cake.MkDocs.Build;

namespace Cake.MkDocs.Tests.Fixtures.Build
{
    public sealed class MkDocsBuildRunnerFixture : MkDocsFixture<MkDocsBuildSettings>
    {
        protected override void RunTool()
        {
            var tool = new MkDocsBuildRunner(FileSystem, Environment, ProcessRunner, Tools);
            tool.Build(Settings);
        }
    }
}
