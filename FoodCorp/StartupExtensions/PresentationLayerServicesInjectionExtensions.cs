using Microsoft.EntityFrameworkCore;
using NLog.Web;

namespace FoodCorp.API.StartupExtensions;

public static class PresentationLayerServicesInjectionExtensions
{
    public static void ReplaceLoggingProviderWithNlog(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        builder.Host.UseNLog();
    }

    public static void AddNlog(this IServiceCollection services)
    {
        services.AddLogging(builder =>
        {
            builder.AddConsole()
                .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information); //output sql commands to console

            builder.AddDebug(); //output to okno otladki
        });
    }
}
