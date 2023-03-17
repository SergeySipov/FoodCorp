using Newtonsoft.Json;

namespace FoodCorp.BusinessLogic.Models.Account.Facebook;

public class FacebookUserInfo
{

    [JsonProperty("first_name")]
    public string FirstName { get; set; }

    [JsonProperty("last_name")]
    public string LastName { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("picture")]
    public FacebookImageData Image { get; set; }
}