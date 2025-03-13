using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedis("redis");
//TODO: ADD rabbitmq-delayed-message-exchange plugin
var rabbitMq = builder.AddRabbitMQ("eventbus")
    .WithManagementPlugin()
    .WithLifetime(ContainerLifetime.Persistent);
var postgres = builder.AddPostgres("postgres")
    .WithPgAdmin()
    .WithLifetime(ContainerLifetime.Persistent);

var demosdb = postgres.AddDatabase("demosdb");

var api = builder.AddProject<CreatorFund_Api>("api")
    .WithReference(demosdb)
    .WaitFor(demosdb);

builder.AddNpmApp("angular", "../../frontend/creator-fund.web")
    .WaitFor(api)
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

builder.Build().Run();
