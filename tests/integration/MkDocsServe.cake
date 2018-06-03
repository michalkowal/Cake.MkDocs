#load "utilities/context.cake"
#load "utilities/paths.cake"
#load "utilities/network.cake"
#load "utilities/xunit.cake"

LongRunTaskContext _taskData;
var serveTasks = new List<CakeTaskBuilder<ActionTask>>();

serveTasks.Add(
	Task("Should-Serve-Project")
		.IsDependentOn("Should-Create-New-Project")
		.Does(() =>
		{
			// Given
			var settings = new MkDocsServeSettings()
			{
				Token = _taskData.CancellationSource.Token
			};
		
			// When
			_taskData.LongRunTask = MkDocsServe(Paths.Temp, settings);
		
			// Then
			Assert.True(Network.PingHost("127.0.0.1", 8000));
		}));
	
serveTasks.Add(
	Task("Should-Serve-With-Config-From-Different-Dir")
		.IsDependentOn("Should-Create-New-Project")
		.Does(() =>
		{
			// Given
			CreateDirectory(Paths.Temp.Combine("config/"));
			MoveFile(Paths.Temp.CombineWithFilePath("mkdocs.yml"), Paths.Temp.CombineWithFilePath("config/mkdocs.yml"));
			var settings = new MkDocsServeSettings()
			{
				Token = _taskData.CancellationSource.Token,
				ConfigFile = Paths.Temp.CombineWithFilePath("config/mkdocs.yml")
			};
		
			// When
			_taskData.LongRunTask = MkDocsServe(Paths.Temp, settings);
			
			// Then
			Assert.True(Network.PingHost("127.0.0.1", 8000));
		})
		.Finally(() =>
		{
			MoveFile(Paths.Temp.CombineWithFilePath("config/mkdocs.yml"), Paths.Temp.CombineWithFilePath("mkdocs.yml"));
			DeleteDirectory(Paths.Temp.Combine("config/"), new DeleteDirectorySettings() { Force = true });
		}));

serveTasks.Add(
	Task("Should-Serve-Project-On-Different-Port")
		.IsDependentOn("Should-Create-New-Project")
		.Does(() =>
		{
			// Given
			var settings = new MkDocsServeSettings()
			{
				Token = _taskData.CancellationSource.Token,
				DevAddr = new MkDocsAddress("127.0.0.1", 8090)
			};
		
			// When
			_taskData.LongRunTask = MkDocsServe(Paths.Temp, settings);
		
			// Then
			Assert.True(Network.PingHost("127.0.0.1", 8090));
		}));
	
serveTasks.Add(
	Task("Should-Reload-After-Copy")
		.IsDependentOn("Should-Create-New-Project")
		.Does(() =>
		{
			// Given
			var contextAdapter = new ConsoleOutputContextAdapter(Context);
			var settings = new MkDocsServeSettings()
			{
				Token = _taskData.CancellationSource.Token,
				ToolTimeout = new TimeSpan(0, 0, 0, 5)
			};
		
			// When
			_taskData.LongRunTask = contextAdapter.MkDocsServe(Paths.Temp, settings);
		
			Network.PingHost("127.0.0.1", 8000);
			
			// Wait for detect changes
			Thread.Sleep(2000);
			CopyFile(Paths.Resources.CombineWithFilePath("index.md"), Paths.Temp.CombineWithFilePath("docs/index.md"));

			contextAdapter.CollectOutput();
			
			// Then
			Assert.Equal(2, contextAdapter.Output.Count(o => o.Contains("Building documentation...")));
		}));
	
serveTasks.Add(	
	Task("Should-Not-Reload-After-Copy-If-NoLiveReload")
		.IsDependentOn("Should-Create-New-Project")
		.Does(() =>
		{
			// Given
			var contextAdapter = new ConsoleOutputContextAdapter(Context);
			var settings = new MkDocsServeSettings()
			{
				Token = _taskData.CancellationSource.Token,
				ToolTimeout = new TimeSpan(0, 0, 0, 5),
				NoLiveReload = true
			};
		
			// When
			_taskData.LongRunTask = contextAdapter.MkDocsServe(Paths.Temp, settings);
		
			Network.PingHost("127.0.0.1", 8000);
			
			// Wait for detect changes
			Thread.Sleep(2000);
			CopyFile(Paths.Resources.CombineWithFilePath("index.md"), Paths.Temp.CombineWithFilePath("docs/index.md"));
			
			contextAdapter.CollectOutput();
			
			// Then
			Assert.Equal(1, contextAdapter.Output.Count(o => o.Contains("Building documentation...")));
		}));

Task("MkDocsServe")
	.IsDependentOn("Should-Serve-Project")
	.IsDependentOn("Should-Serve-With-Config-From-Different-Dir")
	.IsDependentOn("Should-Serve-Project-On-Different-Port")
	.IsDependentOn("Should-Reload-After-Copy")
	.IsDependentOn("Should-Not-Reload-After-Copy-If-NoLiveReload");