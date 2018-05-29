using Cake.Core.IO;
using Cake.MkDocs.Attributes;

namespace Cake.MkDocs.GhDeploy
{
    /// <summary>
    /// MkDocsGhDeploySettings
    /// </summary>
    [MkDocsCommand("gh-deploy")]
    public sealed class MkDocsGhDeploySettings : MkDocsSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether clean mode is enabled.
        /// <para>Remove old files from the site_dir before building (the default).</para>
        /// <value><c>true</c> - force clean mode build</value>
        /// <value><c>false</c> - default cleaning mode;</value>
        /// </summary>
        [MkDocsArgument("clean", ShortArgument = "c")]
        public bool Clean { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether dirty mode is enabled.
        /// <para>DO NOT remove old files from the site_dir before building.</para>
        /// <value><c>true</c> - force dirty mode build</value>
        /// <value><c>false</c> - default cleaning mode;</value>
        /// </summary>
        [MkDocsArgument("dirty")]
        public bool Dirty { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a specific config is provided.
        /// <para>Provide a specific MkDocs config.</para>
        /// <value>Configuration file path and name;</value>
        /// </summary>
        [MkDocsArgument("config-file", ShortArgument = "f", Quoted = true)]
        public FilePath ConfigFile { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a specific commit message is provided.
        /// <para>A commit message to use when commiting to the GitHub Pages remote branch.</para>
        /// <value>Commit message;</value>
        /// </summary>
        [MkDocsArgument("message", ShortArgument = "m", Quoted = true)]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a custom remote branch is provided.
        /// <para>The remote branch to commit to for GitHub Pages. This overrides the value specified in config.</para>
        /// <value>Remote branch;</value>
        /// </summary>
        [MkDocsArgument("remote-branch", ShortArgument = "b", Quoted = true)]
        public string RemoteBranch { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a custom remote name is provided.
        /// <para>The remote name to commit to for GitHub Pages. This overrides the value specified in config.</para>
        /// <value>Remote branch;</value>
        /// </summary>
        [MkDocsArgument("remote-name", ShortArgument = "r", Quoted = true)]
        public string RemoteName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether force push is required.
        /// <para>Force the push to the repository.</para>
        /// <value><c>true</c> - force push enabled; otherwise - <c>false</c></value>
        /// </summary>
        [MkDocsArgument("force")]
        public bool Force { get; set; }
    }
}
