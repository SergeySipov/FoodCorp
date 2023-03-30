namespace FoodCorp.Configuration.Model.AppSettings;

public class DataGeneratorSettings
{
    public int NumbersOfUsersToGenerate { get; set; }
    public string DefaultUserPassword { get; set; }
    public bool IsRandomPasswordGenerationEnabled { get; set; }
    public int NumbersOfProductsToGenerate { get; set; }
}
