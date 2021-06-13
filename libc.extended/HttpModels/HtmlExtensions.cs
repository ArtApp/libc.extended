namespace libc.extended.HttpModels
{
    public static class HtmlExtensions
    {
        private static readonly System.Text.RegularExpressions.Regex regex =
            new System.Text.RegularExpressions.Regex(@"\r\n?|\n");

        public static string ToHtmlLineBreaks(this string str)
        {
            return string.IsNullOrWhiteSpace(str) ? str : regex.Replace(str, "<br/>");
        }
    }
}