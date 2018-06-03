#load "utilities/paths.cake"

#load "MkDocsVersion.cake"
#load "MkDocsNew.cake"
#load "MkDocsBuild.cake"
#load "MkDocsServe.cake"
#load "MkDocsGhDeploy.cake"

TaskSetup((taskContext) =>
{
	if (serveTasks.Any(t => t.Task.Name == taskContext.Task.Name))
	{
		_taskData = new LongRunTaskContext();
	}
	else if (ghDeployTasks.Any(t => t.Task.Name == taskContext.Task.Name))
	{
		Unzip(Paths.Resources.CombineWithFilePath("git.zip"), Paths.Temp);
	}
});

TaskTeardown((taskContext) =>
{
	if (buildTasks.Any(t => t.Task.Name == taskContext.Task.Name))
	{
		if (DirectoryExists(Paths.Temp.Combine("site")))
		{
			DeleteDirectory(Paths.Temp.Combine("site"), new DeleteDirectorySettings() { Recursive = true } );
		}
	}
	else if (serveTasks.Any(t => t.Task.Name == taskContext.Task.Name))
	{
		if (_taskData != null)
		{
			try
			{
				if (!_taskData.CancellationSource.IsCancellationRequested)
				{
					_taskData.CancellationSource.Cancel();
				}
				if (_taskData.LongRunTask != null)
				{
					_taskData.LongRunTask.Wait();
				}
			}
			catch
			{
			}
			finally
			{
				_taskData.Dispose();
				_taskData = null;
			}
		}
	}
	else if (ghDeployTasks.Any(t => t.Task.Name == taskContext.Task.Name))
	{
		DeleteDirectory(Paths.Temp.Combine(".git"), new DeleteDirectorySettings() { Recursive = true, Force = true } );
		if (DirectoryExists(Paths.Temp.Combine("site")))
		{
			DeleteDirectory(Paths.Temp.Combine("site"), new DeleteDirectorySettings() { Recursive = true } );
		}
	}
});

Task("MkDocsAliases")
	.IsDependentOn("MkDocsVersion")
	.IsDependentOn("MkDocsNew")
	.IsDependentOn("MkDocsBuild")
	.IsDependentOn("MkDocsServe");
	.IsDependentOn("MkDocsGhDeploy");