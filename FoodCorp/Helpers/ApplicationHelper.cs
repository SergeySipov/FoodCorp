using System.Reflection;

namespace FoodCorp.API.Helpers;

public static class ApplicationHelper
{
    public static string GetApplicationVersion()
    {
        var currentAppVersion = Assembly.GetEntryAssembly().GetName().Version.ToString();
        return currentAppVersion;
    }
}