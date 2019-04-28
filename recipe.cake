#load nuget:https://www.myget.org/F/cake-contrib/api/v2?package=Cake.Recipe&prerelease
#load version.cake
#load wyam.cake

Environment.SetVariableNames();

var target = Argument("Target", "Default");
var shouldRunIntegrationTests = target.StartsWith("MkDocs-Integration-Tests");

// Publish only once during Windows build
bool shouldPublish = Context.IsRunningOnWindows();

BuildParameters.SetParameters(context: Context,
                            buildSystem: BuildSystem,
                            sourceDirectoryPath: "./src",
                            title: "Cake.MkDocs",
                            repositoryOwner: "michalkowal",
                            repositoryName: "Cake.MkDocs",
                            appVeyorAccountName: "michalkowal",
							webBaseEditUrl: "https://github.com/michalkowal/Cake.MkDocs/tree/master/docs/input/",
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

Version.Initialize(Context);
Information("Cake version: {0}", Version.Cake);
Information("MkDocs version: {0}", Version.MkDocs);

// Task for integration tests without build
Task("MkDocs-Integration-Tests-Standalone")
	.WithCriteria(() => BuildParameters.ShouldRunIntegrationTests)
	.Does(() => 
    {
		CakeExecuteScript(BuildParameters.IntegrationTestScriptPath,
			new CakeSettings 
			{
				Arguments = new Dictionary<string, string>
				{
					{ "nuget_configfile", "./tests/integration/resources/NuGet.Config" },
					{ "verbosity", Context.Log.Verbosity.ToString("F") },
					{ "mkdocs_version", Version.MkDocs }
				}
			});
    })
	.Finally(() =>
	{
		Information("Deleting Cake.MkDocs package...");
		var package = GetDirectories("./tools/**/Cake.MkDocs*").FirstOrDefault();
		if (package != null)
		{
			DeleteDirectory(package, new DeleteDirectorySettings {
				Recursive = true
			});
		}
	});
	
Task("MkDocs-Integration-Tests")
	.IsDependentOn("Package")
	.IsDependentOn("MkDocs-Integration-Tests-Standalone");
	
Task("AppVeyor-With-Integration-Tests")
	.IsDependentOn("MkDocs-Integration-Tests")
	.IsDependentOn("AppVeyor");

Build.RunDotNetCore();