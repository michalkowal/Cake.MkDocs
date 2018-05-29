using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.MkDocs.Build
{
    /// <summary>
    /// The MkDocs build tool creates a new MkDocs project.
    /// </summary>
    public sealed class MkDocsBuildRunner : MkDocsTool<MkDocsBuildSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MkDocsBuildRunner"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="processRunner">The process runner.</param>
        /// <param name="tools">The tool locator.</param>
        public MkDocsBuildRunner(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner, IToolLocator tools)
            : base(fileSystem, environment, processRunner, tools)
        {
        }

        /// <summary>
        /// Build the MkDocs documentation in working directory.
        /// </summary>
        /// <param name="settings">The settings</param>
        public void Build(MkDocsBuildSettings settings)
        {
            Run(settings);
        }

        /// <summary>
        /// Build the MkDocs documentation.
        /// </summary>
        /// <param name="projectDirectory">Project dir to build.</param>
        /// <param name="settings">The settings</param>
        public void Build(DirectoryPath projectDirectory, MkDocsBuildSettings settings)
        {
            Run(settings, projectDirectory);
        }
    }
}
