using PdfSharp.Fonts;

namespace LevendMonopoly.Api.Services
{
    public class RobotoFontResolver : IFontResolver
    {
        private Dictionary<string, string> _fontFiles = new Dictionary<string, string>
            {
                { "Roboto", "Roboto-Regular.ttf" },
                { "Roboto|Bold", "Roboto-Bold.ttf" },
                { "Roboto|Italic", "Roboto-Italic.ttf" },
                { "Roboto|Bold|Italic", "Roboto-BoldItalic.ttf" }
            };

        public byte[]? GetFont(string faceName)
        {
            var fileName = _fontFiles[faceName];
            return new System.Net.WebClient().DownloadData("https://github.com/openmaptiles/fonts/raw/refs/heads/master/roboto/" + fileName);
        }

        public FontResolverInfo? ResolveTypeface(string familyName, bool bold, bool italic)
        {
            if (familyName != "Roboto") return null; 
            if (bold)
            {
                if (italic)
                {
                    return new FontResolverInfo("Roboto|Bold|Italic");
                }
                else
                {
                    return new FontResolverInfo("Roboto|Bold");
                }
            }
            else
            {
                if (italic)
                {
                    return new FontResolverInfo("Roboto|Italic");
                }
                else
                {
                    return new FontResolverInfo("Roboto");
                }
            }
        }
    }
}
