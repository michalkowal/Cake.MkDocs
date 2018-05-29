using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cake.Core;
using Cake.Core.IO;
using Cake.MkDocs.Attributes;

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

        public static bool HasFixedArguments(this MkDocsSettings settings)
        {
            return settings.GetType().GetCustomAttribute<MkDocsArgumentAttribute>() != null;
        }

        public static IEnumerable<string> GetFixedArguments(this MkDocsSettings settings)
        {
            IEnumerable<string> arguments = Enumerable.Empty<string>();

            var argumentAttributes = settings.GetType().GetCustomAttributes<MkDocsArgumentAttribute>();
            var mkDocsArgumentAttributes = argumentAttributes as MkDocsArgumentAttribute[] ?? argumentAttributes.ToArray();
            if (mkDocsArgumentAttributes.Any())
            {
                arguments = mkDocsArgumentAttributes.Select(arg => MkDocsArgumentAttribute.ArgumentPrefix + arg.Argument).ToArray();
            }

            return arguments;
        }

        public static string GetFixedArgumentsInline(this MkDocsSettings settings)
        {
            IEnumerable<string> arguments = settings.GetFixedArguments();
            return string.Join(" ", arguments);
        }

        public static IEnumerable<KeyValuePair<string, string>> GetArguments(this MkDocsSettings settings, ICakeEnvironment environment)
        {
            Dictionary<string, string> arguments = new Dictionary<string, string>();

            var properties = settings
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(pi => pi.GetCustomAttributes<MkDocsArgumentAttribute>().Any())
                .ToArray();

            foreach (var pi in properties)
            {
                var argumentAttribute = pi.GetCustomAttributes<MkDocsArgumentAttribute>().First();
                var argument = MkDocsArgumentAttribute.ArgumentPrefix + argumentAttribute.Argument;
                bool addArgument = false;
                string argumentValue = null;

                object value = pi.GetValue(settings);
                if (value != null)
                {
                    addArgument = true;
                    if (pi.PropertyType == typeof(bool) || pi.PropertyType == typeof(bool?))
                    {
                        addArgument = (bool)value;
                    }
                    else if (pi.PropertyType.IsEnum ||
                             (pi.PropertyType.IsGenericType && pi.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) && pi.PropertyType.GenericTypeArguments.First().IsEnum))
                    {
                        var propertyType = !pi.PropertyType.IsGenericType
                            ? pi.PropertyType
                            : pi.PropertyType.GenericTypeArguments.First();

                        var argumentValueAttribute = propertyType
                            .GetMember(value.ToString())
                            .FirstOrDefault()
                            ?.GetCustomAttribute<MkDocsArgumentValueAttribute>();
                        argumentValue = argumentValueAttribute?.Value ?? value.ToString();
                    }
                    else if (typeof(DirectoryPath).IsAssignableFrom(pi.PropertyType) ||
                             typeof(FilePath).IsAssignableFrom(pi.PropertyType))
                    {
                        string path = pi.PropertyType == typeof(DirectoryPath)
                            ? ((DirectoryPath)value).MakeAbsolute(environment).FullPath
                            : ((FilePath)value).MakeAbsolute(environment).FullPath;
                        argumentValue = $"{path}";
                    }
                    else
                    {
                        argumentValue = value.ToString();
                    }
                }

                if (addArgument)
                {
                    if (argumentAttribute.Quoted)
                    {
                        argumentValue = $"\"{argumentValue}\"";
                    }
                    arguments.Add(argument, argumentValue);
                }
            }

            return arguments;
        }

        public static string GetArgumentsInline(this MkDocsSettings settings, ICakeEnvironment environment)
        {
            IEnumerable<string> arguments = settings
                .GetArguments(environment)
                .Select(kv => $"{kv.Key} {kv.Value}");
            return string.Join(" ", arguments);
        }
    }
}
