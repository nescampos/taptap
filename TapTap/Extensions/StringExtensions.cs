using System.Text;

namespace TapTap.Extensions
{
    public static class StringExtensions
    {
        public static string ToHex(this string input)
        {
            byte[] bytes = Encoding.Default.GetBytes(input);
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}
