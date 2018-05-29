using System;

namespace Cake.MkDocs.Attributes
{
    /// <summary>
    /// Describes tool argument named values related to settings <see cref="MkDocsSettings"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class MkDocsArgumentValueAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MkDocsArgumentValueAttribute"/> class.
        /// </summary>
        /// <param name="value">tool argument value.</param>
        public MkDocsArgumentValueAttribute(string value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets tool argument value
        /// </summary>
        public string Value { get; }
    }
}
