using Cake.MkDocs.Version;

namespace Cake.MkDocs.Tests.Fixtures.Version
{
    public sealed class MkDocsVersionRunnerFixture : BaseMkDocsVersionRunnerFixture
    {
        public System.Version Result { get; private set; }

        protected override void RunTool()
        {
            var tool = new MkDocsVersionRunner(FileSystem, Environment, ProcessRunner, Tools);
            Result = tool.Version(Settings);
        }
    }
}
