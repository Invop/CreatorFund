using Amazon;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedis("redis");
//TODO: ADD rabbitmq-delayed-message-exchange plugin
var rabbitMq = builder.AddRabbitMQ("eventbus")
    .WithImageTag("4.0.7")
    .WithManagementPlugin()
    .WithDockerfile("DockerFiles/RabbitMq", "Dockerfile")
    .WithLifetime(ContainerLifetime.Persistent);
var postgres = builder.AddPostgres("postgres")
    .WithPgAdmin()
    .WithLifetime(ContainerLifetime.Persistent);
var awsConfig = builder.AddAWSSDKConfig().WithProfile("dev")
    .WithRegion(RegionEndpoint.EUCentral1);

var awsResources = builder.AddAWSCloudFormationTemplate("AwsResources", "app-resources.template")
    .WithReference(awsConfig);


var api = builder.AddProject<CreatorFund_Api>("api")
    .WithReference(rabbitMq)
    .WaitFor(rabbitMq)
    .WaitFor(awsResources)
    .WithReference(awsResources);

builder.AddNpmApp("angular", "../../frontend/creator-fund.web")
    .WaitFor(api)
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

builder.Build().Run();
