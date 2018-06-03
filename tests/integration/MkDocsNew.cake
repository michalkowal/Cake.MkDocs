#load "utilities/context.cake"
#load "utilities/paths.cake"
#load "utilities/xunit.cake"

var newTasks = new List<CakeTaskBuilder<ActionTask>>();

newTasks.Add(
	Task("Should-Create-New-Project")
		.Does(() =>
		{
			// When
			MkDocsNew(Paths.Temp);
			
			// Then
			Assert.True(FileExists(Paths.Temp.CombineWithFilePath("mkdocs.yml")));
		}));
	
newTasks.Add(
	Task("Should-Log-Info")
		.Does(() =>
		{
			// Given
			var contextAdapter = new ConsoleOutputContextAdapter(Context);
		
			// When
			contextAdapter.MkDocsNew(Paths.Temp);
			contextAdapter.CollectOutput();
			
			// Then
			Assert.Equal(1, contextAdapter.Output.Count());
			Assert.Contains("INFO", contextAdapter.Output.First());
		}));
	
newTasks.Add(
	Task("Should-Not-Log-With-Quiet")
		.Does(() =>
		{
			// Given
			var contextAdapter = new ConsoleOutputContextAdapter(Context);
		
			// When
			contextAdapter.MkDocsNew(Paths.Temp, new MkDocsNewSettings() { Quiet = true });
			contextAdapter.CollectOutput();
			
			// Then
			Assert.Equal(0, contextAdapter.Output.Count());
		}));

Task("MkDocsNew")
	.IsDependentOn("Should-Create-New-Project")
	.IsDependentOn("Should-Log-Info")
	.IsDependentOn("Should-Not-Log-With-Quiet");