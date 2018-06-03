using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cake.Core.IO;
using Cake.Core.Tooling;
using Cake.MkDocs.Serve;
using Cake.Testing;
using Cake.Testing.Fixtures;

namespace Cake.MkDocs.Tests.Fixtures.Serve
{
    public abstract class BaseMkDocsServeAsyncFixture
    {
        public const string ToolFileName = "mkdocs";
        private bool _handleOperationCanceledException = true;

        public FakeFileSystem FileSystem { get; set; }

        public ToolFixtureProcessLongRunner ProcessRunner { get; set; }

        public FakeEnvironment Environment { get; set; }

        public IGlobber Globber { get; set; }

        public FakeConfiguration Configuration { get; set; }

        public IToolLocator Tools { get; set; }

        public MkDocsServeAsyncSettings Settings { get; set; }

        public FilePath DefaultToolPath { get; }

        protected BaseMkDocsServeAsyncFixture()
        {
            Settings = new MkDocsServeAsyncSettings();
            ProcessRunner = new ToolFixtureProcessLongRunner(CreateResult);
            Environment = FakeEnvironment.CreateUnixEnvironment();
            FileSystem = new FakeFileSystem(Environment);
            Globber = new Globber(FileSystem, Environment);
            Configuration = new FakeConfiguration();
            Tools = new ToolLocator(Environment, new ToolRepository(Environment),
                new ToolResolutionStrategy(FileSystem, Environment, Globber, Configuration));

            // ReSharper disable once VirtualMemberCallInConstructor
            DefaultToolPath = GetDefaultToolPath(ToolFileName);
            FileSystem.CreateFile(DefaultToolPath);

            Settings.Token = new CancellationToken(true);
        }

        protected virtual FilePath GetDefaultToolPath(string toolFilename)
        {
            return new FilePath("./tools/" + toolFilename).MakeAbsolute(Environment);
        }

        public async Task<ToolFixtureResult> Run()
        {
            try
            {
                // Run the tool.
                await RunTool();
            }
            catch (OperationCanceledException) when (_handleOperationCanceledException)
            {
            }

            // Returned the intercepted result.
            return ProcessRunner.Results.LastOrDefault();
        }

        protected virtual ToolFixtureResult CreateResult(FilePath path, ProcessSettings process)
        {
            return new ToolFixtureResult(path, process);
        }

        public void GivenThrowOperationCanceledException()
        {
            _handleOperationCanceledException = false;
        }

        protected abstract Task RunTool();
    }
}
