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

var erpFrontend = builder.AddNpmApp("erp-frontend", "../../Frontend/Frontend.Modules.UI/erp-ui", "dev")
    .WithHttpEndpoint(env: "VITE_PORT")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(erpApi);

erpFrontend.WithEnvironment("ERP_URL", () => erpFrontend.GetEndpoint("http").Url);

var saasFrontend = builder.AddNpmApp("saas-frontend", "../../Frontend/Frontend.Modules.UI/saas-ui", "dev")
    .WithHttpEndpoint(env: "VITE_PORT")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(saasApi);

saasFrontend.WithEnvironment("SAAS_URL", () => saasFrontend.GetEndpoint("http").Url);

builder.Build().Run();