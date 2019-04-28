using Cake.Core.IO;
using Cake.MkDocs.Attributes;

namespace Cake.MkDocs.GhDeploy
{
    /// <summary>
    /// Contains settings used by <see cref="MkDocsGhDeployRunner"/>.
    /// </summary>
    /// <remarks>
    /// <para>See <a href="https://pages.github.com/">GitHub Pages</a>.</para>
    /// <para>See <a href="https://www.mkdocs.org/user-guide/deploying-your-docs/#github-pages">MkDocs guide</a>.</para>
    /// </remarks>
    [MkDocsCommand("gh-deploy")]
    public sealed class MkDocsGhDeploySettings : MkDocsSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether clean mode is enabled.
        /// <para>Remove old files from the site_dir before building (the default).</para>
        /// </summary>
        /// <value><c>true</c> - force clean mode build.</value>
        /// <value><c>false</c> - default cleaning mode;.</value>
        [MkDocsArgument("clean", ShortArgument = "c")]
        public bool Clean { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether dirty mode is enabled.
        /// <para>DO NOT remove old files from the site_dir before building.</para>
        /// </summary>
        /// <value><c>true</c> - force dirty mode build.</value>
        /// <value><c>false</c> - default cleaning mode;.</value>
        [MkDocsArgument("dirty")]
        public bool Dirty { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a specific config is provided.
        /// <para>Provide a specific <c>MkDocs</c> config.</para>
        /// </summary>
        /// <value>Configuration file path and name;.</value>
        [MkDocsArgument("config-file", ShortArgument = "f", Quoted = true)]
        public FilePath ConfigFile { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a specific commit message is provided.
        /// <para>A commit message to use when commiting to the <c>GitHub Pages</c> remote branch.</para>
        /// </summary>
        /// <value>Commit message;.</value>
        [MkDocsArgument("message", ShortArgument = "m", Quoted = true)]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a custom remote branch is provided.
        /// <para>The remote branch to commit to for <c>GitHub Pages</c>. This overrides the value specified in config.</para>
        /// </summary>
        /// <value>Remote branch;.</value>
        [MkDocsArgument("remote-branch", ShortArgument = "b", Quoted = true)]
        public string RemoteBranch { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a custom remote name is provided.
        /// <para>The remote name to commit to for <c>GitHub Pages</c>. This overrides the value specified in config.</para>
        /// </summary>
        /// <value>Remote branch;.</value>
        [MkDocsArgument("remote-name", ShortArgument = "r", Quoted = true)]
        public string RemoteName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether force push is required.
        /// <para>Force the push to the repository.</para>
        /// </summary>
        /// <value><c>true</c> - force push enabled; otherwise - <c>false</c>.</value>
        [MkDocsArgument("force")]
        public bool Force { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether ignore version push is required.
        /// <para>Ignore check that build is not being deployed
        /// with an older version of MkDocs.</para>
        /// </summary>
        /// <value><c>true</c> - ignore version push enabled; otherwise - <c>false</c>.</value>
        [MkDocsArgument("ignore-version")]
        public bool IgnoreVersion { get; set; }
    }
}
