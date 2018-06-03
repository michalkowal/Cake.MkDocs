using System;
using System.Collections.Generic;
using Cake.Core.IO;
using Cake.Testing.Fixtures;

namespace Cake.MkDocs.Tests.Fixtures
{
    public sealed class ToolFixtureProcessLongRunner : IProcessRunner
    {
        private readonly Func<FilePath, ProcessSettings, ToolFixtureResult> _factory;
        private readonly List<ToolFixtureResult> _results;

        public LongRunningFakeProcess Process { get; set; }

        public IReadOnlyList<ToolFixtureResult> Results => _results;

        internal ToolFixtureProcessLongRunner(Func<FilePath, ProcessSettings, ToolFixtureResult> factory)
        {
            _factory = factory;
            _results = new List<ToolFixtureResult>();

            Process = new LongRunningFakeProcess();
        }

        public IProcess Start(FilePath filePath, ProcessSettings settings)
        {
            // Invoke the intercept action.
            _results.Add(_factory(filePath, settings));

            // Return a dummy result.
            return Process;
        }
    }
}
