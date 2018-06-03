using System.Threading.Tasks;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.MkDocs.Serve
{
    /// <summary>
    /// The MkDocs serve tool creates a new MkDocs project.
    /// </summary>
    public sealed class MkDocsServeRunner : MkDocsAsyncTool<MkDocsServeSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MkDocsServeRunner"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="processRunner">The process runner.</param>
        /// <param name="tools">The tool locator.</param>
        public MkDocsServeRunner(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner,
            IToolLocator tools)
            : base(fileSystem, environment, processRunner, tools)
        {
        }

        /// <summary>
        /// Run the builtin development server in working directory.
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <returns>Long running task.</returns>
        public Task Serve(MkDocsServeSettings settings)
        {
            return RunAsync(settings);
        }

        /// <summary>
        /// Run the builtin development server.
        /// </summary>
        /// <param name="projectDirectory">Project dir to serve.</param>
        /// <param name="settings">The settings</param>
        /// <returns>Long running task.</returns>
        public Task Serve(DirectoryPath projectDirectory, MkDocsServeSettings settings)
        {
            return RunAsync(settings, projectDirectory);
        }
    }
}
