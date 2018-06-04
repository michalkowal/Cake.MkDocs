---
Order: 1
---

# Build Script

You can reference Cake.MkDocs in your build script as a cake addin:

```cake
#addin "Cake.MkDocs"
```

or nuget reference:

```cake
#addin "nuget:?package=Cake.MkDocs"
```

# Prerequisites

Cake.MkDocs addin use global installation of  MkDocs - with correct environment variables setup like PATH and PYTHONPATH.  
There is a possibility to use different installation with ToolPath property in ToolSettings (check commands examples). 

MkDocs can be installed with different Package Managers on different platforms.  
For more information and installation instruction see [MkDocs installation guide](https://www.mkdocs.org/#installation).

# Supported version

| Tool | Version |
| --- | --- |
| MkDocs | ~0.17.3 |
| Cake | ~0.27.2 |