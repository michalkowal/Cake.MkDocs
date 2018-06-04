using System;
using System.Collections.Generic;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.MkDocs
{
    /// <summary>
    /// Base class for all <c>MkDocs</c> related tools.
    /// </summary>
    /// <typeparam name="TSettings">the settings type.</typeparam>
    public abstract class MkDocsTool<TSettings> : Tool<TSettings>
        where TSettings : MkDocsSettings
    {
        private readonly ICakeEnvironment _environment;

        /// <summary>
        /// Initializes a new instance of the <see cref="MkDocsTool{TSettings}"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="processRunner">The process runner.</param>
        /// <param name="tools">The tool locator.</param>
        protected MkDocsTool(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner, IToolLocator tools)
            : base(fileSystem, environment, processRunner, tools)
        {
            _environment = environment;
        }

        /// <summary>
        /// Runs the tool using the specified settings.
        /// Creates process arguments based on settings attribute.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="setCommandValues">If specified called during process argument building.</param>
        /// <example>
        /// <code>
        /// // MkDocsNewSettings are related to "new" process command
        /// var settings = new MkDocsNewSettings();
        /// Run(settings, arguments => arguments.Append("./project-directory"));
        /// </code>
        /// </example>
        protected void Run(TSettings settings, Action<ProcessArgumentBuilder> setCommandValues = null)
        {
            Run(settings, new ProcessSettings(), null, setCommandValues);
        }

        /// <summary>
        /// Runs the tool using the specified settings and change process workig directory.
        /// Creates process arguments based on settings attribute.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="projectDirectory">Process working directory.</param>
        /// <param name="setCommandValues">If specified called during process argument building.</param>
        /// <example>
        /// <code>
        /// // MkDocsBuildSettings are related to "build" process command
        /// var settings = new MkDocsBuildSettings();
        /// Run(settings, new DirectoryPath("./project-with-docs-is-here"));
        /// </code>
        /// </example>
        protected void Run(TSettings settings, DirectoryPath projectDirectory, Action<ProcessArgumentBuilder> setCommandValues = null)
        {
            if (projectDirectory == null)
            {
                throw new ArgumentNullException(nameof(projectDirectory));
            }

            var processSettings = new ProcessSettings()
            {
                WorkingDirectory = projectDirectory.MakeAbsolute(_environment)
            };

            Run(settings, processSettings, null, setCommandValues);
        }

        /// <summary>
        /// Runs the tool using a custom tool path and the specified settings.
        /// Creates process arguments based on settings attribute.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="processSettings">The process settings.</param>
        /// <param name="postAction">If specified called after process exit.</param>
        /// <param name="setCommandValues">If specified called during process argument building.</param>
        /// <example>
        /// <code>
        /// // MkDocsBuildSettings are related to "build" process command
        /// var settings = new MkDocsBuildSettings();
        /// IEnumerable&lt;string&gt; output = null;
        /// Run(settings, new DirectoryPath("./project-with-docs-is-here"), process => output = process.GetStandardOutput());
        /// </code>
        /// </example>
        protected void Run(TSettings settings, ProcessSettings processSettings, Action<IProcess> postAction, Action<ProcessArgumentBuilder> setCommandValues = null)
        {
            ProcessArgumentBuilder arguments = BuildArguments(settings, setCommandValues);
            Run(settings, arguments, processSettings, postAction);
        }

        /// <summary>
        /// Builds process arguments based on settings
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="setCommandValues">If specified called during process argument building.</param>
        /// <returns><see cref="ProcessArgumentBuilder"/> with all commands and arguments for process.</returns>
        /// <example>
        /// <code>
        /// // MkDocsNewSettings are related to "new" process command
        /// var settings = new MkDocsNewSettings()
        /// {
        ///     Quiet = true
        /// };
        /// var argumentBuilder = BuildArguments(settings, arguments => arguments.Append("./project-directory"));
        ///
        /// // Generate arguments: "new ./project-directory --quiet"
        /// </code>
        /// </example>
        protected ProcessArgumentBuilder BuildArguments(TSettings settings, Action<ProcessArgumentBuilder> setCommandValues = null)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            var arguments = new ProcessArgumentBuilder();
            if (settings.HasCommand())
            {
                arguments.Append(settings.GetCommand());
                setCommandValues?.Invoke(arguments);
            }

            var settingsArguments = settings.GetArgumentsInline(_environment);
            if (!string.IsNullOrWhiteSpace(settingsArguments))
            {
                arguments.Append(settingsArguments);
            }

            if (settings.HasFixedArguments())
            {
                arguments.Append(settings.GetFixedArgumentsInline());
            }

            return arguments;
        }

        /// <summary>
        /// Gets the name of the tool.
        /// </summary>
        /// <returns>The name of the tool (<c>MkDocs</c>).</returns>
        protected override string GetToolName()
        {
            return "MkDocs";
        }

        /// <summary>
        /// Gets the possible names of the tool executable.
        /// </summary>
        /// <returns>The tool executable name (<c>mkdocs</c>, <c>mkdocs.exe</c>).</returns>
        protected override IEnumerable<string> GetToolExecutableNames()
        {
            yield return "mkdocs";
            yield return "mkdocs.exe";
        }
    }
}
