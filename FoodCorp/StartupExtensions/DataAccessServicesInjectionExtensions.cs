using FoodCorp.Configuration.Constants;
using FoodCorp.Configuration.Model.AppSettings;
using FoodCorp.DataAccess.DatabaseContext;
using FoodCorp.DataAccess.Entities;
using FoodCorp.DataAccess.Repositories.ProductRepository;
using FoodCorp.DataAccess.Seeds;
using FoodCorp.DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FoodCorp.API.StartupExtensions;

public static class DataAccessServicesInjectionExtensions
{
    public static void AddDatabaseContext(this IServiceCollection services,
        IConfiguration configuration,
        SecuritySettings securitySettings)
    {
        var connectionString = configuration.GetConnectionString(AppSettingConstants.FoodCorpDbConnectionStringName);
        services.AddDbContext<FoodCorpDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
            options.UseCamelCaseNamingConvention();
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        services.AddIdentityCore<User>(options =>
            {
                options.SignIn.RequireConfirmedAccount = securitySettings.RequireConfirmedAccount;

                options.User.RequireUniqueEmail = securitySettings.RequireUniqueEmail;

                options.Password.RequireDigit = securitySettings.RequireDigit;
                options.Password.RequiredLength = securitySettings.RequiredLength;
                options.Password.RequireNonAlphanumeric = securitySettings.RequireNonAlphanumeric;
                options.Password.RequireUppercase = securitySettings.RequireUppercase;
                options.Password.RequireLowercase = securitySettings.RequireLowercase;
            })
            .AddEntityFrameworkStores<FoodCorpDbContext>()
            .AddDefaultTokenProviders();
        
        services.Configure<PasswordHasherOptions>(option =>
        {
            option.IterationCount = securitySettings.PasswordHashIterationsCount;
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