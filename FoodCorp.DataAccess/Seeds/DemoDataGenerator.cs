using Bogus;
using Bogus.DataSets;
using FoodCorp.Configuration.Model.AppSettings;
using FoodCorp.DataAccess.Constants;
using FoodCorp.DataAccess.DatabaseContext;
using FoodCorp.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FoodCorp.DataAccess.Seeds;

public class DemoDataGenerator
{
    private readonly FoodCorpDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly IOptions<DataGeneratorSettings> _dataGeneratorSettings;

    public DemoDataGenerator(FoodCorpDbContext context, 
        UserManager<User> userManager, 
        IOptions<DataGeneratorSettings> dataGeneratorSettings)
    {
        _context = context;
        _userManager = userManager;
        _dataGeneratorSettings = dataGeneratorSettings;
    }

    public async Task ClearAllAsync()
    {
        await _context.Database.ExecuteSqlRawAsync(@$"
            DELETE FROM [{DatabaseSchemaNameConstants.User}].[{DatabaseTableNameConstants.User}];
            DELETE FROM [{DatabaseSchemaNameConstants.Product}].[{DatabaseTableNameConstants.Product}];");

        _context.ChangeTracker.Clear();
    }

    public async Task GenerateAsync()
    {
        var userFaker = new Faker<User>()
            .RuleFor(u => u.Name, f => f.Name.FirstName())
            .RuleFor(u => u.Surname, f => f.Name.LastName())
            .RuleFor(u => u.UserName, (f, u) => f.Internet.UserName(u.Name, u.Surname))
            .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.Name, u.Surname))
            .RuleFor(u => u.RegistrationDateTimeUtc, f => f.Date.Past())
            .RuleFor(u => u.Role, f => f.PickRandom<Enums.Role>())
            .RuleFor(u => u.ProfileImagePath, f => f.Image.LoremPixelUrl(LoremPixelCategory.People));

        var fakeUsers = userFaker.Generate(_dataGeneratorSettings.Value.NumbersOfUsersToGenerate);
        fakeUsers.ForEach(fu => _userManager.CreateAsync(fu, _dataGeneratorSettings.Value.IsRandomPasswordGenerationEnabled ?
            Guid.NewGuid().ToString() :
            _dataGeneratorSettings.Value.DefaultUserPassword));

        var productFaker = new Faker<Product>()
            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
            .RuleFor(p => p.Price, f => f.Random.Number(1, 10))
            .RuleFor(p => p.Category, f => f.PickRandom<Enums.Category>());

        var fakeProducts = productFaker.Generate(_dataGeneratorSettings.Value.NumbersOfProductsToGenerate);
        _context.Products.AddRange(fakeProducts);

        await _context.SaveChangesAsync();
    }
}
