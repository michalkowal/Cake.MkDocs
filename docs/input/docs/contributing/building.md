---
Order: 1
---

# Build scripts

To build this package we are using Cake.

On Windows PowerShell run:

```powershell
./build
```

On OSX/Linux run:

```bash
./build.sh
```

# Important developer tasks

## Default task

Clean, DotNetCore build, unit tests, code coverage, code analyze, NuGet package, create release notes.

```powershell
./build
```
```powershell
./build -Target Default
```

## AppVeyor task

The same as Default and:  
Publish documentation, upload code coverage, publish release note, publish NuGet, upload build artifacts.

```powershell
./build -Target Appveyor
```

## Preview task

```powershell
./build -Target Preview
```

Generate documentation and host documentation locally for development.  
More about [Preview](https://cake-contrib.github.io/Cake.Recipe/docs/usage/previewing-documentation) 
and [Docs publishing](https://cake-contrib.github.io/Cake.Recipe/docs/usage/publishing-documentation)