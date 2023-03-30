using Newtonsoft.Json;

namespace FoodCorp.BusinessLogic.Models.Account.Facebook;

public class FacebookImageData
{
    [JsonProperty]
    public string ImageUrl { get; set; }
}