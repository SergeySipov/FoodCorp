using FoodCorp.API.Constants;
using FoodCorp.DataAccess.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace FoodCorp.API.StartupExtensions.DataAccessLayer
{
    public static class DatabaseServicesConfigurationExtension
    {
        public static void AddDatabaseContextConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(AppSettingConstants.FoodCorpDbConnectionStringName);
            services.AddDbContext<FoodCorpDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }
    }
}
