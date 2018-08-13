using Cake.MkDocs.Version;

namespace Cake.MkDocs.Tests.Fixtures.Version
{
    public abstract class BaseMkDocsVersionRunnerFixture : MkDocsFixture<MkDocsVersionSettings>
    {
        private string _toolResult;

        public BaseMkDocsVersionRunnerFixture()
        {
            GivenMkDocsVersion("1.0.0");
        }

        public void GivenMkDocsVersion(string version)
        {
            _toolResult = $"mkdocs, version {version} from \\local\\python\\python36\\lib\\site-packages\\mkdocs (Python 3.6)";
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
