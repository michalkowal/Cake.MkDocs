using System;
using System.Collections.Generic;
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

    // ReSharper disable once ClassNeverInstantiated.Global
    internal sealed class FakeCakeDataService : ICakeDataService
    {
        private readonly Dictionary<Type, object> _data;

        public FakeCakeDataService()
        {
            _data = new Dictionary<Type, object>();
        }

        public TData Get<TData>()
            where TData : class
        {
            if (_data.TryGetValue(typeof(TData), out var data))
            {
                if (data is TData typedData)
                {
                    return typedData;
                }
                var message = $"Context data exists but is of the wrong type ({data.GetType().FullName}).";
                throw new InvalidOperationException(message);
            }
            throw new InvalidOperationException("The context data has not been setup.");
        }

        public void Add<TData>(TData value)
            where TData : class
        {
            if (_data.ContainsKey(typeof(TData)))
            {
                var message = $"Context data of type '{typeof(TData).FullName}' has already been registered.";
                throw new InvalidOperationException(message);
            }
            _data.Add(typeof(TData), value);
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
            Data = new FakeCakeDataService();

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
        public ICakeDataResolver Data { get; internal set; }

        public ICakeConfiguration Configuration { get; internal set; }
        public FilePath DefaultToolPath { get; internal set; }
    }
}
