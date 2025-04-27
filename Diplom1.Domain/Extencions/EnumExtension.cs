using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Diplom.Domain.Extencions
{
    public static class EnumExtension
    {
        public static string GetDisplayName(this System.Enum enumValue)
        {
            return enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()
                ?.GetName() ?? "Неопределенный";
        }
    }
}