using System.Threading;
using Cake.MkDocs.Attributes;

namespace Cake.MkDocs.Serve
{
    /// <summary>
    /// MkDocsServeAsyncSettings
    /// </summary>
    [MkDocsCommand("serve")]
    public sealed class MkDocsServeAsyncSettings : MkDocsServeSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether cancellation token is defined for async operation.
        /// <value><c>CancellationToken.None</c> - default value</value>
        /// </summary>
        public CancellationToken Token { get; set; }
    }
}
