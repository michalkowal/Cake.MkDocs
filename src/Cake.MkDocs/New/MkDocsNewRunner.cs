using System;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.MkDocs.New
{
    /// <summary>
    /// The <c>MkDocs</c> new tool creates a new <c>MkDocs</c> project.
    /// </summary>
    public sealed class MkDocsNewRunner : MkDocsTool<MkDocsNewSettings>
    {
        private readonly ICakeEnvironment _environment;

        /// <summary>
        /// Initializes a new instance of the <see cref="MkDocsNewRunner"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="processRunner">The process runner.</param>
        /// <param name="tools">The tool locator.</param>
        public MkDocsNewRunner(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner, IToolLocator tools)
            : base(fileSystem, environment, processRunner, tools)
        {
            _environment = environment;
        }

        /// <summary>
        /// Create a new <c>MkDocs</c> project in current directory.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        /// runner.New(new MkDocsNewSettings());
        /// </code>
        /// </example>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="settings"/> is not set.</exception>
        /// <exception cref="TimeoutException">Thrown when ToolTimeout specifed and process is still working after this time.</exception>
        /// <exception cref="CakeException">Thrown when tool process ends with code different than <c>0</c>.</exception>
        public void New(MkDocsNewSettings settings)
        {
            var directoryArgument = _environment.WorkingDirectory.MakeAbsolute(_environment).FullPath;
            Run(settings, arguments => arguments.AppendQuoted(directoryArgument));
        }

        /// <summary>
        /// Create a new <c>MkDocs</c> project.
        /// </summary>
        /// <param name="projectDirectory">New project directory path.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        /// runner.New(new DirectoryPath("./project-with-docs-is-here"), new MkDocsNewSettings());
        /// </code>
        /// </example>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="projectDirectory"/> or <paramref name="settings"/> are not set.</exception>
        /// <exception cref="TimeoutException">Thrown when ToolTimeout specifed and process is still working after this time.</exception>
        /// <exception cref="CakeException">Thrown when tool process ends with code different than <c>0</c>.</exception>
        public void New(DirectoryPath projectDirectory, MkDocsNewSettings settings)
        {
            if (projectDirectory == null)
            {
                throw new ArgumentNullException(nameof(projectDirectory));
            }

            var directoryArgument = projectDirectory.MakeAbsolute(_environment).FullPath;
            Run(settings, arguments => arguments.AppendQuoted(directoryArgument));
        }
    }
}
