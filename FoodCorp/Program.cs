using FoodCorp.API.Helpers;
using FoodCorp.API.Middleware;
using FoodCorp.API.StartupExtensions;
using FoodCorp.Configuration.Constants;
using FoodCorp.DataAccess.Seeds;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json.Linq;
using NLog;
using NLog.Web;
using System.Net.Mime;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;

var logger = NLogBuilder.ConfigureNLog(AppSettingConstants.LoggerConfigurationFileName).GetCurrentClassLogger();
try
{
    logger.Debug("Initialization");

    var builder = WebApplication.CreateBuilder(args);
    var services = builder.Services;
    var configuration = builder.Configuration;
    var webHostEnvironment = builder.Environment;
    var appVersion = ApplicationHelper.GetApplicationVersion();

    // Add services to the container.
    var appSettings = services.AddAppSettingsModels(configuration);

    services.AddGlobalizationAndLocalization();
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwagger(appVersion);
    services.AddConfigurationProvider();
    services.AddDatabaseContext(configuration, appSettings.IdentitySecuritySettings);
    services.AddDataAccessAbstractions();
    services.AddDemoDataSeed(); //temp solution
    services.AddMapster();
    services.AddBusinessLogicServices();
    services.AddAuthenticationAndAuthorization(appSettings);
    services.AddPresentationLayerServices();
    services.AddFluentValidation();
    services.AddCors(options =>
    {
        options.AddPolicy(AppSettingConstants.CorsPolicyName, cfg =>
        {
            cfg.WithOrigins(AppSettingConstants.CorsUrl)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
    });
    services.AddHttpClient();

    var connectionString = configuration.GetConnectionString(AppSettingConstants.FoodCorpDbConnectionStringName);
    services.AddHealthChecks()
        .AddSqlServer(connectionString!);

    builder.ReplaceLoggingProviderWithNlog(appSettings.SmtpSettings);

    var app = builder.Build();

    var serviceProvider = app.Services;
    var requestLocalizationOptions = serviceProvider.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;

    // Configure the HTTP request pipeline.
    if (!webHostEnvironment.IsProduction())
    {
        var currentAppVersion = appVersion;
        var swaggerEndpointWithCurrentVersion =
            string.Format(AppSettingConstants.SwaggerEndpointUrl, currentAppVersion);

        app.UseSwagger();
        app.UseSwaggerUI(option =>
        {
            option.SwaggerEndpoint(swaggerEndpointWithCurrentVersion, AppSettingConstants.ProjectName);
        });
    }

    app.UseMiddleware<ErrorHandlerMiddleware>();
    app.UseRequestLocalization(requestLocalizationOptions);
    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseCors(AppSettingConstants.CorsPolicyName);
    app.UseAuthentication();
    app.UseMiddleware<RequestsLoggingMiddleware>();
    app.MapControllers();

    if (!webHostEnvironment.IsProduction())
    {
        app.MapGet(AppSettingConstants.GenerateDemoDataMap, async (DemoDataGenerator generator) => //temp solution
        {
            await generator.ClearAllAsync();
            await generator.GenerateAsync();
        });
    }

    app.MapHealthChecks(AppSettingConstants.HealthCheckMap,
        new HealthCheckOptions { ResponseWriter = WriteHealthCheckResponse });

    app.Run();

    static Task WriteHealthCheckResponse(HttpContext httpContext, HealthReport result)
    {
        httpContext.Response.ContentType = MediaTypeNames.Application.Json;
        var json = new JObject(
            new JProperty("status", result.Status.ToString()),
            new JProperty("results", new JObject(result.Entries.Select(pair =>
                new JProperty(pair.Key, new JObject(
                    new JProperty("status", pair.Value.Status.ToString()),
                    new JProperty("description", pair.Value.Description),
                    new JProperty("data", new JObject(pair.Value.Data.Select(
                        p => new JProperty(p.Key, p.Value))))))))));
        return httpContext.Response.WriteAsync(json.ToString(Formatting.Indented));
    }
}
catch (Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}