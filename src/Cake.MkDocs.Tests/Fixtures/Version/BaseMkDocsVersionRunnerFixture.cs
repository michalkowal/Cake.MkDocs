using Cake.MkDocs.Version;

namespace Cake.MkDocs.Tests.Fixtures.Version
{
    public abstract class BaseMkDocsVersionRunnerFixture : MkDocsFixture<MkDocsVersionSettings>
    {
        private string _toolResult;

        public BaseMkDocsVersionRunnerFixture()
        {
            GivenMkDocsVersion("0.17.3");
        }

        public void GivenMkDocsVersion(string version)
        {
            _toolResult = $"mkdocs, version {version}";
            ProcessRunner.Process.SetStandardOutput(new[]
            {
                _toolResult
            });
        }

        public void GivenNoOutput()
        {
            ProcessRunner.Process.SetStandardOutput(null);
        }
    }
}
