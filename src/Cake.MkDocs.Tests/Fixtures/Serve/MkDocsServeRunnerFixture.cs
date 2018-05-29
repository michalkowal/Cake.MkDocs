using Cake.Core.IO;
using Cake.MkDocs.Serve;

namespace Cake.MkDocs.Tests.Fixtures.Serve
{
    public sealed class MkDocsServeRunnerFixture : MkDocsFixture<MkDocsServeSettings>
    {
        private DirectoryPath _projectDirectory = "project";

        public void GivenProjectDirectory(string projectDirectory)
        {
            _projectDirectory = !string.IsNullOrWhiteSpace(projectDirectory) ? new DirectoryPath(projectDirectory) : null;
        }

        protected override void RunTool()
        {
            var tool = new MkDocsServeRunner(FileSystem, Environment, ProcessRunner, Tools);
            tool.Serve(_projectDirectory, Settings);
        }
    }
}
