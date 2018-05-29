using Cake.Core.IO;
using Cake.MkDocs.Attributes;

namespace Cake.MkDocs.Build
{
    /// <summary>
    /// MkDocsBuildSettings
    /// </summary>
    [MkDocsCommand("build")]
    public sealed class MkDocsBuildSettings : MkDocsSettings
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
        /// Gets or sets a value indicating whether strict mode is enabled.
        /// <para>Enable strict mode. This will cause MkDocs to abort the build on any warnings.</para>
        /// <value><c>true</c> - enabled build in strict mode;</value>
        /// <value><c>false</c> - normal build;</value>
        /// </summary>
        [MkDocsArgument("strict", ShortArgument = "s")]
        public bool Strict { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether different theme should be used.
        /// <para>The theme to use when building your documentation.</para>
        /// <seealso href="https://www.mkdocs.org/user-guide/styling-your-docs/#built-in-themes">Themes description.</seealso>
        /// <value><c>null</c> - default theme; otherwise selected.</value>
        /// </summary>
        [MkDocsArgument("theme", ShortArgument = "t")]
        public MkDocsTheme? Theme { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether custom theme dir is provided.
        /// <para>The theme directory to use when building your documentation.</para>
        /// <value>Custom theme directory path;</value>
        /// </summary>
        [MkDocsArgument("theme-dir", ShortArgument = "e", Quoted = true)]
        public DirectoryPath ThemeDir { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether custom site dir is provided.
        /// <para>The directory to output the result of the documentation build.</para>
        /// <value>Output directory path;</value>
        /// </summary>
        [MkDocsArgument("site-dir", ShortArgument = "d", Quoted = true)]
        public DirectoryPath SiteDir { get; set; }
    }
}
