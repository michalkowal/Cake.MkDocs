using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Core.Tooling;

internal sealed class OutputRedirectProcessRunnerDecorator : IProcessRunner
{
	private readonly IProcessRunner _parent;
	
	private KeyValuePair<ProcessSettings, IProcess> _lastProcess;

	public IEnumerable<string> LastProcessOutput
	{
		get
		{
			return !_lastProcess.Key.Silent && !_lastProcess.Key.RedirectStandardOutput
				? _lastProcess.Value.GetStandardOutput()
				: Enumerable.Empty<string>();
		}
	}
	public IEnumerable<string> LastProcessError
	{
		get
		{
			return !_lastProcess.Key.Silent && !_lastProcess.Key.RedirectStandardError
				? _lastProcess.Value.GetStandardError()
				: Enumerable.Empty<string>();
		}
	}

	public OutputRedirectProcessRunnerDecorator(IProcessRunner parent)
	{
		_parent = parent;
	}

	public IProcess Start(FilePath filePath, ProcessSettings settings)
	{
		ProcessSettings redirectSettings = settings;
		if (!settings.RedirectStandardOutput || !settings.RedirectStandardError)
		{
			redirectSettings = new ProcessSettings()
			{
				RedirectStandardError = true,
				RedirectStandardOutput = true,
				Silent = settings.Silent,
				Timeout = settings.Timeout,
				WorkingDirectory = settings.WorkingDirectory.FullPath
			};

			if (settings.Arguments != null)
			{
				redirectSettings.Arguments = new ProcessArgumentBuilder();
				settings.Arguments.CopyTo(redirectSettings.Arguments);
			}
			if (settings.EnvironmentVariables != null)
			{
				KeyValuePair<string, string>[] envVariables =
					new KeyValuePair<string, string>[settings.EnvironmentVariables.Count];
				settings.EnvironmentVariables.CopyTo(envVariables, 0);

				redirectSettings.EnvironmentVariables = envVariables.ToDictionary(k => k.Key, v => v.Value);
			}

		}

		IProcess process = _parent.Start(filePath, redirectSettings);

		_lastProcess = new KeyValuePair<ProcessSettings, IProcess>(settings, process);

		return process;
	}
}

public sealed class ConsoleOutputContextAdapter : ICakeContext
{
	private readonly OutputRedirectProcessRunnerDecorator _processRunner;
	private readonly CakeContext _cakeContext;
	private readonly List<string> _output = new List<string>();

	public ConsoleOutputContextAdapter(ICakeContext context)
	{
		_processRunner = new OutputRedirectProcessRunnerDecorator(context.ProcessRunner);
		_cakeContext = new CakeContext(context.FileSystem, context.Environment,
			context.Globber, context.Log, context.Arguments, 
			_processRunner, context.Registry, context.Tools);
	}

	public void CollectOutput()
	{
		if (_processRunner.LastProcessOutput != null)
		{
			var output = _processRunner.LastProcessOutput.ToList();
			_output.AddRange(output);
		}
		if (_processRunner.LastProcessError != null)
		{
			var error = _processRunner.LastProcessError.ToList();
			_output.AddRange(error);
		}
	}

	public IFileSystem FileSystem => _cakeContext.FileSystem;
	public ICakeEnvironment Environment => _cakeContext.Environment;
	public IGlobber Globber => _cakeContext.Globber;
	public ICakeLog Log => _cakeContext.Log;
	public ICakeArguments Arguments => _cakeContext.Arguments;
	public IProcessRunner ProcessRunner => _cakeContext.ProcessRunner;
	public IRegistry Registry => _cakeContext.Registry;
	public IToolLocator Tools => _cakeContext.Tools;
	
	public IEnumerable<string> Output => _output;
}

public sealed class LongRunTaskContext : IDisposable
{
	public CancellationTokenSource CancellationSource { get; }
	public Task LongRunTask { get; set; }

	public LongRunTaskContext()
	{
		CancellationSource = new CancellationTokenSource();
	}

	public void Dispose()
	{
		CancellationSource.Dispose();
	}
}
