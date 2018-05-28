using Cake.MkDocs.Version;

namespace Cake.MkDocs.Tests.Fixtures.Version
{
    public sealed class MkDocsVersionRunnerIsSupportedFixture : BaseMkDocsVersionRunnerFixture
    {
        public bool Result { get; private set; }

        protected override void RunTool()
        {
            var tool = new MkDocsVersionRunner(FileSystem, Environment, ProcessRunner, Tools);
            Result = tool.IsSupportedVersion(Settings);
        }
    }
}
