using Cake.MkDocs.Attributes;

namespace Cake.MkDocs
{
    /// <summary>
    /// Represents <c>MkDocs</c> themes.
    /// </summary>
    public enum MkDocsTheme
    {
        /// <summary>
        /// Theme: <c>MkDocs</c>
        /// </summary>
        [MkDocsArgumentValue("mkdocs")]
        MkDocs,

        /// <summary>
        /// Theme: <c>ReadTheDocs</c>
        /// </summary>
        [MkDocsArgumentValue("readthedocs")]
        ReadTheDocs
    }
}
