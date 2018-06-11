#addin nuget:?package=Cake.MkDocs&prerelease

#load "tests/integration/utilities/paths.cake"

#load "tests/integration/MkDocsAliases.cake"

var mkDocsVersion = Argument("mkdocs_version", "");
if (string.IsNullOrEmpty(mkDocsVersion))
	throw new ArgumentNullException("mkdocs_version");

Task("Tests")
	.IsDependentOn("MkDocsAliases")
    .Does(() => {
        Information("Tests complete.");
});

Setup(context => {
    Information("Starting integration tests...");
	CreateDirectory(Paths.Temp);
	
	Information("Install global mkdocs");
	if (Context.IsRunningOnWindows())
		StartProcess("pip", new ProcessSettings() { Arguments = $"install mkdocs=={mkDocsVersion}" });
	else
		StartProcess("sudo", new ProcessSettings() { Arguments = $"pip install mkdocs=={mkDocsVersion}" });
});

Teardown(context => {
	DeleteDirectory(Paths.Temp, new DeleteDirectorySettings() { Recursive = true } );
});

RunTarget("Tests");