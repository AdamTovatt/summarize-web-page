using HtmlAgilityPack;
using System.Text;
using System.Text.RegularExpressions;

namespace SummarizeWebPage
{
    /// <summary>
    /// Class to help with all your html needs
    /// </summary>
    public class HtmlHelper
    {
        /// <summary>
        /// Will get the text content of a web page
        /// </summary>
        /// <param name="url">The url of the page</param>
        /// <returns>The text on the web page</returns>
        public static async Task<string> GetWebPageTextAsync(string url)
        {
            HtmlDocument document = await (new HtmlWeb()).LoadFromWebAsync(url);
            string result = CleanWebPageContent(document.DocumentNode.InnerText, 3);
            return result;
        }

        /// <summary>
        /// Will clean the web page content string
        /// </summary>
        /// <param name="content">The string that was fetched from a web page</param>
        /// <param name="minLineLength">The minimum length of a line, if a line is shorter than this it will be removed. The unit is words</param>
        /// <returns>A cleaned string</returns>
        private static string CleanWebPageContent(string content, int minLineLength)
        {
            Dictionary<string, string> stringsToReplace = new Dictionary<string, string>()
            {
                { "&nbsp", " "} // this will make sure that "&nbsp" is replaced with a space, add more rows like this if you want to replace more things
            };

            string cleanedString = Regex.Replace(content, @"(\s)\s+", "$1"); // clean up extra spaces using regex

            foreach (string key in stringsToReplace.Keys) // replace all strings that should be replaced
            {
                cleanedString = cleanedString.Replace(key, stringsToReplace[key]);
            }

            string[] lines = cleanedString.Split('\n');

            StringBuilder stringBuilder = new StringBuilder();

            foreach (string line in lines) // remove the lines that do not contain minLineLength words
            {
                if (line.Split(" ").Length > minLineLength)
                {
                    stringBuilder.Append(line);
                }
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Will determine if a string looks like a valid url or not
        /// </summary>
        /// <param name="url">The url string to look at</param>
        /// <returns>A boolean value saying if it looks valid or not</returns>
        public static bool LooksLikeValidUrl(string? url)
        {
            if (string.IsNullOrEmpty(url))
                return false;

            try
            {
                Uri uri = new Uri(url);
            }
            catch
            {
                return false; // if there was an error we know that the url doesn't look valid, return false
            }

            return true; // if we get here the url seems to be valid
        }
    }
}
