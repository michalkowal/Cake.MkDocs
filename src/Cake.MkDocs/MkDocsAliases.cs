using System;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;
using Cake.MkDocs.Build;
using Cake.MkDocs.GhDeploy;
using Cake.MkDocs.New;
using Cake.MkDocs.Serve;

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
        }
    }
}
