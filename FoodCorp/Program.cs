using FoodCorp.API.Middleware;
using FoodCorp.API.StartupExtensions;
using FoodCorp.Configuration.Constants;
using NLog;
using NLog.Web;

var logger = NLogBuilder.ConfigureNLog(AppSettingConstants.LoggerConfigurationFileName).GetCurrentClassLogger();
try
{
    logger.Debug("Initialization");

    var builder = WebApplication.CreateBuilder(args);
    var services = builder.Services;
    var configuration = builder.Configuration;

    // Add services to the container.
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services.AddConfigurationProvider();
    services.AddDatabaseContext(configuration);
    services.AddDataAccessAbstractions();
    services.AddNlog();

    builder.ReplaceLoggingProviderWithNlog();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    
    app.UseMiddleware<ErrorHandlerMiddleware>();
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    
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