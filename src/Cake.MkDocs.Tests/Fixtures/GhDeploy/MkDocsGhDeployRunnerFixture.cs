using Cake.Core.IO;
using Cake.MkDocs.GhDeploy;

namespace Cake.MkDocs.Tests.Fixtures.GhDeploy
{
    public sealed class MkDocsGhDeployRunnerFixture : MkDocsFixture<MkDocsGhDeploySettings>
    {
        private DirectoryPath _projectDirectory = "project";

        public void GivenProjectDirectory(string projectDirectory)
        {
            _projectDirectory = !string.IsNullOrWhiteSpace(projectDirectory) ? new DirectoryPath(projectDirectory) : null;
        }

        protected override void RunTool()
        {
            var tool = new MkDocsGhDeployRunner(FileSystem, Environment, ProcessRunner, Tools);
            tool.GhDeploy(_projectDirectory, Settings);
        }
    }
}
