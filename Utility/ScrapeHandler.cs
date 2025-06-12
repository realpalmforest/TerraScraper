using System;
using System.IO;
using System.Linq;
using System.Text;
using Terraria.ModLoader;
using TerraScraper.Scrapers;

namespace TerraScraper.Utility;

public static class ScrapeHandler
{
    public static void RunScraper(Scraper scraper, CommandCaller caller)
    {
        try
        {
            scraper.PreScrape(caller);
            scraper.ScrapeAll(caller);
            scraper.PostScrape(caller);
        }
        catch (Exception e)
        {
            caller.Reply($"Encountered error within {scraper.GetType().Name}:\n{e.Message}");
        }
    }

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
