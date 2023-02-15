using Microsoft.Extensions.Configuration;

namespace FoodCorp.Configuration.ConfigurationProvider;

public class FoodCorpConfigurationProvider : IFoodCorpConfigurationProvider
{
    private readonly IConfiguration _configuration;

    public FoodCorpConfigurationProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }
}
