using System;
using System.Globalization;
using System.Threading.Tasks;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.MkDocs.Serve
{
    /// <summary>
    /// The MkDocs serve async tool creates a new MkDocs project.
    /// </summary>
    public sealed class MkDocsServeAsyncRunner : MkDocsTool<MkDocsServeAsyncSettings>
    {
        private readonly ICakeEnvironment _environment;

        /// <summary>
        /// Initializes a new instance of the <see cref="MkDocsServeAsyncRunner"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="processRunner">The process runner.</param>
        /// <param name="tools">The tool locator.</param>
        public MkDocsServeAsyncRunner(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner,
            IToolLocator tools)
            : base(fileSystem, environment, processRunner, tools)
        {
            _environment = environment;
        }

        /// <summary>
        /// Run the builtin development server in working directory async.
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <returns>Long running task.</returns>
        public Task ServeAsync(MkDocsServeAsyncSettings settings)
        {
            return RunAsync(settings);
        }

        /// <summary>
        /// Run the builtin development server async.
        /// </summary>
        /// <param name="projectDirectory">Project dir to serve.</param>
        /// <param name="settings">The settings</param>
        /// <returns>Long running task.</returns>
        public Task ServeAsync(DirectoryPath projectDirectory, MkDocsServeAsyncSettings settings)
        {
            return RunAsync(settings, projectDirectory);
        }

        /// <summary>
        /// Runs the tool in long running mode, using the specified settings.
        /// Creates process arguments based on settings attribute.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="setCommandValues">If specified called during process argument building.</param>
        /// <returns>Long running task.</returns>
        private Task RunAsync(MkDocsServeAsyncSettings settings, Action<ProcessArgumentBuilder> setCommandValues = null)
        {
            return RunAsync(settings, new ProcessSettings(), null, setCommandValues);
        }

        /// <summary>
        /// Runs the tool in long running mode, using the specified settings and change process workig directory.
        /// Creates process arguments based on settings attribute.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="projectDirectory">Process working directory.</param>
        /// <param name="setCommandValues">If specified called during process argument building.</param>
        /// <returns>Long running task.</returns>
        private Task RunAsync(MkDocsServeAsyncSettings settings, DirectoryPath projectDirectory, Action<ProcessArgumentBuilder> setCommandValues = null)
        {
            if (projectDirectory == null)
            {
                throw new ArgumentNullException(nameof(projectDirectory));
            }

            var processSettings = new ProcessSettings()
            {
                WorkingDirectory = projectDirectory.MakeAbsolute(_environment)
            };

            return RunAsync(settings, processSettings, null, setCommandValues);
        }

        /// <summary>
        /// Runs the tool in long running mode, using a custom tool path and the specified settings.
        /// Creates process arguments based on settings attribute.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="processSettings">The process settings.</param>
        /// <param name="postAction">If specified called after process exit.</param>
        /// <param name="setCommandValues">If specified called during process argument building.</param>
        /// <returns>Long running task.</returns>
        private Task RunAsync(
            MkDocsServeAsyncSettings settings,
            ProcessSettings processSettings,
            Action<IProcess> postAction,
            Action<ProcessArgumentBuilder> setCommandValues = null)
        {
            ProcessArgumentBuilder arguments = BuildArguments(settings, setCommandValues);
            var process = RunProcess(settings, arguments, processSettings);

            var task = Task.Factory.StartNew(() =>
            {
                settings.Token.Register(() =>
                {
                    process.Kill();
                });

                // Wait for the process to exit.
                if (settings.ToolTimeout.HasValue)
                {
                    if (!process.WaitForExit((int)settings.ToolTimeout.Value.TotalMilliseconds))
                    {
                        const string message = "Tool timeout ({0}): {1}";
                        throw new TimeoutException(string.Format(CultureInfo.InvariantCulture, message, settings.ToolTimeout.Value, GetToolName()));
                    }
                }
                else
                {
                    process.WaitForExit();
                }

                try
                {
                    settings.Token.ThrowIfCancellationRequested();
                    ProcessExitCode(process.GetExitCode());
                }
                finally
                {
                    // Post action specified?
                    postAction?.Invoke(process);
                }
            }, settings.Token);

            return task;
        }
    }
}
