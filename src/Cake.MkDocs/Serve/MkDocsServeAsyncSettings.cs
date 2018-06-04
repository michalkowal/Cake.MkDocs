using System.Threading;
using Cake.MkDocs.Attributes;

namespace Cake.MkDocs.Serve
{
    /// <summary>
    /// Contains settings used by <see cref="MkDocsServeAsyncRunner"/>.
    /// </summary>
    [MkDocsCommand("serve")]
    public sealed class MkDocsServeAsyncSettings : MkDocsServeSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether cancellation token is defined for async operation.
        /// </summary>
        /// <value><c>CancellationToken.None</c> - default value</value>
        public CancellationToken Token { get; set; }
    }
}
