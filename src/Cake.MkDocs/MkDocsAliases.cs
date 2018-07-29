using System;
using System.Threading;
using System.Threading.Tasks;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;
using Cake.MkDocs.Build;
using Cake.MkDocs.GhDeploy;
using Cake.MkDocs.New;
using Cake.MkDocs.Serve;
using Cake.MkDocs.Version;

namespace Cake.MkDocs
{
    /// <summary>
    /// Contains functionalities for working with <c>MkDocs</c>.
    /// <para>Contains functionality related to <c>MkDocs</c>.</para>
    /// <para>
    /// In order to use the commands for this alias, include the following in your build.cake file to download and
    /// install from <c>NuGet.org</c>, or specify the ToolPath within the correct ToolSettings class:
    /// <code>
    /// #addin "Cake.MkDocs"
    /// </code>
    /// <code>
    /// #addin "nuget:?package=Cake.MkDocs"
    /// </code>
    /// </para>
    /// </summary>
    /// <remarks>
    /// <para>See <a href="https://www.nuget.org/packages/Cake.MkDocs/">NuGet.org</a></para>
    /// <para>See <a href="https://github.com/mkdocs/mkdocs">MkDocs repository</a></para>
    /// </remarks>
    [CakeAliasCategory("MkDocs")]
    public static class MkDocsAliases
    {
        /// <summary>
        /// Show the <c>MkDocs</c> version.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns><c>MkDocs</c> tool version in <see cref="System.Version"/> format.</returns>
        /// <example>
        /// <code>
        /// var mkDocsVersion = MkDocsVersion();
        /// Information($"MkDocs tool version: {mkDocsVersion}");
        /// </code>
        /// </example>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="context"/> is not set.</exception>
        /// <exception cref="CakeException">Thrown when tool process ends with code different than <c>0</c></exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Version")]
        [CakeNamespaceImport("Cake.MkDocs.Version")]
        public static System.Version MkDocsVersion(this ICakeContext context)
        {
            return MkDocsVersion(context, new MkDocsVersionSettings());
        }

        /// <summary>
        /// Show the <c>MkDocs</c> version.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">The settings.</param>
        /// <returns><c>MkDocs</c> tool version in <see cref="System.Version"/> format.</returns>
        /// <example>
        /// <code>
        /// var mkDocsVersion = MkDocsVersion(new MkDocsVersionSettings()
        /// {
        ///     Quiet = true
        /// });
        /// Information($"MkDocs tool version: {mkDocsVersion}"); // 0.17.5
        /// </code>
        /// </example>
        /// <example>
        /// <code>
        /// var mkDocsVersion = MkDocsVersion(new MkDocsVersionSettings()
        /// {
        ///     ToolPath = "./path-to-local-tool/bin/mkdocs"
        /// });
        /// Information($"MkDocs tool version: {mkDocsVersion}"); // e.g. - 0.16.0
        /// </code>
        /// </example>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="context"/> or <paramref name="settings"/> are not set.</exception>
        /// <exception cref="TimeoutException">Thrown when ToolTimeout specifed and process is still working after this time</exception>
        /// <exception cref="CakeException">Thrown when tool process ends with code different than <c>0</c></exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Version")]
        [CakeNamespaceImport("Cake.MkDocs.Version")]
        public static System.Version MkDocsVersion(this ICakeContext context, MkDocsVersionSettings settings)
        {
            var runner = MkDocsRunnerFactory.CreateVersionRunner(context);
            var result = runner.Version(settings);

            return result;
        }

        /// <summary>
        /// Check is provided <c>MkDocs</c> tool is in supported version.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns><c>true</c> - version is supported; otherwise, <c>false</c>.</returns>
        /// <example>
        /// <code>
        /// if (!MkDocsIsSupportedVersion())
        ///     throw new Exception("Installed unsupported MkDocs version");
        /// </code>
        /// </example>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="context"/> is not set.</exception>
        /// <exception cref="CakeException">Thrown when tool process ends with code different than <c>0</c></exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Version")]
        [CakeNamespaceImport("Cake.MkDocs.Version")]
        public static bool MkDocsIsSupportedVersion(this ICakeContext context)
        {
            return MkDocsIsSupportedVersion(context, new MkDocsVersionSettings());
        }

        /// <summary>
        /// Check is provided <c>MkDocs</c> tool is in supported version.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">settings</param>
        /// <returns><c>true</c> - version is supported; otherwise, <c>false</c>.</returns>
        /// <example>
        /// <code>
        /// bool isMkDocsSupported = MkDocsIsSupportedVersion(new MkDocsVersionSettings()
        /// {
        ///     Quiet = true
        /// });
        /// if (!isMkDocsSupported)
        ///     throw new Exception("Installed unsupported MkDocs version");
        /// </code>
        /// </example>
        /// <example>
        /// <code>
        /// bool isSupported = MkDocsIsSupportedVersion(new MkDocsVersionSettings()
        /// {
        ///     ToolPath = "./path-to-local-tool/bin/mkdocs"
        /// });
        /// Information($"Is provided MkDocs version supported: {isSupported}");
        /// </code>
        /// </example>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="context"/> or <paramref name="settings"/> are not set.</exception>
        /// <exception cref="TimeoutException">Thrown when ToolTimeout specifed and process is still working after this time</exception>
        /// <exception cref="CakeException">Thrown when tool process ends with code different than <c>0</c></exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Version")]
        [CakeNamespaceImport("Cake.MkDocs.Version")]
        public static bool MkDocsIsSupportedVersion(this ICakeContext context, MkDocsVersionSettings settings)
        {
            var runner = MkDocsRunnerFactory.CreateVersionRunner(context);
            var result = runner.IsSupportedVersion(settings);

            return result;
        }

        /// <summary>
        /// Create a new <c>MkDocs</c> project in working directory.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <example>
        /// <code>
        /// MkDocsNew();
        /// </code>
        /// </example>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="context"/> is not set.</exception>
        /// <exception cref="CakeException">Thrown when tool process ends with code different than <c>0</c></exception>
        [CakeMethodAlias]
        [CakeAliasCategory("New")]
        [CakeNamespaceImport("Cake.MkDocs.New")]
        public static void MkDocsNew(this ICakeContext context)
        {
            context.MkDocsNew(new MkDocsNewSettings());
        }

        /// <summary>
        /// Create a new <c>MkDocs</c> project in working directory.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        /// MkDocsNew(new MkDocsNewSettings()
        /// {
        ///     Verbose = true
        /// });
        /// </code>
        /// </example>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="context"/> or <paramref name="settings"/> are not set.</exception>
        /// <exception cref="TimeoutException">Thrown when ToolTimeout specifed and process is still working after this time</exception>
        /// <exception cref="CakeException">Thrown when tool process ends with code different than <c>0</c></exception>
        [CakeMethodAlias]
        [CakeAliasCategory("New")]
        [CakeNamespaceImport("Cake.MkDocs.New")]
        public static void MkDocsNew(this ICakeContext context, MkDocsNewSettings settings)
        {
            var runner = MkDocsRunnerFactory.CreateNewRunner(context);
            runner.New(settings);
        }

        /// <summary>
        /// Create a new <c>MkDocs</c> project.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="projectDirectory">New project directory path.</param>
        /// <example>
        /// <code>
        /// MkDocsNew("./docs-project");
        /// </code>
        /// </example>
        /// <example>
        /// <code>
        /// MkDocsNew(new DirectoryPath("./docs-project"));
        /// </code>
        /// </example>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="context"/> or <paramref name="projectDirectory"/> are not set.</exception>
        /// <exception cref="CakeException">Thrown when tool process ends with code different than <c>0</c></exception>
        [CakeMethodAlias]
        [CakeAliasCategory("New")]
        [CakeNamespaceImport("Cake.MkDocs.New")]
        public static void MkDocsNew(this ICakeContext context, DirectoryPath projectDirectory)
        {
            context.MkDocsNew(projectDirectory, new MkDocsNewSettings());
        }

        /// <summary>
        /// Create a new <c>MkDocs</c> project.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="projectDirectory">New project directory path.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        /// MkDocsNew("./docs-project", new MkDocsNewSettings()
        /// {
        ///     Verbose = true
        /// });
        /// </code>
        /// </example>
        /// <example>
        /// <code>
        /// MkDocsNew(new DirectoryPath("./docs-project") ,new MkDocsNewSettings()
        /// {
        ///     Verbose = true
        /// });
        /// </code>
        /// </example>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="context"/>, <paramref name="projectDirectory"/> or <paramref name="settings"/> are not set.</exception>
        /// <exception cref="TimeoutException">Thrown when ToolTimeout specifed and process is still working after this time</exception>
        /// <exception cref="CakeException">Thrown when tool process ends with code different than <c>0</c></exception>
        [CakeMethodAlias]
        [CakeAliasCategory("New")]
        [CakeNamespaceImport("Cake.MkDocs.New")]
        public static void MkDocsNew(this ICakeContext context, DirectoryPath projectDirectory, MkDocsNewSettings settings)
        {
            var runner = MkDocsRunnerFactory.CreateNewRunner(context);
            runner.New(projectDirectory, settings);
        }

        /// <summary>
        /// Build the <c>MkDocs</c> documentation in working directory.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <example>
        /// <code>
        /// MkDocsBuild();
        /// </code>
        /// </example>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="context"/> is not set.</exception>
        /// <exception cref="CakeException">Thrown when tool process ends with code different than <c>0</c></exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Build")]
        [CakeNamespaceImport("Cake.MkDocs.Build")]
        public static void MkDocsBuild(this ICakeContext context)
        {
            context.MkDocsBuild(new MkDocsBuildSettings());
        }

        /// <summary>
        /// Build the <c>MkDocs</c> documentation in working directory.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        /// MkDocsBuild(new MkDocsBuildSettings()
        /// {
        ///     Dirty = true,
        ///     Theme = MkDocsTheme.ReadTheDocs
        /// });
        /// </code>
        /// </example>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="context"/> or <paramref name="settings"/> are not set.</exception>
        /// <exception cref="TimeoutException">Thrown when ToolTimeout specifed and process is still working after this time</exception>
        /// <exception cref="CakeException">Thrown when tool process ends with code different than <c>0</c></exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Build")]
        [CakeNamespaceImport("Cake.MkDocs.Build")]
        public static void MkDocsBuild(this ICakeContext context, MkDocsBuildSettings settings)
        {
            var runner = MkDocsRunnerFactory.CreateBuildRunner(context);
            runner.Build(settings);
        }

        /// <summary>
        /// Build the <c>MkDocs</c> documentation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="projectDirectory">Project directory path to build.</param>
        /// <example>
        /// <code>
        /// MkDocsBuild("./docs-project");
        /// </code>
        /// </example>
        /// <example>
        /// <code>
        /// MkDocsBuild(new DirectoryPath("./docs-project"));
        /// </code>
        /// </example>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="context"/> or <paramref name="projectDirectory"/> are not set.</exception>
        /// <exception cref="CakeException">Thrown when tool process ends with code different than <c>0</c></exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Build")]
        [CakeNamespaceImport("Cake.MkDocs.Build")]
        public static void MkDocsBuild(this ICakeContext context, DirectoryPath projectDirectory)
        {
            context.MkDocsBuild(projectDirectory, new MkDocsBuildSettings());
        }

        /// <summary>
        /// Build the <c>MkDocs</c> documentation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="projectDirectory">Project directory path to build.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        /// MkDocsBuild("./docs-project", new MkDocsBuildSettings()
        /// {
        ///     Dirty = true,
        ///     Theme = MkDocsTheme.ReadTheDocs
        /// });
        /// </code>
        /// </example>
        /// <example>
        /// <code>
        /// MkDocsBuild(new DirectoryPath("./docs-project"), new MkDocsBuildSettings()
        /// {
        ///     Dirty = true,
        ///     Theme = MkDocsTheme.ReadTheDocs
        /// });
        /// </code>
        /// </example>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="context"/>, <paramref name="projectDirectory"/> or <paramref name="settings"/> are not set.</exception>
        /// <exception cref="TimeoutException">Thrown when ToolTimeout specifed and process is still working after this time</exception>
        /// <exception cref="CakeException">Thrown when tool process ends with code different than <c>0</c></exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Build")]
        [CakeNamespaceImport("Cake.MkDocs.Build")]
        public static void MkDocsBuild(this ICakeContext context, DirectoryPath projectDirectory, MkDocsBuildSettings settings)
        {
            var runner = MkDocsRunnerFactory.CreateBuildRunner(context);
            runner.Build(projectDirectory, settings);
        }

        /// <summary>
        /// Run the builtin development server async in working directory.
        /// </summary>
        /// <remarks>
        /// <para>This method will block build process. Use <c>Ctrl+C</c> in console to quit.</para>
        /// </remarks>
        /// <param name="context">The context.</param>
        /// <example>
        /// <code>
        /// MkDocsServe();
        /// </code>
        /// </example>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="context"/> is not set.</exception>
        /// <exception cref="CakeException">Thrown when tool process ends with code different than <c>0</c></exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Serve")]
        [CakeNamespaceImport("Cake.MkDocs.Serve")]
        public static void MkDocsServe(this ICakeContext context)
        {
            context.MkDocsServe(new MkDocsServeSettings());
        }

        /// <summary>
        /// Run the builtin development server async in working directory.
        /// </summary>
        /// <remarks>
        /// <para>This method will block build process. Use <c>Ctrl+C</c> in console to quit.</para>
        /// </remarks>
        /// <param name="context">The context.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        /// MkDocsServe(new MkDocsServeSettings()
        /// {
        ///     DevAddr = new MkDocsAddress("localhost", 8090),
        ///     Theme = MkDocsTheme.ReadTheDocs
        /// });
        /// </code>
        /// </example>
        /// <example>
        /// <code>
        /// try
        /// {
        ///     MkDocsServe(new MkDocsServeSettings()
        ///     {
        ///         ToolTimeout = new TimeSpan(0, 0, 1, 0)
        ///     });
        /// }
        /// catch (TimeoutException)
        /// {
        ///     // Kill tool process after 1 minute
        /// }
        /// </code>
        /// </example>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="context"/> or <paramref name="settings"/> are not set.</exception>
        /// <exception cref="TimeoutException">Thrown when ToolTimeout specifed and process is still working after this time</exception>
        /// <exception cref="CakeException">Thrown when tool process ends with code different than <c>0</c></exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Serve")]
        [CakeNamespaceImport("Cake.MkDocs.Serve")]
        public static void MkDocsServe(this ICakeContext context, MkDocsServeSettings settings)
        {
            var runner = MkDocsRunnerFactory.CreateServeRunner(context);
            runner.Serve(settings);
        }

        /// <summary>
        /// Run the builtin development server.
        /// </summary>
        /// <remarks>
        /// <para>This method will block build process. Use <c>Ctrl+C</c> in console to quit.</para>
        /// </remarks>
        /// <param name="context">The context.</param>
        /// <param name="projectDirectory">Project directory path to serve.</param>
        /// <example>
        /// <code>
        /// MkDocsServe("./docs-project");
        /// </code>
        /// </example>
        /// <example>
        /// <code>
        /// MkDocsServe(new DirectoryPath("./docs-project"));
        /// </code>
        /// </example>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="context"/> or <paramref name="projectDirectory"/> are not set.</exception>
        /// <exception cref="CakeException">Thrown when tool process ends with code different than <c>0</c></exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Serve")]
        [CakeNamespaceImport("Cake.MkDocs.Serve")]
        public static void MkDocsServe(this ICakeContext context, DirectoryPath projectDirectory)
        {
            context.MkDocsServe(projectDirectory, new MkDocsServeSettings());
        }

        /// <summary>
        /// Run the builtin development server.
        /// </summary>
        /// <remarks>
        /// <para>This method will block build process. Use <c>Ctrl+C</c> in console to quit.</para>
        /// </remarks>
        /// <param name="context">The context.</param>
        /// <param name="projectDirectory">Project directory path to serve.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        /// MkDocsServe("./docs-project", new MkDocsServeSettings()
        /// {
        ///     DevAddr = new MkDocsAddress("localhost", 8090),
        ///     Theme = MkDocsTheme.ReadTheDocs
        /// });
        /// </code>
        /// </example>
        /// <example>
        /// <code>
        /// MkDocsServe(new DirectoryPath("./docs-project"), new MkDocsServeSettings()
        /// {
        ///     DevAddr = new MkDocsAddress("localhost", 8090),
        ///     Theme = MkDocsTheme.ReadTheDocs
        /// });
        /// </code>
        /// </example>
        /// <example>
        /// <code>
        /// try
        /// {
        ///     MkDocsServe(new DirectoryPath("./docs-project"), new MkDocsServeSettings()
        ///     {
        ///         ToolTimeout = new TimeSpan(0, 0, 1, 0)
        ///     });
        /// }
        /// catch (TimeoutException)
        /// {
        ///     // Kill tool process after 1 minute
        /// }
        /// </code>
        /// </example>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="context"/>, <paramref name="projectDirectory"/> or <paramref name="settings"/> are not set.</exception>
        /// <exception cref="TimeoutException">Thrown when ToolTimeout specifed and process is still working after this time</exception>
        /// <exception cref="CakeException">Thrown when tool process ends with code different than <c>0</c></exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Serve")]
        [CakeNamespaceImport("Cake.MkDocs.Serve")]
        public static void MkDocsServe(this ICakeContext context, DirectoryPath projectDirectory, MkDocsServeSettings settings)
        {
            var runner = MkDocsRunnerFactory.CreateServeRunner(context);
            runner.Serve(projectDirectory, settings);
        }

        /// <summary>
        /// Run the builtin development server async in working directory.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method will block build process. Use <c>Ctrl+C</c> in console to quit
        /// or use <see cref="CancellationToken"/> to cancel task programmatically.
        /// </para>
        /// </remarks>
        /// <param name="context">The context.</param>
        /// <returns>Long running task.</returns>
        /// <example>
        /// <code>
        /// var task = MkDocsServeAsync();
        /// // Do work...
        /// task.Wait();
        /// </code>
        /// </example>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="context"/> is not set.</exception>
        /// <exception cref="CakeException">Thrown when tool process ends with code different than <c>0</c></exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Serve")]
        [CakeNamespaceImport("Cake.MkDocs.Serve")]
        public static Task MkDocsServeAsync(this ICakeContext context)
        {
            return context.MkDocsServeAsync(new MkDocsServeAsyncSettings());
        }

        /// <summary>
        /// Run the builtin development server async in working directory.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method will block build process. Use <c>Ctrl+C</c> in console to quit
        /// or use <see cref="CancellationToken"/> to cancel task programmatically.
        /// </para>
        /// </remarks>
        /// <param name="context">The context.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>Long running task.</returns>
        /// <example>
        /// <code>
        /// using (var tokenSource = new CancellationTokenSource())
        /// {
        ///     var task = MkDocsServeAsync(new MkDocsServeAsyncSettings()
        ///     {
        ///         Token = tokenSource.Token
        ///     });
        ///
        ///     // Do work...
        ///     tokenSource.Cancel();
        ///
        ///     try
        ///     {
        ///         task.Wait();
        ///     }
        ///     catch (OperationCanceledException)
        ///     {
        ///     }
        /// }
        /// </code>
        /// </example>
        /// <example>
        /// <code>
        /// using (var tokenSource = new CancellationTokenSource())
        /// {
        ///     var task = MkDocsServeAsync(new MkDocsServeAsyncSettings()
        ///     {
        ///         ToolTimeout = new TimeSpan(0, 0, 1, 0)
        ///     });
        ///
        ///     // Do work...
        ///
        ///     try
        ///     {
        ///         task.Wait();
        ///     }
        ///     catch (TimeoutException)
        ///     {
        ///         // Kill tool process after 1 minute
        ///     }
        /// }
        /// </code>
        /// </example>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="context"/> or <paramref name="settings"/> are not set.</exception>
        /// <exception cref="TimeoutException">Thrown when ToolTimeout specifed and process is still working after this time</exception>
        /// <exception cref="CakeException">Thrown when tool process ends with code different than <c>0</c></exception>
        /// <exception cref="OperationCanceledException">Thrown in a thread upon cancellation of an operation that the task was executing.</exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Serve")]
        [CakeNamespaceImport("Cake.MkDocs.Serve")]
        public static Task MkDocsServeAsync(this ICakeContext context, MkDocsServeAsyncSettings settings)
        {
            var runner = MkDocsRunnerFactory.CreateServeAsyncRunner(context);
            return runner.ServeAsync(settings);
        }

        /// <summary>
        /// Run the builtin development server async.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method will block build process. Use <c>Ctrl+C</c> in console to quit
        /// or use <see cref="CancellationToken"/> to cancel task programmatically.
        /// </para>
        /// </remarks>
        /// <param name="context">The context.</param>
        /// <param name="projectDirectory">Project directory path to serve.</param>
        /// <returns>Long running task.</returns>
        /// <example>
        /// <code>
        /// var task = MkDocsServeAsync("./docs-project");
        /// // Do work...
        /// task.Wait();
        /// </code>
        /// </example>
        /// <example>
        /// <code>
        /// var task = MkDocsServeAsync(new DirectoryPath("./docs-project"));
        /// // Do work...
        /// task.Wait();
        /// </code>
        /// </example>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="context"/> or <paramref name="projectDirectory"/> are not set.</exception>
        /// <exception cref="CakeException">Thrown when tool process ends with code different than <c>0</c></exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Serve")]
        [CakeNamespaceImport("Cake.MkDocs.Serve")]
        public static Task MkDocsServeAsync(this ICakeContext context, DirectoryPath projectDirectory)
        {
            return context.MkDocsServeAsync(projectDirectory, new MkDocsServeAsyncSettings());
        }

        /// <summary>
        /// Run the builtin development server async.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method will block build process. Use <c>Ctrl+C</c> in console to quit
        /// or use <see cref="CancellationToken"/> to cancel task programmatically.
        /// </para>
        /// </remarks>
        /// <param name="context">The context.</param>
        /// <param name="projectDirectory">Project directory path to serve.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>Long running task.</returns>
        /// <example>
        /// <code>
        /// using (var tokenSource = new CancellationTokenSource())
        /// {
        ///     var task = MkDocsServeAsync("./docs-project", new MkDocsServeAsyncSettings()
        ///     {
        ///         Token = tokenSource.Token
        ///     });
        ///
        ///     // Do work...
        ///     tokenSource.Cancel();
        ///
        ///     try
        ///     {
        ///         task.Wait();
        ///     }
        ///     catch (OperationCanceledException)
        ///     {
        ///     }
        /// }
        /// </code>
        /// </example>
        /// <example>
        /// <code>
        /// using (var tokenSource = new CancellationTokenSource())
        /// {
        ///     var task = MkDocsServeAsync(new DirectoryPath("./docs-project"), new MkDocsServeAsyncSettings()
        ///     {
        ///         Token = tokenSource.Token
        ///     });
        ///
        ///     // Do work...
        ///     tokenSource.Cancel();
        ///
        ///     try
        ///     {
        ///         task.Wait();
        ///     }
        ///     catch (OperationCanceledException)
        ///     {
        ///     }
        /// }
        /// </code>
        /// </example>
        /// <example>
        /// <code>
        /// using (var tokenSource = new CancellationTokenSource())
        /// {
        ///     var task = MkDocsServeAsync(new DirectoryPath("./docs-project"), new MkDocsServeAsyncSettings()
        ///     {
        ///         ToolTimeout = new TimeSpan(0, 0, 1, 0)
        ///     });
        ///
        ///     // Do work...
        ///
        ///     try
        ///     {
        ///         task.Wait();
        ///     }
        ///     catch (TimeoutException)
        ///     {
        ///         // Kill tool process after 1 minute
        ///     }
        /// }
        /// </code>
        /// </example>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="context"/>, <paramref name="projectDirectory"/> or <paramref name="settings"/> are not set.</exception>
        /// <exception cref="TimeoutException">Thrown when ToolTimeout specifed and process is still working after this time</exception>
        /// <exception cref="CakeException">Thrown when tool process ends with code different than <c>0</c></exception>
        /// <exception cref="OperationCanceledException">Thrown in a thread upon cancellation of an operation that the task was executing.</exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Serve")]
        [CakeNamespaceImport("Cake.MkDocs.Serve")]
        public static Task MkDocsServeAsync(this ICakeContext context, DirectoryPath projectDirectory, MkDocsServeAsyncSettings settings)
        {
            var runner = MkDocsRunnerFactory.CreateServeAsyncRunner(context);
            return runner.ServeAsync(projectDirectory, settings);
        }

        /// <summary>
        /// Deploy your documentation to <c>GitHub Pages</c> (project in working directory).
        /// </summary>
        /// <remarks>
        /// <para>See <a href="https://pages.github.com/">GitHub Pages</a>.</para>
        /// <para>See <a href="https://www.mkdocs.org/user-guide/deploying-your-docs/#github-pages">MkDocs guide</a>.</para>
        /// </remarks>
        /// <param name="context">The context.</param>
        /// <example>
        /// <code>
        /// MkDocsGhDeploy();
        /// </code>
        /// </example>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="context"/> is not set.</exception>
        /// <exception cref="CakeException">Thrown when tool process ends with code different than <c>0</c></exception>
        [CakeMethodAlias]
        [CakeAliasCategory("GhDeploy")]
        [CakeNamespaceImport("Cake.MkDocs.GhDeploy")]
        public static void MkDocsGhDeploy(this ICakeContext context)
        {
            context.MkDocsGhDeploy(new MkDocsGhDeploySettings());
        }

        /// <summary>
        /// Deploy your documentation to <c>GitHub Pages</c> (project in working directory).
        /// </summary>
        /// <remarks>
        /// <para>See <a href="https://pages.github.com/">GitHub Pages</a>.</para>
        /// <para>See <a href="https://www.mkdocs.org/user-guide/deploying-your-docs/#github-pages">MkDocs guide</a>.</para>
        /// </remarks>
        /// <param name="context">The context.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        /// MkDocsGhDeploy(new MkDocsGhDeploySettings()
        /// {
        ///     RemoteBranch = "pages-branch",
        ///     RemoteName = "second-origin"
        /// });
        /// </code>
        /// </example>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="context"/> or <paramref name="settings"/> are not set.</exception>
        /// <exception cref="TimeoutException">Thrown when ToolTimeout specifed and process is still working after this time</exception>
        /// <exception cref="CakeException">Thrown when tool process ends with code different than <c>0</c></exception>
        [CakeMethodAlias]
        [CakeAliasCategory("GhDeploy")]
        [CakeNamespaceImport("Cake.MkDocs.GhDeploy")]
        public static void MkDocsGhDeploy(this ICakeContext context, MkDocsGhDeploySettings settings)
        {
            var runner = MkDocsRunnerFactory.CreateGhDeployRunner(context);
            runner.GhDeploy(settings);
        }

        /// <summary>
        /// Deploy your documentation to <c>GitHub Pages</c>.
        /// </summary>
        /// <remarks>
        /// <para>See <a href="https://pages.github.com/">GitHub Pages</a>.</para>
        /// <para>See <a href="https://www.mkdocs.org/user-guide/deploying-your-docs/#github-pages">MkDocs guide</a>.</para>
        /// </remarks>
        /// <param name="context">The context.</param>
        /// <param name="projectDirectory">Project directory path to deploy.</param>
        /// <example>
        /// <code>
        /// MkDocsGhDeploy("./docs-project";
        /// </code>
        /// </example>
        /// <example>
        /// <code>
        /// MkDocsGhDeploy(new DictionaryPath("./docs-project"));
        /// </code>
        /// </example>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="context"/> or <paramref name="projectDirectory"/> are not set.</exception>
        /// <exception cref="CakeException">Thrown when tool process ends with code different than <c>0</c></exception>
        [CakeMethodAlias]
        [CakeAliasCategory("GhDeploy")]
        [CakeNamespaceImport("Cake.MkDocs.GhDeploy")]
        public static void MkDocsGhDeploy(this ICakeContext context, DirectoryPath projectDirectory)
        {
            context.MkDocsGhDeploy(projectDirectory, new MkDocsGhDeploySettings());
        }

        /// <summary>
        /// Deploy your documentation to <c>GitHub Pages</c>.
        /// </summary>
        /// <remarks>
        /// <para>See <a href="https://pages.github.com/">GitHub Pages</a>.</para>
        /// <para>See <a href="https://www.mkdocs.org/user-guide/deploying-your-docs/#github-pages">MkDocs guide</a>.</para>
        /// </remarks>
        /// <param name="context">The context.</param>
        /// <param name="projectDirectory">Project directory path to deploy.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        /// MkDocsGhDeploy("./docs-project", new MkDocsGhDeploySettings()
        /// {
        ///     RemoteBranch = "pages-branch",
        ///     RemoteName = "second-origin"
        /// });
        /// </code>
        /// </example>
        /// <example>
        /// <code>
        /// MkDocsGhDeploy(new DictionaryPath("./docs-project"), new MkDocsGhDeploySettings()
        /// {
        ///     RemoteBranch = "pages-branch",
        ///     RemoteName = "second-origin"
        /// });
        /// </code>
        /// </example>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="context"/>, <paramref name="projectDirectory"/> or <paramref name="settings"/> are not set.</exception>
        /// <exception cref="TimeoutException">Thrown when ToolTimeout specifed and process is still working after this time</exception>
        /// <exception cref="CakeException">Thrown when tool process ends with code different than <c>0</c></exception>
        [CakeMethodAlias]
        [CakeAliasCategory("GhDeploy")]
        [CakeNamespaceImport("Cake.MkDocs.GhDeploy")]
        public static void MkDocsGhDeploy(this ICakeContext context, DirectoryPath projectDirectory, MkDocsGhDeploySettings settings)
        {
            var runner = MkDocsRunnerFactory.CreateGhDeployRunner(context);
            runner.GhDeploy(projectDirectory, settings);
        }
    }
}
