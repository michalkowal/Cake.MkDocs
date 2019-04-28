using System;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.MkDocs.GhDeploy
{
    /// <summary>
    /// The <c>MkDocs</c> gh-deploy tool deploying created site to <c>GitHub Pages</c>.
    /// </summary>
    /// <remarks>
    /// <para>See <a href="https://pages.github.com/">GitHub Pages</a>.</para>
    /// <para>See <a href="https://www.mkdocs.org/user-guide/deploying-your-docs/#github-pages">MkDocs guide</a>.</para>
    /// </remarks>
    public sealed class MkDocsGhDeployRunner : MkDocsTool<MkDocsGhDeploySettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MkDocsGhDeployRunner"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="processRunner">The process runner.</param>
        /// <param name="tools">The tool locator.</param>
        public MkDocsGhDeployRunner(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner,
            IToolLocator tools)
            : base(fileSystem, environment, processRunner, tools)
        {
        }

        /// <summary>
        /// Deploy your documentation to <c>GitHub Pages</c> (project in working directory).
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        /// runner.GhDeploy(new MkDocsGhDeploySettings());
        /// </code>
        /// </example>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="settings"/> is not set.</exception>
        /// <exception cref="TimeoutException">Thrown when ToolTimeout specifed and process is still working after this time.</exception>
        /// <exception cref="CakeException">Thrown when tool process ends with code different than <c>0</c>.</exception>
        public void GhDeploy(MkDocsGhDeploySettings settings)
        {
            Run(settings);
        }

        /// <summary>
        /// Deploy your documentation to <c>GitHub Pages</c>.
        /// </summary>
        /// <param name="projectDirectory">Project dir to deploy.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        /// runner.GhDeploy(new DirectoryPath("./project-with-docs-is-here"), new MkDocsGhDeploySettings());
        /// </code>
        /// </example>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="projectDirectory"/> or <paramref name="settings"/> are not set.</exception>
        /// <exception cref="TimeoutException">Thrown when ToolTimeout specifed and process is still working after this time.</exception>
        /// <exception cref="CakeException">Thrown when tool process ends with code different than <c>0</c>.</exception>
        public void GhDeploy(DirectoryPath projectDirectory, MkDocsGhDeploySettings settings)
        {
            Run(settings, projectDirectory);
        }
    }
}
