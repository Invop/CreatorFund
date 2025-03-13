using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedis("redis");
var rabbitMq = builder.AddRabbitMQ("eventbus")
    .WithLifetime(ContainerLifetime.Persistent);
var postgres = builder.AddPostgres("postgres")
    .WithImageTag("latest")
    .WithPgAdmin()
    .WithLifetime(ContainerLifetime.Persistent);

var api = builder.AddProject<CreatorFund_Api>("api");

builder.AddNpmApp("angular", "../../frontend/creator-fund.web")
    .WaitFor(api)
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

builder.Build().Run();
