using System;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.MkDocs.Serve
{
    /// <summary>
    /// The <c>MkDocs</c> serve tool buid and generate preview of <c>MkDocs</c> documentation.
    /// </summary>
    public sealed class MkDocsServeRunner : MkDocsTool<MkDocsServeSettings>
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
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        /// runner.Serve(new MkDocsServeSettings());
        /// </code>
        /// </example>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="settings"/> is not set.</exception>
        /// <exception cref="TimeoutException">Thrown when ToolTimeout specifed and process is still working after this time.</exception>
        /// <exception cref="CakeException">Thrown when tool process ends with code different than <c>0</c>.</exception>
        public void Serve(MkDocsServeSettings settings)
        {
            Run(settings);
        }

        /// <summary>
        /// Run the builtin development server.
        /// </summary>
        /// <param name="projectDirectory">Project dir to serve.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        /// runner.Serve(new DirectoryPath("./project-with-docs-is-here"), new MkDocsServeSettings());
        /// </code>
        /// </example>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="projectDirectory"/> or <paramref name="settings"/> are not set.</exception>
        /// <exception cref="TimeoutException">Thrown when ToolTimeout specifed and process is still working after this time.</exception>
        /// <exception cref="CakeException">Thrown when tool process ends with code different than <c>0</c>.</exception>
        public void Serve(DirectoryPath projectDirectory, MkDocsServeSettings settings)
        {
            Run(settings, projectDirectory);
        }
    }
}
