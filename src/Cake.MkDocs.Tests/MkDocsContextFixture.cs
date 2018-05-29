using Cake.Core;
using Cake.Core.Configuration;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Core.Tooling;
using Cake.Testing;

namespace Cake.MkDocs.Tests
{
    internal sealed class FakeProcessRunner : IProcessRunner
    {
        private readonly IProcess _process;

        public FakeProcessRunner(IProcess process)
        {
            _process = process;
        }

        public IProcess Start(FilePath filePath, ProcessSettings settings)
        {
            return _process;
        }
    }

    public sealed class MkDocsContextFixture : ICakeContext
    {
        public MkDocsContextFixture()
            : this(new FakeProcess())
        {
        }

        public MkDocsContextFixture(IProcess process)
        {
            ProcessRunner = new FakeProcessRunner(process);
            Environment = FakeEnvironment.CreateUnixEnvironment();
            FileSystem = new FakeFileSystem(Environment);
            Globber = new Globber(FileSystem, Environment);
            Configuration = new FakeConfiguration();
            Tools = new ToolLocator(Environment, new ToolRepository(Environment), new ToolResolutionStrategy(FileSystem, Environment, Globber, Configuration));

            DefaultToolPath = new FilePath("./tools/mkdocs.exe").MakeAbsolute(Environment);
            ((FakeFileSystem)FileSystem).CreateFile(DefaultToolPath);
        }

        public IFileSystem FileSystem { get; internal set; }
        public ICakeEnvironment Environment { get; internal set; }
        public IGlobber Globber { get; internal set; }
        public ICakeLog Log { get; internal set; }
        public ICakeArguments Arguments { get; internal set; }
        public IProcessRunner ProcessRunner { get; internal set; }
        public IRegistry Registry { get; internal set; }
        public IToolLocator Tools { get; internal set; }

        public ICakeConfiguration Configuration { get; internal set; }
        public FilePath DefaultToolPath { get; internal set; }
    }
}
