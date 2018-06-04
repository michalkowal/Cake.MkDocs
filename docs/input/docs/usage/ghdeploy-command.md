---
Order: 6
---

# MkDocs GitHub Pages deploy command

> For more information see [GitHub Pages site](https://pages.github.com/)
> and [MkDocs guide](https://www.mkdocs.org/user-guide/deploying-your-docs/#github-pages).

## MkDocs project in working directory

GhDeploy MkDocs project:

```c#
MkDocsGhDeploy();
```

With settings:

```c#
MkDocsGhDeploy(new MkDocsGhDeploySettings()
{
    RemoteBranch = "pages-branch",
    RemoteName = "second-origin"
});
```

## MkDocs project in different directory

GhDeploy MkDocs in provided directory:

```c#
MkDocsGhDeploy("./docs-project");
```
```c#
MkDocsGhDeploy(new DirectoryPath("./docs-project"));
```

With settings:

```c#
MkDocsGhDeploy"./docs-project", new MkDocsGhDeploySettings()
{
    RemoteBranch = "pages-branch",
    RemoteName = "second-origin"
});
```
```c#
MkDocsGhDeploy(new DirectoryPath("./docs-project") ,new MkDocsGhDeploySettings()
{
	RemoteBranch = "pages-branch",
    RemoteName = "second-origin"
});
```