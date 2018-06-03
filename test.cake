#addin nuget:?package=Cake.MkDocs&prerelease

#load "tests/integration/utilities/paths.cake"

#load "tests/integration/MkDocsAliases.cake"

Task("Tests")
	.IsDependentOn("MkDocsAliases")
    .Does(() => {
        Information("Tests complete.");
});

Setup(context => {
    Information("Starting integration tests...");
	CreateDirectory(Paths.Temp);
});

Teardown(context => {
	DeleteDirectory(Paths.Temp, new DeleteDirectorySettings() { Recursive = true } );
});

RunTarget("Tests");