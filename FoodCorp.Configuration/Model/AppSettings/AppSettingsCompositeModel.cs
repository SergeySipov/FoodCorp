namespace FoodCorp.Configuration.Model.AppSettings;

public class AppSettingsCompositeModel
{
    public JwtSettings JwtSettings { get; set; }
    public SecuritySettings SecuritySettings { get; set; }
    public GoogleAuthenticationSettings GoogleAuthenticationSettings { get; set; }
    public FacebookAuthenticationSettings FacebookAuthenticationSettings { get; set; }
    public SmtpSettings SmtpSettings { get; set; }
}
