using System.ComponentModel;
using FoodCorp.Common.Models;

namespace FoodCorp.Common.Helpers;

public static class EnumHelper
{
    public static List<EnumModel> ConvertToModel<T>() where T : struct, IConvertible
    {
        var type = typeof(T);

        if (!type.IsEnum)
        {
            throw new ArgumentException("parameter must be System.Enum");
        }

        var result = new List<EnumModel>();
        foreach (var value in Enum.GetValues(type))
        {
            var enumModel = new EnumModel
            {
                Id = (int)value,
                Name = ((T)value).GetDescription()
            };

            result.Add(enumModel);
        }

        return result;
    }

    public static string GetDescription<T>(this T enumValue) where T : struct, IConvertible
    {
        if (!typeof(T).IsEnum)
        {
            return null;
        }

        var description = enumValue.ToString();
        var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

        if (fieldInfo != null)
        {
            var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
            if (attrs.Length > 0)
            {
                description = ((DescriptionAttribute)attrs[0]).Description;
            }
        }

        return description;
    }
}