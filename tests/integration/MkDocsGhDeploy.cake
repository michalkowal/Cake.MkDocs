#load "utilities/context.cake"
#load "utilities/paths.cake"
#load "utilities/xunit.cake"

var ghDeployTasks = new List<CakeTaskBuilder>();

ghDeployTasks.Add(
	Task("Try-To-Deploy")
		.IsDependentOn("Should-Create-New-Project")
		.Does(() =>
		{
			// Given
			var contextAdapter = new ConsoleOutputContextAdapter(Context);
		
			// When
			try
			{
				contextAdapter.MkDocsGhDeploy(Paths.Temp);
			}
			catch (CakeException)
			{
				// Expected error
			}
			contextAdapter.CollectOutput();
			
			// Then
			var sitePath = Paths.Temp.Combine("site").FullPath;
			if (Context.IsRunningOnWindows())
				sitePath = sitePath.Replace("/", "\\");
			Assert.True(contextAdapter.Output.Any(o => o.Contains($"Copying '{sitePath}' to 'gh-pages' branch")));
			Assert.True(contextAdapter.Output.Any(o => o.Contains("'origin' does not appear to be a git repository")));
		}));

ghDeployTasks.Add(
	Task("Try-To-Deploy-To-Different-Remote-Name")
		.IsDependentOn("Should-Create-New-Project")
		.Does(() =>
		{
			// Given
			var contextAdapter = new ConsoleOutputContextAdapter(Context);
			var settings = new MkDocsGhDeploySettings()
			{
				RemoteName = "fakeorigin"
			};
		
			// When
			try
			{
				contextAdapter.MkDocsGhDeploy(Paths.Temp, settings);
			}
			catch (CakeException)
			{
				// Expected error
			}
			contextAdapter.CollectOutput();
			
			// Then
			var sitePath = Paths.Temp.Combine("site").FullPath;
			if (Context.IsRunningOnWindows())
				sitePath = sitePath.Replace("/", "\\");
			Assert.True(contextAdapter.Output.Any(o => o.Contains($"Copying '{sitePath}' to 'gh-pages' branch")));
			Assert.True(contextAdapter.Output.Any(o => o.Contains("'fakeorigin' does not appear to be a git repository")));
		}));
	
ghDeployTasks.Add(
	Task("Try-To-Deploy-To-Different-Branch")
		.IsDependentOn("Should-Create-New-Project")
		.Does(() =>
		{
			// Given
			var contextAdapter = new ConsoleOutputContextAdapter(Context);
			var settings = new MkDocsGhDeploySettings()
			{
				RemoteBranch = "ghp"
			};
		
			// When
			try
			{
				contextAdapter.MkDocsGhDeploy(Paths.Temp, settings);
			}
			catch (CakeException)
			{
				// Expected error
			}
			contextAdapter.CollectOutput();
			
			// Then
			var sitePath = Paths.Temp.Combine("site").FullPath;
			if (Context.IsRunningOnWindows())
				sitePath = sitePath.Replace("/", "\\");
			Assert.True(contextAdapter.Output.Any(o => o.Contains($"Copying '{sitePath}' to 'ghp' branch")));
			Assert.True(contextAdapter.Output.Any(o => o.Contains("'origin' does not appear to be a git repository")));
		}));
	
ghDeployTasks.Add(
	Task("Try-To-Deploy-With-Clean-Site")
		.IsDependentOn("Should-Create-New-Project")
		.Does(() =>
		{
			// Given
			CreateDirectory(Paths.Temp.Combine("site"));
			CopyFile(Paths.Resources.CombineWithFilePath("noop.txt"), Paths.Temp.CombineWithFilePath("site/noop.txt"));
			var settings = new MkDocsGhDeploySettings()
			{
				Clean = true
			};
		
			// When
			try
			{
				MkDocsGhDeploy(Paths.Temp, settings);
			}
			catch (CakeException)
			{
				// Expected error
			}
			
			// Then
			Assert.False(FileExists(Paths.Temp.CombineWithFilePath("site/noop.txt")));
		}));
	
ghDeployTasks.Add(
	Task("Try-To-Deploy-With-Dirty-Site")
		.IsDependentOn("Should-Create-New-Project")
		.Does(() =>
		{
			// Given
			CreateDirectory(Paths.Temp.Combine("site"));
			CopyFile(Paths.Resources.CombineWithFilePath("noop.txt"), Paths.Temp.CombineWithFilePath("site/noop.txt"));
			var settings = new MkDocsGhDeploySettings()
			{
				Dirty = true
			};
		
			// When
			try
			{
				MkDocsGhDeploy(Paths.Temp, settings);
			}
			catch (CakeException)
			{
				// Expected error
			}
			
			// Then
			Assert.True(FileExists(Paths.Temp.CombineWithFilePath("site/noop.txt")));
		}));
	
ghDeployTasks.Add(
	Task("Try-To-Deploy-With-Different-Config")
		.IsDependentOn("Should-Create-New-Project")
		.Does(() =>
		{
			// Given
			CreateDirectory(Paths.Temp.Combine("config/"));
			MoveFile(Paths.Temp.CombineWithFilePath("mkdocs.yml"), Paths.Temp.CombineWithFilePath("config/mkdocs.yml"));
			var settings = new MkDocsGhDeploySettings()
			{
				ConfigFile = Paths.Temp.CombineWithFilePath("config/mkdocs.yml")
			};
		
			// When
			try
			{
				MkDocsGhDeploy(Paths.Temp, settings);
			}
			catch (CakeException)
			{
				// Expected error
			}
			
			// Then
			Assert.True(DirectoryExists(Paths.Temp.Combine("site/")));
			Assert.True(FileExists(Paths.Temp.CombineWithFilePath("site/index.html")));
		})
		.Finally(() =>
		{
			MoveFile(Paths.Temp.CombineWithFilePath("config/mkdocs.yml"), Paths.Temp.CombineWithFilePath("mkdocs.yml"));
			DeleteDirectory(Paths.Temp.Combine("config/"), new DeleteDirectorySettings());
		}));

Task("MkDocsGhDeploy")
	.IsDependentOn("Try-To-Deploy")
	.IsDependentOn("Try-To-Deploy-To-Different-Remote-Name")
	.IsDependentOn("Try-To-Deploy-To-Different-Branch")
	.IsDependentOn("Try-To-Deploy-With-Clean-Site")
	.IsDependentOn("Try-To-Deploy-With-Dirty-Site")
	.IsDependentOn("Try-To-Deploy-With-Different-Config");