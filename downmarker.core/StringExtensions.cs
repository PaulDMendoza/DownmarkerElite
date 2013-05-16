using System.Text.RegularExpressions;

namespace DownMarker.Core
{
    public static class StringExtensions
    {
        public static string ReplaceSubstring(
            this string s, int startIndex, int length, string replacement)
        {
            string before = s.Substring(0, startIndex);
            string after = s.Substring(startIndex + length);
            return before + replacement + after;
        }

        public static string NormalizeToUnixLineEndings(this string s)
        {
            return Regex.Replace(s, @"\r\n|\r", "\n");
        }
    }
}
