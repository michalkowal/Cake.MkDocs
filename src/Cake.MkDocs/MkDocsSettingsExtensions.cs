using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Cake.MkDocs
{
    internal static class MkDocsSettingsExtensions
    {
        public static bool HasCommand(this MkDocsSettings settings)
        {
            return settings.GetType().GetCustomAttribute<MkDocsCommandAttribute>() != null;
        }

        public static string GetCommand(this MkDocsSettings settings)
        {
            string command = null;

            var commandAttribute = settings.GetType().GetCustomAttribute<MkDocsCommandAttribute>();
            if (commandAttribute != null)
            {
                command = commandAttribute.Command;
            }

            return command;
        }

        public static bool HasArguments(this MkDocsSettings settings)
        {
            return settings.GetType().GetCustomAttribute<MkDocsArgumentAttribute>() != null;
        }

        public static IEnumerable<string> GetArguments(this MkDocsSettings settings)
        {
            IEnumerable<string> arguments = Enumerable.Empty<string>();

            var argumentAttributes = settings.GetType().GetCustomAttributes<MkDocsArgumentAttribute>();
            var mkDocsArgumentAttributes = argumentAttributes as MkDocsArgumentAttribute[] ?? argumentAttributes.ToArray();
            if (mkDocsArgumentAttributes.Any())
            {
                arguments = mkDocsArgumentAttributes.Select(arg => "--" + arg.Argument).ToArray();
            }

            return arguments;
        }

        public static string GetArgumentsInline(this MkDocsSettings settings)
        {
            IEnumerable<string> arguments = settings.GetArguments();
            return string.Join(" ", arguments);
        }
    }
}
