Task("Download-MkDocs-Badge")
	.Does(() =>
	{
		DownloadBadge("MkDocs", Version.MkDocs, "orange");
	});
	
Task("Download-Cake-Badge")
	.Does(() =>
	{
		DownloadBadge("Cake", Version.Cake, "orange");
	});

BuildParameters.Tasks.PublishDocumentationTask
	.IsDependentOn("Download-MkDocs-Badge")
	.IsDependentOn("Download-Cake-Badge")
    .Does(() => RequireTool(WyamTool, () => {
        // Check to see if any documentation has changed
        var sourceCommit = GitLogTip("./");
        Information("Source Commit Sha: {0}", sourceCommit.Sha);
        var filesChanged = GitDiff("./", sourceCommit.Sha);
        Information("Number of changed files: {0}", filesChanged.Count);
        var docFileChanged = false;

        var wyamDocsFolderDirectoryName = BuildParameters.WyamRootDirectoryPath.GetDirectoryName();

        foreach(var file in filesChanged)
        {
            var backslash = '\\';
            Verbose("Changed File OldPath: {0}, Path: {1}", file.OldPath, file.Path);
            if(file.OldPath.Contains(string.Format("{0}{1}", wyamDocsFolderDirectoryName, backslash)) ||
                file.Path.Contains(string.Format("{0}{1}", wyamDocsFolderDirectoryName, backslash)) ||
                file.Path.Contains("config.wyam"))
            {
            docFileChanged = true;
            break;
            }
        }

        if(docFileChanged)
        {
            Information("Detected that documentation files have changed, so running Wyam...");

            Wyam(new WyamSettings
            {
                Recipe = BuildParameters.WyamRecipe,
                Theme = BuildParameters.WyamTheme,
                OutputPath = MakeAbsolute(BuildParameters.Paths.Directories.PublishedDocumentation),
                RootPath = BuildParameters.WyamRootDirectoryPath,
                ConfigurationFile = BuildParameters.WyamConfigurationFile,
                PreviewVirtualDirectory = BuildParameters.WebLinkRoot,
                Settings = new Dictionary<string, object>
                {
                    { "Host",  BuildParameters.WebHost },
                    { "LinkRoot",  BuildParameters.WebLinkRoot },
                    { "BaseEditUrl", BuildParameters.WebBaseEditUrl },
                    { "SourceFiles", BuildParameters.WyamSourceFiles },
                    { "Title", BuildParameters.Title },
                    { "IncludeGlobalNamespace", false },
					{ "CakeVersion", Version.Cake },
					{ "MkDocsVersion", Version.MkDocs }
                }
            });

            PublishDocumentation();
        }
        else
        {
            Information("No documentation has changed, so no need to generate documentation");
        }
    })
);

BuildParameters.Tasks.PreviewDocumentationTask
	.IsDependentOn("Download-MkDocs-Badge")
	.IsDependentOn("Download-Cake-Badge")
    .Does(() => RequireTool(WyamTool, () => {
        Wyam(new WyamSettings
        {
            Recipe = BuildParameters.WyamRecipe,
            Theme = BuildParameters.WyamTheme,
            OutputPath = MakeAbsolute(BuildParameters.Paths.Directories.PublishedDocumentation),
            RootPath = BuildParameters.WyamRootDirectoryPath,
            Preview = true,
            Watch = true,
            ConfigurationFile = BuildParameters.WyamConfigurationFile,
            PreviewVirtualDirectory = BuildParameters.WebLinkRoot,
            Settings = new Dictionary<string, object>
            {
                { "Host",  BuildParameters.WebHost },
                { "LinkRoot",  BuildParameters.WebLinkRoot },
                { "BaseEditUrl", BuildParameters.WebBaseEditUrl },
                { "SourceFiles", BuildParameters.WyamSourceFiles },
                { "Title", BuildParameters.Title },
                { "IncludeGlobalNamespace", false },
				{ "CakeVersion", Version.Cake },
				{ "MkDocsVersion", Version.MkDocs }
            }
        });
    })
);

BuildParameters.Tasks.ForcePublishDocumentationTask
	.IsDependentOn("Download-MkDocs-Badge")
	.IsDependentOn("Download-Cake-Badge")
    .Does(() => RequireTool(WyamTool, () => {
        Wyam(new WyamSettings
        {
            Recipe = BuildParameters.WyamRecipe,
            Theme = BuildParameters.WyamTheme,
            OutputPath = MakeAbsolute(BuildParameters.Paths.Directories.PublishedDocumentation),
            RootPath = BuildParameters.WyamRootDirectoryPath,
            ConfigurationFile = BuildParameters.WyamConfigurationFile,
            PreviewVirtualDirectory = BuildParameters.WebLinkRoot,
            Settings = new Dictionary<string, object>
            {
                { "Host",  BuildParameters.WebHost },
                { "LinkRoot",  BuildParameters.WebLinkRoot },
                { "BaseEditUrl", BuildParameters.WebBaseEditUrl },
                { "SourceFiles", BuildParameters.WyamSourceFiles },
                { "Title", BuildParameters.Title },
                { "IncludeGlobalNamespace", false },
				{ "CakeVersion", Version.Cake },
				{ "MkDocsVersion", Version.MkDocs }
            }
        });

        PublishDocumentation();
    })
);

private void DownloadBadge(string subject, string status, string color)
{
	var url = $"https://img.shields.io/badge/{subject}-{status}-{color}.svg";
	var badgeFile = new FilePath($"./docs/input/assets/images/{subject.ToLower()}-badge.svg");
	DownloadFile(url, badgeFile);
}