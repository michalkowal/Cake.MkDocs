using System;

namespace Cake.MkDocs.Attributes
{
    /// <summary>
    /// Describes tool argument related to settings <see cref="MkDocsSettings"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
    public sealed class MkDocsArgumentAttribute : Attribute
    {
        /// <summary>
        /// Prefix of argument
        /// </summary>
        public const string ArgumentPrefix = "--";

        /// <summary>
        /// Prefix of short version argument
        /// </summary>
        public const string ShortArgumentPrefix = "-";

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

        /// <summary>
        /// Gets or sets a value indicating whether argument should be quoted.
        /// </summary>
        public bool Quoted { get; set; }
    }
}
