using FoodCorp.Configuration.Constants;
using FoodCorp.DataAccess.DatabaseContext;
using FoodCorp.DataAccess.Repositories.ProductRepository;
using FoodCorp.DataAccess.Seeds;
using FoodCorp.DataAccess.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace FoodCorp.API.StartupExtensions;

public static class DataAccessServicesInjectionExtensions
{
    public static void AddDatabaseContext(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(AppSettingConstants.FoodCorpDbConnectionStringName);
        services.AddDbContext<FoodCorpDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
    }

    public static void AddDataAccessAbstractions(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IProductRepository, ProductRepository>();
    }

    public static void AddDemoDataSeed(this IServiceCollection services)
    {
        services.AddScoped<DemoDataGenerator>();
    }
}