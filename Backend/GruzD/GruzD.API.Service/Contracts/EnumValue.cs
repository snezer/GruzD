using System;
using System.Collections.Generic;
using System.Linq;

namespace GruzD.Web.Contracts
{
    public class EnumValue<T>
    {
        public T Value { get; set; }
        public string Description { get; set; }
    }

    public static class EnumExtensionMethods
    {
        public static string GetDescription<T>(this T enumVal) where T : Enum
        {
            var genericEnumType = typeof(T);
            var memberInfo = genericEnumType.GetMember(enumVal.ToString());
            if (memberInfo.Length <= 0)
            {
                return enumVal.ToString();
            }

            var attrs = memberInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
            return attrs.Any()
                ? ((System.ComponentModel.DescriptionAttribute)attrs.ElementAt(0)).Description
                : enumVal.ToString();
        }

        public static IEnumerable<EnumValue<T>> GetAllEnumValues<T>() where T : Enum
        {
            var genericEnumType = typeof(T);
            var values = (T[])Enum.GetValues(genericEnumType);
            var result = values.Select(v => new EnumValue<T> { Value = v, Description = v.GetDescription() });
            return result;
        }
    }
}
