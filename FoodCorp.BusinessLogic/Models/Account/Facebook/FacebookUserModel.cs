using Newtonsoft.Json;

namespace FoodCorp.BusinessLogic.Models.Account.Facebook;

public class FacebookUserModel
{
    [JsonProperty("data")]
    public FacebookUserData UserInfo { get; set; }
}