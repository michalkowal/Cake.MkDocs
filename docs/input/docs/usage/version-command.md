---
Order: 2
---

# MkDocs version command

## Tool version

Get global MkDocs installation version:

```c#
var mkDocsVersion = MkDocsVersion();
Information($"MkDocs tool version: {mkDocsVersion}");
```

Get provided MkDocs tool version:

```c#
var mkDocsVersion = MkDocsVersion(new MkDocsVersionSettings()
{
    ToolPath = "./path-to-local-tool/bin/mkdocs"
});
Information($"MkDocs tool version: {mkDocsVersion}"); // e.g. - 0.16.0
```

## Supported version

Check is global MkDocs version is supported:

```c#
if (!MkDocsIsSupportedVersion())
     throw new Exception("Installed unsupported MkDocs version");
```

Check is local MkDocs version is supported:

```c#
bool isSupported = MkDocsIsSupportedVersion(new MkDocsVersionSettings()
{
    ToolPath = "./path-to-local-tool/bin/mkdocs"
});
Information($"Is provided MkDocs version supported: {isSupported}");
```