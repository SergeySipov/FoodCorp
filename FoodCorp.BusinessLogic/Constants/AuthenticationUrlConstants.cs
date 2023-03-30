namespace FoodCorp.BusinessLogic.Constants;

public static class AuthenticationUrlConstants
{
    public const string TokenValidationUrl =
        "https://graph.facebook.com/debug_token?input_token={0}&access_token={1}|{2}";

    public const string UserInfoUrl =
        "https://graph.facebook.com/me?fields=first_name,last_name,email,id,picture&access_token={0}";
}
