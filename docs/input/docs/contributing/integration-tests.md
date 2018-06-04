---
Order: 2
---

# Running integration tests

Addin contains set of integration tests written as additional Cake files.  
**test.cake** - is an entry point for integration tests. Installs Cake.MkDocs nuget package from BuildArtifacts directory and use him in all tests.  
**setup.cake** - his responsibility is launch integration tests in sandbox.

## Run-Integration-Tests task

```powershell
./build -Target Run-Integration-Tests
```

Task first of all execute *Default* target to build all artifacts.  
After all use BuildArtifacts and runs integration tests (**test.cake**).

## Run-Integration-Tests-Standalone task

```powershell
./build -Target Run-Integration-Tests-Standalone
```

Task use BuildArtifacts and runs integration tests without build - use for integration tests development.  
This task is also run additionally after target task, when target is different than *Run-Integration-Tests* (e.g. *Default*, *Appveyor*) and BuildParameters.ShouldRunIntegrationTests flag is true.