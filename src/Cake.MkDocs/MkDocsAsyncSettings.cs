using System.Threading;

namespace Cake.MkDocs
{
    /// <summary>
    /// Base MkDocs settings class for async operation.
    /// </summary>
    public abstract class MkDocsAsyncSettings : MkDocsSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether cancellation token is defined for async operation.
        /// <value><c>CancellationToken.None</c> - default value</value>
        /// </summary>
        public CancellationToken Token { get; set; }
    }
}
