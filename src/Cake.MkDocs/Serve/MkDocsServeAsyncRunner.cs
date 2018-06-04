using System;
using System.Globalization;
using System.Threading.Tasks;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.MkDocs.Serve
{
    /// <summary>
    /// The <c>MkDocs</c> serve async tool buid and generate preview of <c>MkDocs</c> documentation.
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
        /// <example>
        /// <code>
        /// var tokenSource = new CancellationTokenSource();
        /// var task = runner.Serve(new MkDocsServeSettings() { Token = tokenSource.Token });
        ///
        /// // Do something...
        ///
        /// tokenSource.Cancel();
        ///
        /// try
        /// {
        ///     task.Wait();
        /// }
        /// catch (OperationCanceledException)
        /// {
        ///     // Handle excecption
        /// }
        /// </code>
        /// </example>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="settings"/> is not set.</exception>
        /// <exception cref="TimeoutException">Thrown when ToolTimeout specifed and process is still working after this time</exception>
        /// <exception cref="CakeException">Thrown when tool process ends with code different than <c>0</c></exception>
        /// <exception cref="OperationCanceledException">Thrown in a thread upon cancellation of an operation that the task was executing.</exception>
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
        /// <example>
        /// <code>
        /// var tokenSource = new CancellationTokenSource();
        /// var task = runner.Serve(new DirectoryPath("./project-with-docs-is-here"),
        ///     new MkDocsServeSettings() { Token = tokenSource.Token });
        ///
        /// // Do something...
        ///
        /// tokenSource.Cancel();
        ///
        /// try
        /// {
        ///     task.Wait();
        /// }
        /// catch (OperationCanceledException)
        /// {
        ///     // Handle excecption
        /// }
        /// </code>
        /// </example>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="projectDirectory"/> or <paramref name="settings"/> are not set.</exception>
        /// <exception cref="TimeoutException">Thrown when ToolTimeout specifed and process is still working after this time</exception>
        /// <exception cref="CakeException">Thrown when tool process ends with code different than <c>0</c></exception>
        /// <exception cref="OperationCanceledException">Thrown in a thread upon cancellation of an operation that the task was executing.</exception>
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
