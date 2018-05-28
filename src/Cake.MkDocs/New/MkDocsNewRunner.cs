using System;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.MkDocs.New
{
    /// <summary>
    /// The MkDocs new tool creates a new MkDocs project.
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
        /// Create a new MkDocs project.
        /// </summary>
        /// <param name="projectDirectory">New project directory path.</param>
        /// <param name="settings">The settings</param>
        public void New(DirectoryPath projectDirectory, MkDocsNewSettings settings)
        {
            if (projectDirectory == null)
            {
                throw new ArgumentNullException(nameof(projectDirectory));
            }

            Run(settings, arguments => arguments.AppendQuoted(projectDirectory.MakeAbsolute(_environment).FullPath));
        }
    }
}
