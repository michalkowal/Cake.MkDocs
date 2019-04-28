using System;

namespace Cake.MkDocs.Attributes
{
    /// <summary>
    /// Describes tool argument named values related to settings <see cref="MkDocsSettings"/>.
    /// </summary>
    /// <example>
    /// <code>
    /// public enum SpecialTypes
    /// {
    ///     [MkDocsArgumentValue("not-special")]
    ///     NotSpecial,
    ///     [MkDocsArgumentValue("special")]
    ///     Special
    /// }
    /// [MkDocsCommand("do-special")]
    /// public class SpecialCommandSettings : MkDocsSettings
    /// {
    ///     [MkDocsArgument("special-name", "s")]
    ///     public SpecialTypes Name { get; set; }
    /// }
    ///
    /// var settings = new SpecialCommandSettings() { Name = SpecialTypes.NotSpecial };
    ///
    /// // Addin tool will execute process with arguments: "mkdocs do-special --special-name not-special"
    /// </code>
    /// </example>
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
        /// Gets tool argument value.
        /// </summary>
        public string Value { get; }
    }
}
