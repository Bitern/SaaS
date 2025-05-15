var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var postgresServer = builder.AddPostgres("postgres-server")
                           .WithDataVolume("postgres_data")
                           .WithPgAdmin();

var sharedDb = postgresServer.AddDatabase("biternDB");

var saasApi = builder.AddProject<Projects.SaaS_Api>("saas-api")
    .WithReference(sharedDb);

var erpApi = builder.AddProject<Projects.ERP_Api>("erp-api")
    .WithReference(sharedDb);

builder.AddProject<Projects.Frontend_Shell_Blazor>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(saasApi)
    .WaitFor(saasApi);

builder.Build().Run();
