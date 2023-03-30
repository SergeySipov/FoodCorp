namespace FoodCorp.Configuration.Constants;

public static class AppSettingConstants
{
    public const string FoodCorpDbConnectionStringName = "FoodCorpDb";
    public const string LoggerConfigurationFileName = "web.config";
    public const string ProjectName = "FoodCorp";
    public const string ProjectDescription = "My pet project";
    public const string SwaggerEndpointUrl = "/swagger/{0}/swagger.json";
    public const string GenerateDemoDataMap = "/AddDemoRecordsToDb";
    public const string HealthCheckMap = "/Health";
    public const string CorsPolicyName = "DefaultPolicy";
    public const string CorsUrl = "http://localhost:3000";
    public const string ResourcesPath = "Resources";
}