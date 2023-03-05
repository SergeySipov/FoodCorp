using FoodCorp.Configuration.Constants;
using FoodCorp.Configuration.Model.AppSettings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;
using System.Text;
using FoodCorp.API.Mappers.AccountMapper;
using System.Reflection;
using FluentValidation.AspNetCore;
using FluentValidation;

namespace FoodCorp.API.StartupExtensions;

public static class PresentationLayerServicesInjectionExtensions
{
    public static void ReplaceLoggingProviderWithNlog(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        builder.Host.UseNLog();
    }
    
    public static void AddAuthenticationAndAuthorization(this IServiceCollection services, JwtSettings jwtSettings)
    {
        var key = Encoding.ASCII.GetBytes(jwtSettings.SecretKey);

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = jwtSettings.SaveToken;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                    ValidIssuer = jwtSettings.ValidIssuer,
                    ValidateIssuer = jwtSettings.ValidateIssuer,
                    ValidAudience = jwtSettings.ValidAudience,
                    ValidateAudience = jwtSettings.ValidateAudience,
                    ValidateLifetime = jwtSettings.ValidateLifetime
                };
            });

        services.AddAuthorization();
    }

    public static void AddPresentationLayerServices(this IServiceCollection services)
    {
        services.AddSingleton<IAccountMapper, AccountMapper>();
    }
    
    public static AppSettingsCompositeModel AddAppSettingsModels(this IServiceCollection services, IConfiguration configuration)
    {
        (configuration as ConfigurationManager).AddUserSecrets(Assembly.GetExecutingAssembly(), true);

        var jwtSettingsSection = configuration.GetSection(SettingsSectionNameConstants.JwtSettings);
        var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
        services.Configure<JwtSettings>(jwtSettingsSection);

        var securitySettingsSection = configuration.GetSection(SettingsSectionNameConstants.SecuritySettings);
        var securitySettings = securitySettingsSection.Get<SecuritySettings>();
        services.Configure<SecuritySettings>(securitySettingsSection);

        var compositeModel = new AppSettingsCompositeModel
        {
            JwtSettings = jwtSettings,
            SecuritySettings = securitySettings
        };

        return compositeModel;
    }

    public static void AddFluentValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblyContaining<Program>();
    }
}