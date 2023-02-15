using FoodCorp.Configuration.ConfigurationProvider;

namespace FoodCorp.API.StartupExtensions;

public static class ConfigurationServicesInjectionExtensions
{
    public static void AddConfigurationProvider(this IServiceCollection services)
    {
        services.AddSingleton<IFoodCorpConfigurationProvider, FoodCorpConfigurationProvider>();
    }
}
