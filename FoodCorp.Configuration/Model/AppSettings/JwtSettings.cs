namespace FoodCorp.Configuration.Model.AppSettings;

public class JwtSettings
{
    public bool ValidateIssuerSigningKey { get; set; }
    public bool ValidateIssuer { get; set; }
    public bool ValidateAudience { get; set; }
    public bool SaveToken { get; set; }
    public string SecretKey { get; set; }
    public bool ValidateLifetime { get; set; }
    public string ValidIssuer { get; set; }
    public string ValidAudience { get; set; }
    public int TokenDurationInMinutes { get; set; }
}
