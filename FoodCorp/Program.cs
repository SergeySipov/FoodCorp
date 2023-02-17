using FoodCorp.API.Helpers;
using FoodCorp.API.Middleware;
using FoodCorp.API.StartupExtensions;
using FoodCorp.Configuration.Constants;
using FoodCorp.DataAccess.Seeds;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;

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
    services.AddControllers();
    services.AddEndpointsApiExplorer();

    services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc(appVersion,
            new OpenApiInfo
            {
                Title = AppSettingConstants.ProjectName,
                Version = appVersion
            });
    });

    services.AddConfigurationProvider();
    services.AddDatabaseContext(configuration);
    services.AddDataAccessAbstractions();
    services.AddDemoDataSeed();//temp solution

    builder.ReplaceLoggingProviderWithNlog();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!webHostEnvironment.IsProduction())
    {
        var currentAppVersion = appVersion;
        var swaggerEndpointWithCurrentVersion = string.Format(AppSettingConstants.SwaggerEndpointUrl, currentAppVersion);

        app.UseSwagger();
        app.UseSwaggerUI(option =>
        {
            option.SwaggerEndpoint(swaggerEndpointWithCurrentVersion, AppSettingConstants.ProjectName);
        });
    }
    
    app.UseMiddleware<ErrorHandlerMiddleware>();
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    app.MapGet("/AddDemoRecordsToDb", async (DemoDataGenerator generator) => //temp solution
    {
        await generator.ClearAllAsync();
        await generator.GenerateAsync();
    });

    app.Run();
}
catch(Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}