namespace FoodCorp.Configuration.Model.AppSettings;

public class AppSettingsCompositeModel
{
    public JwtSettings JwtSettings { get; set; }
    public IdentitySecuritySettings IdentitySecuritySettings { get; set; }
    public GoogleAuthenticationSettings GoogleAuthenticationSettings { get; set; }
    public FacebookAuthenticationSettings FacebookAuthenticationSettings { get; set; }
    public SmtpSettings SmtpSettings { get; set; }
    public DataGeneratorSettings DataGeneratorSettings { get; set; }
}
