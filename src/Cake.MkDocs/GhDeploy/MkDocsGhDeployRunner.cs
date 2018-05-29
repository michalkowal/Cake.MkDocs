using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.MkDocs.GhDeploy
{
    /// <summary>
    /// The MkDocs gh-deploy tool creates a new MkDocs project.
    /// </summary>
    public sealed class MkDocsGhDeployRunner : MkDocsTool<MkDocsGhDeploySettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MkDocsGhDeployRunner"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="processRunner">The process runner.</param>
        /// <param name="tools">The tool locator.</param>
        public MkDocsGhDeployRunner(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner,
            IToolLocator tools)
            : base(fileSystem, environment, processRunner, tools)
        {
        }

        /// <summary>
        /// Deploy your documentation to GitHub Pages (project in working directory).
        /// </summary>
        /// <param name="settings">The settings</param>
        public void GhDeploy(MkDocsGhDeploySettings settings)
        {
            Run(settings);
        }

        /// <summary>
        /// Deploy your documentation to GitHub Pages.
        /// </summary>
        /// <param name="projectDirectory">Project dir to deploy.</param>
        /// <param name="settings">The settings</param>
        public void GhDeploy(DirectoryPath projectDirectory, MkDocsGhDeploySettings settings)
        {
            Run(settings, projectDirectory);
        }
    }
}
