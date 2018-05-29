using Cake.Core.IO;
using Cake.MkDocs.Build;

namespace Cake.MkDocs.Tests.Fixtures.Build
{
    public sealed class MkDocsBuildRunnerFixture : MkDocsFixture<MkDocsBuildSettings>
    {
        private DirectoryPath _projectDirectory = "project";

        public void GivenProjectDirectory(string projectDirectory)
        {
            _projectDirectory = !string.IsNullOrWhiteSpace(projectDirectory) ? new DirectoryPath(projectDirectory) : null;
        }

        protected override void RunTool()
        {
            var tool = new MkDocsBuildRunner(FileSystem, Environment, ProcessRunner, Tools);
            tool.Build(_projectDirectory, Settings);
        }
    }
}
