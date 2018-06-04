---
Order: 3
---

# MkDocs new command

## MkDocs project in working directory

Create new MkDocs project:

```c#
MkDocsNew();
```

With settings:

```c#
MkDocsNew(new MkDocsNewSettings()
{
    Verbose = true
});
```

## MkDocs project in different directory

Create new MkDocs in provided directory:

```c#
MkDocsNew("./docs-project");
```
```c#
MkDocsNew(new DirectoryPath("./docs-project"));
```

With settings:

```c#
MkDocsNew("./docs-project", new MkDocsNewSettings()
{
    Verbose = true
});
```
```c#
MkDocsNew(new DirectoryPath("./docs-project") ,new MkDocsNewSettings()
{
    Verbose = true
});
```