using CreatorFund.Application;
using CreatorFund.Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddDatabase();
builder.AddRabbitMq("eventbus");
builder.AddAws();
builder.Services.AddApplication();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddTransient<AwsTest>();
var app = builder.Build();
app.MapDefaultEndpoints();

app.MapPost("/test", async (AwsTest awsTest) =>
{
    try
    {
        await awsTest.PutObject();
        return Results.Ok(new { message = "File uploaded successfully to S3" });
    }
    catch (Exception ex)
    {
        return Results.Problem(
            title: "Upload failed",
            detail: ex.Message,
            statusCode: 500
        );
    }
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.Run();
