using System;

namespace Cake.MkDocs
{
    /// <summary>
    /// Describes tool argument related to settings <see cref="MkDocsSettings"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
    public sealed class MkDocsArgumentAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MkDocsArgumentAttribute"/> class.
        /// </summary>
        /// <param name="argument">tool argument.</param>
        public MkDocsArgumentAttribute(string argument)
        {
            Argument = argument;
        }

        /// <summary>
        /// Gets tool argument
        /// </summary>
        public string Argument { get; }

        /// <summary>
        /// Gets or sets tool arguments (one letter version)
        /// </summary>
        public string ShortArgument { get; set; }
    }
}
