#load nuget:https://www.myget.org/F/cake-contrib/api/v2?package=Cake.Recipe&prerelease

Environment.SetVariableNames();

BuildParameters.SetParameters(context: Context,
                            buildSystem: BuildSystem,
                            sourceDirectoryPath: "./src",
                            title: "Cake.MkDocs",
                            repositoryOwner: "michalkowal",
                            repositoryName: "Cake.MkDocs",
                            appVeyorAccountName: "michalkowal",
							nuspecFilePath: "./nuspec/Cake.MkDocs.nuspec");

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(context: Context,
							dupFinderExcludeFilesByStartingCommentSubstring: new string[] 
							{
								"DupFinder exclude"
							});

Task("Run-Local-Integration-Tests")
    .IsDependentOn("Default")
    .Does(() => {
    CakeExecuteScript("./test.cake",
        new CakeSettings {
            Arguments = new Dictionary<string, string>{
                { "version", BuildParameters.Version.SemVersion },
                { "verbosity", Context.Log.Verbosity.ToString("F") }
            }});
});

Build.RunDotNetCore();