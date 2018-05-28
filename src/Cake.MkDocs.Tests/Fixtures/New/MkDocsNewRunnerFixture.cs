using Cake.MkDocs.New;

namespace Cake.MkDocs.Tests.Fixtures.New
{
    public sealed class MkDocsNewRunnerFixture : MkDocsFixture<MkDocsNewSettings>
    {
        private string _projectDirectory = "project";

        public void GivenProjectDirectory(string projectDirectory)
        {
            _projectDirectory = projectDirectory;
        }

        protected override void RunTool()
        {
            var tool = new MkDocsNewRunner(FileSystem, Environment, ProcessRunner, Tools);
            tool.New(_projectDirectory, Settings);
        }
    }
}
