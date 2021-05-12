namespace TechshopService.Shared.Extensions
{
    public static class StringExtensions
    {
        public static string JoinWith(this string str, string joinedStr, char separator) =>
            string.Join(separator, str, joinedStr);

        public static string FormatWith(this string str, params object[] args) =>
            string.Format(str, args);
    }
}
