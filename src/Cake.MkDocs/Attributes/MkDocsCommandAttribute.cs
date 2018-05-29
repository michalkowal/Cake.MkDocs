using System;

namespace Cake.MkDocs.Attributes
{
    /// <summary>
    /// Describes tool commands related to settings <see cref="MkDocsSettings"/>
    /// </summary>
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
