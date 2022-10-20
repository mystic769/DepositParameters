using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace DepositParameters.Extensions;

public static class EnumExtensions
{
    /// <summary>
    /// Получить аттрибут DisplayName
    /// </summary>
    /// <param name="enumValue"></param>
    /// <returns></returns>
    public static string GetDisplayName(this Enum enumValue)
    {
        return enumValue.GetType()
                        .GetMember(enumValue.ToString())
                        .First()
                        .GetCustomAttribute<DisplayAttribute>()
                        .GetName();
    }
}
