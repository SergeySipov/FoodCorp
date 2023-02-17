using NLog.Web;

namespace FoodCorp.API.StartupExtensions;

public static class PresentationLayerServicesInjectionExtensions
{
    public static void ReplaceLoggingProviderWithNlog(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        builder.Host.UseNLog();
    }
}
