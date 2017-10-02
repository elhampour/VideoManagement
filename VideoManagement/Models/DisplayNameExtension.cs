using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace VideoManagement.Models
{
    public static class DisplayNameExtension
    {
        private static TAttribute GetAttribute<TAttribute>(this Enum enumValue) where TAttribute : Attribute
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<TAttribute>();
        }

        public static string GetDisplayName(this Enum enumValue)
        {
            var str = enumValue.GetAttribute<DisplayAttribute>();
            if (str == null)
            {
                return enumValue.ToString();
            }
            var str2 = str.Name;
            return str2;
        }
    }
}
