using Cake.Core.IO;
using Cake.MkDocs.Attributes;

namespace Cake.MkDocs.Serve
{
    /// <summary>
    /// Contains settings used by <see cref="MkDocsServeRunner"/>.
    /// </summary>
    [MkDocsCommand("serve")]
    public class MkDocsServeSettings : MkDocsSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether a specific config is provided.
        /// <para>Provide a specific MkDocs config.</para>
        /// </summary>
        /// <value>Configuration file path and name;</value>
        [MkDocsArgument("config-file", ShortArgument = "f", Quoted = true)]
        public FilePath ConfigFile { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether custom dev address is provided.
        /// <para>IP address and port to serve documentation locally (default localhost:8000).</para>
        /// </summary>
        /// <value>Custom IP address and port; otherwise - <c>localhost:8000</c>;</value>
        [MkDocsArgument("dev-addr", ShortArgument = "a")]
        public MkDocsAddress DevAddr { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether strict mode is enabled.
        /// <para>Enable strict mode. This will cause MkDocs to abort the build on any warnings.</para>
        /// </summary>
        /// <value><c>true</c> - enabled build in strict mode;</value>
        /// <value><c>false</c> - normal build;</value>
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
        /// </summary>
        /// <value>Custom theme directory path;</value>
        [MkDocsArgument("theme-dir", ShortArgument = "e", Quoted = true)]
        public DirectoryPath ThemeDir { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether live reloading is enabled.
        /// <para>Enable the live reloading in the development server (this is the default).</para>
        /// </summary>
        /// <value><c>true</c> - enabled live reload mode; otherwise - <c>false</c></value>
        [MkDocsArgument("livereload")]
        public bool LiveReload { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether live reloading is disabled.
        /// <para>Disable the live reloading in the development server.</para>
        /// </summary>
        /// <value><c>true</c> - disabled live reload mode; otherwise - <c>false</c></value>
        [MkDocsArgument("no-livereload")]
        public bool NoLiveReload { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether live reloading is enabled.
        /// <para>Enable the live reloading in the development server, but only re-build files that have changed.</para>
        /// </summary>
        /// <value><c>true</c> - enabled dirty reload mode; otherwise - <c>false</c></value>
        [MkDocsArgument("dirtyreload")]
        public bool DirtyReload { get; set; }
    }
}
