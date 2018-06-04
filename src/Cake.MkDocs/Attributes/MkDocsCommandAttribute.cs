using System;

namespace Cake.MkDocs.Attributes
{
    /// <summary>
    /// Describes tool commands related to settings <see cref="MkDocsSettings"/>
    /// </summary>
    /// <example>
    /// <code>
    /// [MkDocsCommand("do-special")]
    /// public class SpecialCommandSettings : MkDocsSettings
    /// {
    /// }
    ///
    /// // Addin tool will execute process with arguments: "mkdocs do-special"
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Class)]
    internal sealed class MkDocsCommandAttribute : Attribute
    {
        /// <summary>
        /// Gets tool command
        /// </summary>
        public string Command { get; }

        public MkDocsCommandAttribute(string command)
        {
            Command = command;
        }
    }
}
