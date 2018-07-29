#load "utilities/paths.cake"
#load "utilities/xunit.cake"

var buildTasks = new List<CakeTaskBuilder>();

buildTasks.Add(
	Task("Should-Build-Project")
		.IsDependentOn("Should-Create-New-Project")
		.Does(() =>
		{
			// When
			MkDocsBuild(Paths.Temp);
			
			// Then
			Assert.True(DirectoryExists(Paths.Temp.Combine("site/")));
			Assert.True(FileExists(Paths.Temp.CombineWithFilePath("site/index.html")));
		}));
	
buildTasks.Add(
	Task("Should-Remove-File-With-Clean")
		.IsDependentOn("Should-Create-New-Project")
		.Does(() =>
		{
			// Given
			CreateDirectory(Paths.Temp.Combine("site"));
			CopyFile(Paths.Resources.CombineWithFilePath("noop.txt"), Paths.Temp.CombineWithFilePath("site/noop.txt"));
		
			// When
			MkDocsBuild(Paths.Temp, new MkDocsBuildSettings() { Clean = true });
			
			// Then
			Assert.False(FileExists(Paths.Temp.CombineWithFilePath("site/noop.txt")));
		}));
	
buildTasks.Add(
	Task("Should-Not-Remove-File-With-Dirty")
		.IsDependentOn("Should-Create-New-Project")
		.Does(() =>
		{
			// Given
			CreateDirectory(Paths.Temp.Combine("site"));
			CopyFile(Paths.Resources.CombineWithFilePath("noop.txt"), Paths.Temp.CombineWithFilePath("site/noop.txt"));
		
			// When
			MkDocsBuild(Paths.Temp, new MkDocsBuildSettings() { Dirty = true });
			
			// Then
			Assert.True(FileExists(Paths.Temp.CombineWithFilePath("site/noop.txt")));
		}));
	
buildTasks.Add(
	Task("Should-Use-Config-From-Differet-Dir")
		.IsDependentOn("Should-Create-New-Project")
		.Does(() =>
		{
			// Given
			CreateDirectory(Paths.Temp.Combine("config/"));
			MoveFile(Paths.Temp.CombineWithFilePath("mkdocs.yml"), Paths.Temp.CombineWithFilePath("config/mkdocs.yml"));
		
			// When
			MkDocsBuild(Paths.Temp, new MkDocsBuildSettings() { ConfigFile = Paths.Temp.CombineWithFilePath("config/mkdocs.yml") });
			
			// Then
			Assert.True(DirectoryExists(Paths.Temp.Combine("site/")));
			Assert.True(FileExists(Paths.Temp.CombineWithFilePath("site/index.html")));
		})
		.Finally(() =>
		{
			MoveFile(Paths.Temp.CombineWithFilePath("config/mkdocs.yml"), Paths.Temp.CombineWithFilePath("mkdocs.yml"));
			DeleteDirectory(Paths.Temp.Combine("config/"), new DeleteDirectorySettings());
		}));
	
buildTasks.Add(
	Task("Should-Use-ReadTheDocs-Theme")
		.IsDependentOn("Should-Create-New-Project")
		.Does(() =>
		{
			// When
			MkDocsBuild(Paths.Temp, new MkDocsBuildSettings() { Theme = MkDocsTheme.ReadTheDocs });
			
			// Then
			Assert.True(DirectoryExists(Paths.Temp.Combine("site/")));
			Assert.True(FileExists(Paths.Temp.CombineWithFilePath("site/css/theme.css")));
		}));
	
buildTasks.Add(
	Task("Should-Use-Different-Site-Dir")
		.IsDependentOn("Should-Create-New-Project")
		.Does(() =>
		{
			// When
			MkDocsBuild(Paths.Temp, new MkDocsBuildSettings() { SiteDir = Paths.Temp.Combine("site_special/") });
			
			// Then
			Assert.True(DirectoryExists(Paths.Temp.Combine("site_special/")));
			Assert.True(FileExists(Paths.Temp.CombineWithFilePath("site_special/index.html")));
		}));
	
buildTasks.Add(
	Task("Should-Log-Debug-With-Verbose")
		.IsDependentOn("Should-Create-New-Project")
		.Does(() =>
		{
			// Given
			var contextAdapter = new ConsoleOutputContextAdapter(Context);
		
			// When
			contextAdapter.MkDocsBuild(Paths.Temp, new MkDocsBuildSettings() { Verbose = true });
			contextAdapter.CollectOutput();
			
			// Then
			Assert.NotEqual(0, contextAdapter.Output.Count());
			Assert.Contains("DEBUG", contextAdapter.Output.First());
		}));
	
Task("MkDocsBuild")
	.IsDependentOn("Should-Build-Project")
	.IsDependentOn("Should-Remove-File-With-Clean")
	.IsDependentOn("Should-Not-Remove-File-With-Dirty")
	.IsDependentOn("Should-Use-Config-From-Differet-Dir")
	.IsDependentOn("Should-Use-ReadTheDocs-Theme")
	.IsDependentOn("Should-Use-Different-Site-Dir")
	.IsDependentOn("Should-Log-Debug-With-Verbose");