using Cake.Core.Tooling;
using Cake.MkDocs.Attributes;

namespace Cake.MkDocs
{
    /// <summary>
    /// Base MkDocs settings class.
    /// </summary>
    public abstract class MkDocsSettings : ToolSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether quiet mode is enabled.
        /// <para>Silence warnings.</para>
        /// <value><c>true</c> - enabled run without infos and warnings;</value>
        /// <value><c>false</c> - standard logging level (infos, warnings, errors);</value>
        /// </summary>
        [MkDocsArgument("quiet", ShortArgument = "q")]
        public bool Quiet { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether verbose mode is enabled.
        /// <para>Enable verbose output.</para>
        /// <value><c>true</c> - enabled run with debug information;</value>
        /// <value><c>false</c> - standard logging level;</value>
        /// </summary>
        [MkDocsArgument("verbose", ShortArgument = "v")]
        public bool Verbose { get; set; }
    }
}
