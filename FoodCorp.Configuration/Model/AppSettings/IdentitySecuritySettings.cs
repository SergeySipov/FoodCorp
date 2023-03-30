namespace FoodCorp.Configuration.Model.AppSettings;

public class IdentitySecuritySettings
{
    public bool RequireConfirmedAccount { get; set; }
    public bool RequireUniqueEmail { get; set; }
    public bool RequireDigit { get; set; }
    public int RequiredLength { get; set; }
    public bool RequireNonAlphanumeric { get; set; }
    public bool RequireUppercase { get; set; }
    public bool RequireLowercase { get; set; }
    public int PasswordHashIterationsCount { get; set; }
    public bool RequireConfirmedEmail { get; set; }
}