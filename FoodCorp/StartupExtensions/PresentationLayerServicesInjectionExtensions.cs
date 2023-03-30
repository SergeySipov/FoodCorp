using System.Globalization;
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
using Microsoft.AspNetCore.Localization;
using FoodCorp.API.Constants;
using NLog.Targets;
using LogLevel = NLog.LogLevel;

namespace FoodCorp.API.StartupExtensions;

public static class PresentationLayerServicesInjectionExtensions
{
    public static void ReplaceLoggingProviderWithNlog(this WebApplicationBuilder builder, SmtpSettings smtpSettings)
    {
        builder.Logging.ClearProviders();
        builder.Host.UseNLog();

        var config = NLog.LogManager.Configuration;

        var target = new MailTarget
        {
            Name = nameof(MailTarget),
            SmtpServer = smtpSettings.Host,
            SmtpPort = smtpSettings.SslEnabledPort,
            SmtpAuthentication = SmtpAuthenticationMode.Basic,
            SmtpUserName = smtpSettings.SenderMail,
            SmtpPassword = smtpSettings.SenderMailPassword,
            From = smtpSettings.SenderMail,
            To = smtpSettings.RecipientMail,
            EnableSsl = smtpSettings.IsSslEnabled,

            Html = true,
            AddNewLines = true,
            ReplaceNewlineWithBrTagInHtml = true,
            Subject = "SYSTEM MESSAGE：${machinename} #### ${shortdate} ${time} create ${level} message",
            Body = @"${newline} Time：${longdate} ${newline}${newline}Log level：${level:uppercase=true} ${newline}${newline}Logger：${logger} ${newline}${newline}Source：${callsite:className=true} ${newline}${newline}Exception：${exception:format=type} ${newline}${newline}Message：${message} ${newline}${newline}"
        };

        config.AddTarget(target);
        config.AddRuleForOneLevel(LogLevel.Info, target);
    }
    
    public static void AddAuthenticationAndAuthorization(this IServiceCollection services,
        AppSettingsCompositeModel appSettings)
    {
        var jwtSettings = appSettings.JwtSettings;

        var key = Encoding.ASCII.GetBytes(jwtSettings.SecretKey);

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
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

    public static AppSettingsCompositeModel AddAppSettingsModels(this IServiceCollection services,
        IConfiguration configuration)
    {
        (configuration as ConfigurationManager).AddUserSecrets(Assembly.GetExecutingAssembly(), true);

        var jwtSettingsSection = configuration.GetSection(SettingsSectionNameConstants.JwtSettings);
        var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
        services.Configure<JwtSettings>(jwtSettingsSection);

        var identitySecuritySettingsSection = configuration.GetSection(SettingsSectionNameConstants.SecuritySettings);
        var identitySecuritySettings = identitySecuritySettingsSection.Get<IdentitySecuritySettings>();
        services.Configure<IdentitySecuritySettings>(identitySecuritySettingsSection);

        var googleAuthenticationSettingsSection = configuration.GetSection(SettingsSectionNameConstants.GoogleAuthenticationSettings);
        var googleAuthenticationSettings = googleAuthenticationSettingsSection.Get<GoogleAuthenticationSettings>();
        services.Configure<GoogleAuthenticationSettings>(googleAuthenticationSettingsSection);

        var facebookAuthenticationSettingsSection = configuration.GetSection(SettingsSectionNameConstants.FacebookAuthenticationSettings);
        var facebookAuthenticationSettings = facebookAuthenticationSettingsSection.Get<FacebookAuthenticationSettings>();
        services.Configure<FacebookAuthenticationSettings>(facebookAuthenticationSettingsSection);

        var smtpSettingsSection = configuration.GetSection(SettingsSectionNameConstants.SmtpSettings);
        var smtpSettings = smtpSettingsSection.Get<SmtpSettings>();
        services.Configure<SmtpSettings>(smtpSettingsSection);

        var dataGeneratorSettingsSection = configuration.GetSection(SettingsSectionNameConstants.DataGeneratorSettings);
        var dataGeneratorSettings = dataGeneratorSettingsSection.Get<DataGeneratorSettings>();
        services.Configure<DataGeneratorSettings>(dataGeneratorSettingsSection);

        var compositeModel = new AppSettingsCompositeModel
        {
            JwtSettings = jwtSettings,
            IdentitySecuritySettings = identitySecuritySettings,
            GoogleAuthenticationSettings = googleAuthenticationSettings,
            FacebookAuthenticationSettings = facebookAuthenticationSettings,
            SmtpSettings = smtpSettings,
            DataGeneratorSettings = dataGeneratorSettings
        };

        return compositeModel;
    }

    public static void AddFluentValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblyContaining<Program>();
    }

    public static void AddGlobalizationAndLocalization(this IServiceCollection services)
    {
        services.AddLocalization(options =>
        {
            options.ResourcesPath = AppSettingConstants.ResourcesPath;
        });

        var supportedCultures = new[]
        {
            new CultureInfo(CultureInfoConstants.EnglishUs),
            new CultureInfo(CultureInfoConstants.Russian)
        };
        
        services.Configure<RequestLocalizationOptions>(options => {
            options.DefaultRequestCulture = new RequestCulture(CultureInfoConstants.EnglishUs);
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
        });
    }
}