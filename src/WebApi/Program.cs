using FirebaseAdmin;
using Google.Api.Gax.ResourceNames;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using OpsManagerAPI.Application;
using OpsManagerAPI.Infrastructure;
using OpsManagerAPI.Infrastructure.Common;
using OpsManagerAPI.Infrastructure.Logging.Serilog;
using OpsManagerAPI.WebApi.Configurations;
using OpsManagerAPI.WebApi.Controllers.Conventions;
using Serilog;

[assembly: ApiConventionType(typeof(OPSApiConventions))]

StaticLogger.EnsureInitialized();
Log.Information("Server Booting Up...");
try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.AddConfigurations().RegisterSerilog();

    builder.Services
                    .AddControllers(opts =>
                    {
                        opts.Conventions.Add(new RouteTokenTransformerConvention(
                          new ToKebabCaseParameterTransformer()));
                    })
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.Converters.Add(new UppercaseEnumConverter());
                    });

    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddApplication();

    var app = builder.Build();

    // firebase
    var defaultApp = FirebaseApp.Create(new AppOptions()
    {
        Credential = GoogleCredential.FromFile(Path.Combine(Directory.GetCurrentDirectory(), "Configurations/firebase.json")),
    });

    await app.Services.InitializeDatabasesAsync();

    app.UseInfrastructure(builder.Configuration);
    app.MapEndpoints();
    app.Run();
}
catch (Exception ex) when (!ex.GetType().Name.Equals("StopTheHostException", StringComparison.Ordinal))
{
    StaticLogger.EnsureInitialized();
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    StaticLogger.EnsureInitialized();
    Log.Information("Server Shutting down...");
    Log.CloseAndFlush();
}