using System;
using System.Linq;
using System.Text.RegularExpressions;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;
using Semver;

namespace Cake.MkDocs.Version
{
    /// <summary>
    /// The <c>MkDocs</c> version tool provides information about tool and addin versions compatibility.
    /// </summary>
    public sealed class MkDocsVersionRunner : MkDocsTool<MkDocsSettings>
    {
        private readonly Regex _versionRegex = new Regex("mkdocs, version (?<version>\\S*)");
        private static readonly SemVersion SupportedVersion = new SemVersion(1, 0, 4);

        /// <summary>
        /// Initializes a new instance of the <see cref="MkDocsVersionRunner"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="processRunner">The process runner.</param>
        /// <param name="tools">The tool locator.</param>
        public MkDocsVersionRunner(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner, IToolLocator tools)
            : base(fileSystem, environment, processRunner, tools)
        {
        }

        /// <summary>
        /// Gets provided <c>MkDocs</c> tool version.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>Provided <c>MkDocs</c> version.</returns>
        /// <example>
        /// <code>
        /// var mkDocsVersion = runner.Version(new MkDocsVersionSettings());
        /// Console.WriteLine(mkDocsVersion.ToString()); // 1.0.4
        /// </code>
        /// </example>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="settings"/> are not set.</exception>
        /// <exception cref="TimeoutException">Thrown when ToolTimeout specifed and process is still working after this time.</exception>
        /// <exception cref="CakeException">Thrown when tool process ends with code different than <c>0</c>.</exception>
        public System.Version Version(MkDocsSettings settings)
        {
            System.Version result = null;

            var processSettings = new ProcessSettings()
            {
                RedirectStandardOutput = true
            };

            string output = null;
            Run(settings, processSettings, process => output = process.GetStandardOutput()?.FirstOrDefault());

            if (!string.IsNullOrEmpty(output))
            {
                Match match = _versionRegex.Match(output);
                if (match.Success && match.Groups["version"].Success)
                {
                    try
                    {
                        var semVer = SemVersion.Parse(match.Groups["version"].Value);
                        result = new System.Version(semVer.Major, semVer.Minor, semVer.Patch);
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }

            return result ?? throw new CakeException("Invalid version.");
        }

        /// <summary>
        /// Checks if provided <c>MkDocs</c> tool version is compatible with addin.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns><c>true</c> - <c>MkDocs</c> version is compatible with addin; otherwise, <c>false</c>.</returns>
        /// <example>
        /// <code>
        /// bool isSupported = runner.IsSupportedVersion(new MkDocsNewSettings() { ToolPath = "./path-to-local-tool/bin/mkdocs" });
        /// </code>
        /// </example>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="settings"/> are not set.</exception>
        /// <exception cref="TimeoutException">Thrown when ToolTimeout specifed and process is still working after this time.</exception>
        /// <exception cref="CakeException">Thrown when tool process ends with code different than <c>0</c>.</exception>
        public bool IsSupportedVersion(MkDocsSettings settings)
        {
            System.Version toolVersion = Version(settings);

            return toolVersion.Major == SupportedVersion.Major &&
                   toolVersion.Minor >= SupportedVersion.Minor;
        }
    }
}
