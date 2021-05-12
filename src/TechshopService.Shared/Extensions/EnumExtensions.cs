using System;

namespace TechshopService.Shared.Extensions
{
    public static class EnumExtensions
    {
        public static TEnum ToEnum<TEnum>(this string value) where TEnum : struct =>
            (TEnum)Enum.Parse(typeof(TEnum), value);
    }
}
