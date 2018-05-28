using Cake.Core.IO;
using Cake.MkDocs.New;

namespace Cake.MkDocs.Tests.Fixtures.New
{
    public sealed class MkDocsNewRunnerFixture : MkDocsFixture<MkDocsNewSettings>
    {
        private DirectoryPath _projectDirectory = "project";

        public void GivenProjectDirectory(string projectDirectory)
        {
            _projectDirectory = !string.IsNullOrWhiteSpace(projectDirectory) ? new DirectoryPath(projectDirectory) : null;
        }

        protected override void RunTool()
        {
            var tool = new MkDocsNewRunner(FileSystem, Environment, ProcessRunner, Tools);
            tool.New(_projectDirectory, Settings);
        }
    }
}
