---
Order: 4
---

# MkDocs build command

## MkDocs project in working directory

Build MkDocs project:

```c#
MkDocsBuild();
```

With settings:

```c#
MkDocsBuild(new MkDocsBuildSettings()
{
    Dirty = true,
    Theme = MkDocsTheme.ReadTheDocs
});
```

## MkDocs project in different directory

Build MkDocs in provided directory:

```c#
MkDocsBuild("./docs-project");
```
```c#
MkDocsBuild(new DirectoryPath("./docs-project"));
```

With settings:

```c#
MkDocsBuild"./docs-project", new MkDocsBuildSettings()
{
    Dirty = true,
    Theme = MkDocsTheme.ReadTheDocs
});
```
```c#
MkDocsBuild(new DirectoryPath("./docs-project") ,new MkDocsBuildSettings()
{
    Dirty = true,
    Theme = MkDocsTheme.ReadTheDocs
});
```