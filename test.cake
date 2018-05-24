var testsTask = Task("Tests")
    .Does(() => {
        Information("Tests complete.");
});

Setup(context => {
    Information("Starting integration tests...");
});

Teardown(context => {

});

RunTarget("Tests");