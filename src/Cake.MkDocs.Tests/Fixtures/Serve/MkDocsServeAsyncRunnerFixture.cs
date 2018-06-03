using System.Threading.Tasks;
using Cake.Core.IO;
using Cake.MkDocs.Serve;

namespace Cake.MkDocs.Tests.Fixtures.Serve
{
    public sealed class MkDocsServeAsyncRunnerFixture : BaseMkDocsServeAsyncFixture
    {
        private DirectoryPath _projectDirectory = "project";

        public void GivenProjectDirectory(string projectDirectory)
        {
            _projectDirectory = !string.IsNullOrWhiteSpace(projectDirectory) ? new DirectoryPath(projectDirectory) : null;
        }

        protected override Task RunTool()
        {
            var tool = new MkDocsServeAsyncRunner(FileSystem, Environment, ProcessRunner, Tools);
            return tool.ServeAsync(_projectDirectory, Settings);
        }
    }
}
