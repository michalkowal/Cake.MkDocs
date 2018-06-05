#load nuget:https://www.myget.org/F/cake-contrib/api/v2?package=Cake.Recipe&prerelease

Environment.SetVariableNames();

var shouldRunIntegrationTests = Argument("Target", "Default") == "Run-Integration-Tests";
// Publish only once during Windows build
bool shouldPublish = Context.IsRunningOnWindows() ? true : false;

BuildParameters.SetParameters(context: Context,
                            buildSystem: BuildSystem,
                            sourceDirectoryPath: "./src",
                            title: "Cake.MkDocs",
                            repositoryOwner: "michalkowal",
                            repositoryName: "Cake.MkDocs",
                            appVeyorAccountName: "michalkowal",
							webBaseEditUrl: $"https://github.com/michalkowal/Cake.MkDocs/tree/master/docs/input/",
							shouldRunIntegrationTests: shouldRunIntegrationTests,
							
							// Build on Unix
							shouldRunGitVersion: true,
							shouldExecuteGitLink: shouldPublish,
							shouldPublishMyGet: shouldPublish,
							shouldPublishChocolatey: shouldPublish,
							shouldPublishNuGet: shouldPublish,
							shouldPublishGitHub: shouldPublish,
							shouldGenerateDocumentation: shouldPublish);

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(context: Context);

// Task for integration tests without build
Task("Run-Integration-Tests-Standalone")
	.Does(() => 
    {
		CakeExecuteScript(BuildParameters.IntegrationTestScriptPath,
			new CakeSettings 
			{
				Arguments = new Dictionary<string, string>
				{
					{ "nuget_configfile", "./tests/integration/resources/NuGet.Config" },
					{ "verbosity", Context.Log.Verbosity.ToString("F") }
				}
			});
    })
	.Finally(() =>
	{
		Information("Deleting Cake.MkDocs package...");
		var packages = GetDirectories("./tools/**/Cake.MkDocs*");
		DeleteDirectories(packages, new DeleteDirectorySettings {
			Recursive = true
		});
	});

BuildParameters.Tasks.IntegrationTestTask.Task.Actions.Clear();
BuildParameters.Tasks.IntegrationTestTask
	.IsDependentOn("Run-Integration-Tests-Standalone");

Build.RunDotNetCore();

// If target is not Run-Integration-Tests and tests are allowed
// Run them after all
if (!shouldRunIntegrationTests && BuildParameters.ShouldRunIntegrationTests)
{
	RunTarget("Run-Integration-Tests-Standalone");
}