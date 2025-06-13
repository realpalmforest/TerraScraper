using System;
using System.IO;
using System.Linq;
using System.Text;
using Terraria.ModLoader;
using TerraScraper.Scrapers;

namespace TerraScraper.Utility;

public static class ScrapeHandler
{
    public static string ValidateFilename(string name, string replacement = " ")
    {
        var invalidChars = Path.GetInvalidFileNameChars();
        var builder = new StringBuilder(name.Length);

        foreach (char c in name)
        {
            if (invalidChars.Contains(c))
                builder.Append(replacement);
            else
                builder.Append(c);
        }

        return builder.ToString();
    }
}
