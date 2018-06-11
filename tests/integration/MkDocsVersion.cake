#load "utilities/paths.cake"
#load "utilities/xunit.cake"

var versionTasks = new List<CakeTaskBuilder<ActionTask>>();

versionTasks.Add(
	Task("Should-Return-MkDocs-Version")
		.Does(() =>
		{
			// When
			var result = MkDocsVersion();
			
			// Then
			Assert.NotNull(result);
			Assert.Equal(mkDocsVersion, result.ToString());
		}));
	
versionTasks.Add(
	Task("Should-Confirm-Supported-Version")
		.Does(() =>
		{
			// When
			var result = MkDocsIsSupportedVersion();
			
			// Then
			Assert.True(result);
		}));
	
versionTasks.Add(
	Task("Install-Older-Local-MkDocs")
		.Does(() =>
		{
			Information("Uninstall global mkdocs");
			if (Context.IsRunningOnWindows())
				StartProcess("pip", new ProcessSettings() { Arguments = "uninstall mkdocs --y" });
			else
				StartProcess("sudo", new ProcessSettings() { Arguments = "pip uninstall mkdocs --y" });
			
			Information("Install local mkdocs");
			if (Context.IsRunningOnWindows())
				StartProcess("pip", new ProcessSettings() { Arguments = $"install mkdocs==0.16.3 --target \"{Paths.Temp.Combine("packages/")}\"" });
			else
				StartProcess("sudo", new ProcessSettings() { Arguments = $"pip install mkdocs==0.16.3 --prefix /usr/local" });
		}));
	
versionTasks.Add(
	Task("Should-Use-MkDocs-From-ToolPath")
		.IsDependentOn("Install-Older-Local-MkDocs")
		.Does(() =>
		{
			// Given
			var localMkdocs = Context.IsRunningOnWindows() ? Paths.Temp.CombineWithFilePath("packages/bin/mkdocs.exe") : new FilePath("/usr/local/bin/mkdocs");
			var pythonPath = (EnvironmentVariable("PYTHONPATH") ?? string.Empty);
			if (!string.IsNullOrEmpty(pythonPath))
			{
				pythonPath += Context.IsRunningOnWindows() ? ";" : ":";
			}
			pythonPath += Context.IsRunningOnWindows()
				? $"{Paths.Temp.Combine("packages")};{Paths.Temp.Combine("packages/bin")}"
				: $"{new DirectoryPath("/usr/local")};{new DirectoryPath("/usr/local/bin")}";
			
			var settings = new MkDocsVersionSettings()
			{
				ToolPath = localMkdocs,
				EnvironmentVariables = new Dictionary<string, string>()
				{
					{ "PYTHONPATH", pythonPath }
				}
			};
		
			// When
			var result = MkDocsVersion(settings);
			
			// Then
			Assert.NotNull(result);
			Assert.Equal("0.16.3", result.ToString());
		}));
	
versionTasks.Add(
	Task("Should-Reject-Local-MkDocs-Version")
		.Does(() =>
		{
			// Given
			var localMkdocs = Context.IsRunningOnWindows() ? Paths.Temp.CombineWithFilePath("packages/bin/mkdocs.exe") : new FilePath("/usr/local/bin/mkdocs");
			var pythonPath = (EnvironmentVariable("PYTHONPATH") ?? string.Empty);
			if (!string.IsNullOrEmpty(pythonPath))
			{
				pythonPath += Context.IsRunningOnWindows() ? ";" : ":";
			}
			pythonPath += Context.IsRunningOnWindows()
				? $"{Paths.Temp.Combine("packages")};{Paths.Temp.Combine("packages/bin")}"
				: $"{new DirectoryPath("/usr/local")};{new DirectoryPath("/usr/local/bin")}";
			
			var settings = new MkDocsVersionSettings()
			{
				ToolPath = localMkdocs,
				EnvironmentVariables = new Dictionary<string, string>()
				{
					{ "PYTHONPATH", pythonPath }
				}
			};
			
			// When
			var result = MkDocsIsSupportedVersion(settings);
			
			// Then
			Assert.False(result);
		})
		.Finally(() =>
		{
			if (Context.IsRunningOnWindows())
			{
				Information("Remove local packages");
				DeleteDirectory(Paths.Temp.Combine("packages/"), new DeleteDirectorySettings() { Recursive = true });
			}
			else
				StartProcess("sudo", new ProcessSettings() { Arguments = $"pip uninstall mkdocs --y" });
				
			Information("Install global mkdocs");
			if (Context.IsRunningOnWindows())
				StartProcess("pip", new ProcessSettings() { Arguments = $"install mkdocs=={mkDocsVersion}" });
			else
				StartProcess("sudo", new ProcessSettings() { Arguments = $"pip install mkdocs=={mkDocsVersion}" });
		}));

Task("MkDocsVersion")
	.IsDependentOn("Should-Return-MkDocs-Version")
	.IsDependentOn("Should-Confirm-Supported-Version")
	.IsDependentOn("Should-Use-MkDocs-From-ToolPath")
	.IsDependentOn("Should-Reject-Local-MkDocs-Version");