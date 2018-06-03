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
			Assert.Equal("0.17.3", result.ToString());
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
			StartProcess("pip", new ProcessSettings() { Arguments = $"install mkdocs==0.16.3 --target \"{Paths.Temp.Combine("packages/")}\"" });
		}));
	
versionTasks.Add(
	Task("Should-Use-MkDocs-From-ToolPath")
		.IsDependentOn("Install-Older-Local-MkDocs")
		.Does(() =>
		{
			// Given
			var extension = Context.IsRunningOnWindows() ? ".exe" : "";
			var settings = new MkDocsVersionSettings()
			{
				ToolPath = Paths.Temp.CombineWithFilePath("packages/bin/mkdocs" + extension),
				EnvironmentVariables = new Dictionary<string, string>()
				{
					{ "PYTHONPATH", $"{Paths.Temp.Combine("packages")};{Paths.Temp.Combine("packages/bin")}" }
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
			var extension = Context.IsRunningOnWindows() ? ".exe" : "";
			var settings = new MkDocsVersionSettings()
			{
				ToolPath = Paths.Temp.CombineWithFilePath("packages/bin/mkdocs" + extension),
				EnvironmentVariables = new Dictionary<string, string>()
				{
					{ "PYTHONPATH", $"{Paths.Temp.Combine("packages")};{Paths.Temp.Combine("packages/bin")}" }
				}
			};
			
			// When
			var result = MkDocsIsSupportedVersion(settings);
			
			// Then
			Assert.False(result);
		})
		.Finally(() =>
		{
			Information("Remove local packages");
			DeleteDirectory(Paths.Temp.Combine("packages/"), new DeleteDirectorySettings() { Recursive = true });
		
			Information("Install global mkdocs");
			if (Context.IsRunningOnWindows())
				StartProcess("pip", new ProcessSettings() { Arguments = $"install mkdocs==0.17.3" });
			else
				StartProcess("sudo", new ProcessSettings() { Arguments = $"pip install mkdocs==0.17.3" });
		}));

Task("MkDocsVersion")
	.IsDependentOn("Should-Return-MkDocs-Version")
	.IsDependentOn("Should-Confirm-Supported-Version")
	.IsDependentOn("Should-Use-MkDocs-From-ToolPath")
	.IsDependentOn("Should-Reject-Local-MkDocs-Version");