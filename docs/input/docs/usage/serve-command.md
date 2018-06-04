---
Order: 5
---

# MkDocs serve command

> **This method swill block build process. Use <c>Ctrl+C</c> in console to quit.**

## MkDocs project in working directory

Serve MkDocs project:

```c#
MkDocsServe();
```

With settings:

```c#
MkDocsServe(new MkDocsServeSettings()
{
    DevAddr = new MkDocsAddress("localhost", 8090),
    Theme = MkDocsTheme.ReadTheDocs
});
```

Configuring timeout:

```c#
try
{
    MkDocsServe(new MkDocsServeSettings()
    {
        ToolTimeout = new TimeSpan(0, 0, 1, 0)
    });
}
catch (TimeoutException)
{
    // Kill tool process after 1 minute
}
```

## MkDocs project in different directory

Serve MkDocs in provided directory:

```c#
MkDocsServe("./docs-project");
```
```c#
MkDocsServe(new DirectoryPath("./docs-project"));
```

With settings:

```c#
MkDocsServe"./docs-project", new MkDocsServeSettings()
{
    Dirty = true,
    Theme = MkDocsTheme.ReadTheDocs
});
```
```c#
MkDocsServe(new DirectoryPath("./docs-project") ,new MkDocsServeSettings()
{
    Dirty = true,
    Theme = MkDocsTheme.ReadTheDocs
});
```

Configuring timeout:

```c#
try
{
    MkDocsServe(new DirectoryPath("./docs-project"), new MkDocsServeSettings()
    {
        ToolTimeout = new TimeSpan(0, 0, 1, 0)
    });
}
catch (TimeoutException)
{
    // Kill tool process after 1 minute
}
```

# MkDocs serve async command

> **This method will block build process. Use <c>Ctrl+C</c> in console to quit**
> **or use CancellationToken to cancel task programmatically.**

## MkDocs project in working directory

Serve MkDocs project:

```c#
var task = MkDocsServeAsync();
// Do work...
task.Wait();
```

With settings:

```c#
using (var tokenSource = new CancellationTokenSource())
{
    var task = MkDocsServeAsync(new MkDocsServeAsyncSettings()
    {
        Token = tokenSource.Token
    });

    // Do work...
    tokenSource.Cancel();

    try
    {
        task.Wait();
    }
    catch (OperationCanceledException)
    {
    }
}
```

Configuring timeout:

```c#
using (var tokenSource = new CancellationTokenSource())
{
    var task = MkDocsServeAsync(new MkDocsServeAsyncSettings()
    {
        ToolTimeout = new TimeSpan(0, 0, 1, 0)
    });

    // Do work...

    try
    {
        task.Wait();
    }
    catch (TimeoutException)
    {
        // Kill tool process after 1 minute
    }
}
```

## MkDocs project in different directory

Serve MkDocs in provided directory:

```c#
var task = MkDocsServeAsync("./docs-project");
// Do work...
task.Wait();
```
```c#
var task = MkDocsServeAsync(new DirectoryPath("./docs-project"));
// Do work...
task.Wait();
```

With settings:

```c#
using (var tokenSource = new CancellationTokenSource())
{
    var task = MkDocsServeAsync("./docs-project", new MkDocsServeAsyncSettings()
    {
        Token = tokenSource.Token
    });

    // Do work...
    tokenSource.Cancel();

    try
    {
        task.Wait();
    }
    catch (OperationCanceledException)
    {
    }
}
```
```c#
using (var tokenSource = new CancellationTokenSource())
{
    var task = MkDocsServeAsync(new DirectoryPath("./docs-project"), new MkDocsServeAsyncSettings()
    {
        Token = tokenSource.Token
    });

    // Do work...
    tokenSource.Cancel();

    try
    {
        task.Wait();
    }
    catch (OperationCanceledException)
    {
    }
}
```

Configuring timeout:

```c#
using (var tokenSource = new CancellationTokenSource())
{
    var task = MkDocsServeAsync(new DirectoryPath("./docs-project"), new MkDocsServeAsyncSettings()
    {
        ToolTimeout = new TimeSpan(0, 0, 1, 0)
    });

    // Do work...

    try
    {
        task.Wait();
    }
    catch (TimeoutException)
    {
        // Kill tool process after 1 minute
    }
}
```