// DupFinder exclude

using System;
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
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var runner = new MkDocsVersionRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            var result = runner.Version(settings);

            return result;
        }

        /// <summary>
        /// Check is provided MkDocs tool is in supported version.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>true - version is supported</returns>
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
        /// <returns>true - version is supported</returns>
        [CakeMethodAlias]
        [CakeAliasCategory("Version")]
        [CakeNamespaceImport("Cake.MkDocs.Version")]
        public static bool MkDocsIsSupportedVersion(this ICakeContext context, MkDocsVersionSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var runner = new MkDocsVersionRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            var result = runner.IsSupportedVersion(settings);

            return result;
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
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var runner = new MkDocsNewRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            runner.New(projectDirectory, settings);
        }

        /// <summary>
        /// Build the MkDocs documentation.
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
        /// Build the MkDocs documentation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">The settings.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("Build")]
        [CakeNamespaceImport("Cake.MkDocs.Build")]
        public static void MkDocsBuild(this ICakeContext context, MkDocsBuildSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var runner = new MkDocsBuildRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            runner.Build(settings);
        }

        /// <summary>
        /// Run the builtin development server.
        /// </summary>
        /// <param name="context">The context.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("Serve")]
        [CakeNamespaceImport("Cake.MkDocs.Serve")]
        public static void MkDocsServe(this ICakeContext context)
        {
            context.MkDocsServe(new MkDocsServeSettings());
        }

        /// <summary>
        /// Run the builtin development server.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">The settings.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("Serve")]
        [CakeNamespaceImport("Cake.MkDocs.Serve")]
        public static void MkDocsServe(this ICakeContext context, MkDocsServeSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var runner = new MkDocsServeRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            runner.Serve(settings);
        }

        /// <summary>
        /// Deploy your documentation to GitHub Pages.
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
        /// Deploy your documentation to GitHub Pages.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">The settings.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("GhDeploy")]
        [CakeNamespaceImport("Cake.MkDocs.GhDeploy")]
        public static void MkDocsGhDeploy(this ICakeContext context, MkDocsGhDeploySettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var runner = new MkDocsGhDeployRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            runner.GhDeploy(settings);
        }
    }
}
