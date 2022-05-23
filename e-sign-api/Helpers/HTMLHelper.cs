using e_sign_api.Models;
using System.Text.RegularExpressions;

namespace e_sign_api.Helpers
{
    public class HTMLHelper
    {
        public static string ParseComponentToHTML(ESignComponent component)
        {
            string html;

            switch (component.Type)
            {
                case "HEADER":
                    html = $"<h1 style=\"text-align:{component.Style.TextAlign};font-size:{component.Style.FontSize}\">{component.Value}</h1>";
                    break;

                case "SIGNATURE":
                    html = "<ds-signature data-ds-role=\"Signer\" />";
                    break;

                default:
                    html = $"<pre style=\"text-align:{component.Style.TextAlign};font-size:{component.Style.FontSize}\">{component.Value}</pre>";
                    break;
            }

            return html;
        }

        public static ESignComponent ParseHTMLToComponent(string html)
        {
            var tagPattern = @"\w+";
            var match = Regex.Match(html, tagPattern).Value;

            var valuePattern = @"(?<=>).*?(?=<)";
            var alignPattern = @"(?<=text-align:).*?(?=;)";
            var fontSizePattern = "(?<=font-size:).*?(?=\")";

            string valueMatch;
            string alignMatch;
            string fontSizeMatch;

            var component = new ESignComponent();
            component.Id = Guid.NewGuid().ToString();

            switch (match.ToLower())
            {
                case "h1":
                    component.Type = "HEADER";
                    
                    valueMatch = Regex.Match(html, valuePattern).Value;
                    component.Value = valueMatch;

                    alignMatch = Regex.Match(html, alignPattern).Value;
                    fontSizeMatch = Regex.Match(html, fontSizePattern).Value;
                    component.Style = new ESignComponentStyle
                    {
                        FontSize = fontSizeMatch,
                        TextAlign = alignMatch
                    };

                    break;

                case "pre":
                    component.Type = "TEXT";

                    valueMatch = Regex.Match(html, valuePattern).Value;
                    component.Value = valueMatch;

                    alignMatch = Regex.Match(html, alignPattern).Value;
                    fontSizeMatch = Regex.Match(html, fontSizePattern).Value;
                    component.Style = new ESignComponentStyle
                    {
                        FontSize = fontSizeMatch,
                        TextAlign = alignMatch
                    };

                    break;

                case "ds":
                    component.Type = "SIGNATURE";
                    component.Value = "";

                    break;

                default:
                    return null;
            }

            return component;
        }

        public static string[] GetTagsFromHTML(string html)
        {
            var pattern = @"(?:<\w+.*?>.*?<\/\w+>)||(?:<ds-signature .*?\/>)";

            var matches = Regex.Matches(html, pattern).Select(m => m.Value).ToArray();

            return matches;
        }
    }
}
