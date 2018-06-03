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
    /// Contains functionalities for working with MkDocs CLI.
    /// <para>Contains functionality related to <see href="https://github.com/mkdocs/mkdocs">MkDocs</see>.</para>
    /// <para>
    /// In order to use the commands for this alias, include the following in your build.cake file to download and
    /// install from NuGet.org, or specify the ToolPath within the correct ToolSettings class:
    /// <code>
    /// #addin nuget:?package=Cake.MkDocs
    /// </code>
    /// </para>
    /// </summary>
    [CakeAliasCategory("MkDocs")]
    public static class MkDocsAliases
    {
        /// <summary>
        /// Show the MkDocs version.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>MkDocs tool version.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory("Version")]
        [CakeNamespaceImport("Cake.MkDocs.Version")]
        public static System.Version MkDocsVersion(this ICakeContext context)
        {
            return MkDocsVersion(context, new MkDocsVersionSettings());
        }

        /// <summary>
        /// Show the MkDocs version.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>MkDocs tool version.</returns>
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
        /// Check is provided MkDocs tool is in supported version.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns><c>true</c> - version is supported; otherwise, <c>false</c>.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory("Version")]
        [CakeNamespaceImport("Cake.MkDocs.Version")]
        public static bool MkDocsIsSupportedVersion(this ICakeContext context)
        {
            return MkDocsIsSupportedVersion(context, new MkDocsVersionSettings());
        }

        /// <summary>
        /// Check is provided MkDocs tool is in supported version.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">settings</param>
        /// <returns><c>true</c> - version is supported; otherwise, <c>false</c>.</returns>
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
        /// Create a new MkDocs project in working directory.
        /// </summary>
        /// <param name="context">The context.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("New")]
        [CakeNamespaceImport("Cake.MkDocs.New")]
        public static void MkDocsNew(this ICakeContext context)
        {
            context.MkDocsNew(new MkDocsNewSettings());
        }

        /// <summary>
        /// Create a new MkDocs project in working directory.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">The settings.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("New")]
        [CakeNamespaceImport("Cake.MkDocs.New")]
        public static void MkDocsNew(this ICakeContext context, MkDocsNewSettings settings)
        {
            var runner = MkDocsRunnerFactory.CreateNewRunner(context);
            runner.New(settings);
        }

        /// <summary>
        /// Create a new MkDocs project.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="projectDirectory">New project directory path.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("New")]
        [CakeNamespaceImport("Cake.MkDocs.New")]
        public static void MkDocsNew(this ICakeContext context, DirectoryPath projectDirectory)
        {
            context.MkDocsNew(projectDirectory, new MkDocsNewSettings());
        }

        /// <summary>
        /// Create a new MkDocs project.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="projectDirectory">New project directory path.</param>
        /// <param name="settings">The settings.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("New")]
        [CakeNamespaceImport("Cake.MkDocs.New")]
        public static void MkDocsNew(this ICakeContext context, DirectoryPath projectDirectory, MkDocsNewSettings settings)
        {
            var runner = MkDocsRunnerFactory.CreateNewRunner(context);
            runner.New(projectDirectory, settings);
        }

        /// <summary>
        /// Build the MkDocs documentation in working directory.
        /// </summary>
        /// <param name="context">The context.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("Build")]
        [CakeNamespaceImport("Cake.MkDocs.Build")]
        public static void MkDocsBuild(this ICakeContext context)
        {
            context.MkDocsBuild(new MkDocsBuildSettings());
        }

        /// <summary>
        /// Build the MkDocs documentation in working directory.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">The settings.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("Build")]
        [CakeNamespaceImport("Cake.MkDocs.Build")]
        public static void MkDocsBuild(this ICakeContext context, MkDocsBuildSettings settings)
        {
            var runner = MkDocsRunnerFactory.CreateBuildRunner(context);
            runner.Build(settings);
        }

        /// <summary>
        /// Build the MkDocs documentation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="projectDirectory">Project directory path to build.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("Build")]
        [CakeNamespaceImport("Cake.MkDocs.Build")]
        public static void MkDocsBuild(this ICakeContext context, DirectoryPath projectDirectory)
        {
            context.MkDocsBuild(projectDirectory, new MkDocsBuildSettings());
        }

        /// <summary>
        /// Build the MkDocs documentation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="projectDirectory">Project directory path to build.</param>
        /// <param name="settings">The settings.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("Build")]
        [CakeNamespaceImport("Cake.MkDocs.Build")]
        public static void MkDocsBuild(this ICakeContext context, DirectoryPath projectDirectory, MkDocsBuildSettings settings)
        {
            var runner = MkDocsRunnerFactory.CreateBuildRunner(context);
            runner.Build(projectDirectory, settings);
        }

        /// <summary>
        /// Run the builtin development server in working directory.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Long running task.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory("Serve")]
        [CakeNamespaceImport("Cake.MkDocs.Serve")]
        public static Task MkDocsServe(this ICakeContext context)
        {
            return context.MkDocsServe(new MkDocsServeSettings());
        }

        /// <summary>
        /// Run the builtin development server in working directory.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>Long running task.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory("Serve")]
        [CakeNamespaceImport("Cake.MkDocs.Serve")]
        public static Task MkDocsServe(this ICakeContext context, MkDocsServeSettings settings)
        {
            var runner = MkDocsRunnerFactory.CreateServeRunner(context);
            return runner.Serve(settings);
        }

        /// <summary>
        /// Run the builtin development server.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="projectDirectory">Project directory path to serve.</param>
        /// <returns>Long running task.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory("Serve")]
        [CakeNamespaceImport("Cake.MkDocs.Serve")]
        public static Task MkDocsServe(this ICakeContext context, DirectoryPath projectDirectory)
        {
            return context.MkDocsServe(projectDirectory, new MkDocsServeSettings());
        }

        /// <summary>
        /// Run the builtin development server.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="projectDirectory">Project directory path to serve.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>Long running task.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory("Serve")]
        [CakeNamespaceImport("Cake.MkDocs.Serve")]
        public static Task MkDocsServe(this ICakeContext context, DirectoryPath projectDirectory, MkDocsServeSettings settings)
        {
            var runner = MkDocsRunnerFactory.CreateServeRunner(context);
            return runner.Serve(projectDirectory, settings);
        }

        /// <summary>
        /// Deploy your documentation to GitHub Pages (project in working directory).
        /// </summary>
        /// <param name="context">The context.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("GhDeploy")]
        [CakeNamespaceImport("Cake.MkDocs.GhDeploy")]
        public static void MkDocsGhDeploy(this ICakeContext context)
        {
            context.MkDocsGhDeploy(new MkDocsGhDeploySettings());
        }

        /// <summary>
        /// Deploy your documentation to GitHub Pages (project in working directory).
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">The settings.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("GhDeploy")]
        [CakeNamespaceImport("Cake.MkDocs.GhDeploy")]
        public static void MkDocsGhDeploy(this ICakeContext context, MkDocsGhDeploySettings settings)
        {
            var runner = MkDocsRunnerFactory.CreateGhDeployRunner(context);
            runner.GhDeploy(settings);
        }

        /// <summary>
        /// Deploy your documentation to GitHub Pages.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="projectDirectory">Project directory path to deploy.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("GhDeploy")]
        [CakeNamespaceImport("Cake.MkDocs.GhDeploy")]
        public static void MkDocsGhDeploy(this ICakeContext context, DirectoryPath projectDirectory)
        {
            context.MkDocsGhDeploy(projectDirectory, new MkDocsGhDeploySettings());
        }

        /// <summary>
        /// Deploy your documentation to GitHub Pages.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="projectDirectory">Project directory path to deploy.</param>
        /// <param name="settings">The settings.</param>
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
