using FoodCorp.DataAccess.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FoodCorp.DataAccess.DesignTimeFactories;

public class FoodCorpDbContextDesignTimeFactory : IDesignTimeDbContextFactory<FoodCorpDbContext>
{
    private const string AspNetCoreEnvironmentVariableName = "ASPNETCORE_ENVIRONMENT";
    private const string AppSettingsJsonFileName = "appsettings.json";
    private const string FoodCorpDbConnectionStringName = "FoodCorpDb";
    private const string ApiProjectDirectoryName = "FoodCorp";
    
    public FoodCorpDbContext CreateDbContext(string[] args)
    {
        var environment = Environment.GetEnvironmentVariable(AspNetCoreEnvironmentVariableName);

        var basePath = GetBasePath();

        var builder = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", false, true)
            .AddJsonFile($"appsettings.{environment}.json", true)
            .AddEnvironmentVariables();

        var config = builder.Build();
        var connectionString = config.GetConnectionString(FoodCorpDbConnectionStringName);

        var optionsBuilder = new DbContextOptionsBuilder<FoodCorpDbContext>();
        optionsBuilder.UseSqlServer(connectionString);
        var dbContext = new FoodCorpDbContext(optionsBuilder.Options);

        return dbContext;
    }

    private string GetBasePath()
    {
        var currentDirectoryPath = Directory.GetCurrentDirectory();
        var parentDirectoryPath = Directory.GetParent(currentDirectoryPath).FullName;
        var basePath = Path.Combine(parentDirectoryPath, ApiProjectDirectoryName);

        return basePath;
    }
}