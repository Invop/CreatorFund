using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<CreatorFund_Api>("api");

builder.AddNpmApp("angular", "../../frontend/creator-fund.web")
    .WaitFor(api)
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

builder.Build().Run();
