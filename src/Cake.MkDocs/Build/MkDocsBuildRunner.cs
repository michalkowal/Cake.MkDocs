using System;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.MkDocs.Build
{
    /// <summary>
    /// The <c>MkDocs</c> build tool for <c>MkDocs</c> project.
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
        /// Build the <c>MkDocs</c> documentation in working directory.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        /// runner.Build(new MkDocsBuildSettings());
        /// </code>
        /// </example>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="settings"/> is not set.</exception>
        /// <exception cref="TimeoutException">Thrown when ToolTimeout specifed and process is still working after this time.</exception>
        /// <exception cref="CakeException">Thrown when tool process ends with code different than <c>0</c>.</exception>
        public void Build(MkDocsBuildSettings settings)
        {
            Run(settings);
        }

        /// <summary>
        /// Build the <c>MkDocs</c> documentation.
        /// </summary>
        /// <param name="projectDirectory">Project dir to build.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        /// runner.Build(new DirectoryPath("./project-with-docs-is-here"), new MkDocsBuildSettings());
        /// </code>
        /// </example>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="projectDirectory"/> or <paramref name="settings"/> are not set.</exception>
        /// <exception cref="TimeoutException">Thrown when ToolTimeout specifed and process is still working after this time.</exception>
        /// <exception cref="CakeException">Thrown when tool process ends with code different than <c>0</c>.</exception>
        public void Build(DirectoryPath projectDirectory, MkDocsBuildSettings settings)
        {
            Run(settings, projectDirectory);
        }
    }
}
