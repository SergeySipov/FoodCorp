namespace FoodCorp.Configuration.Model.AppSettings;

public class AppSettingsCompositeModel
{
    public JwtSettings JwtSettings { get; set; }
    public SecuritySettings SecuritySettings { get; set; }
    public GoogleAuthenticationSettings GoogleAuthenticationSettings { get; set; }
}
