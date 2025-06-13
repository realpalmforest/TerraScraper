using System;
using System.Collections.Generic;
using System.Text;
using TerraScraper.Scrapers;

namespace TerraScraper.Components;

public static class ScraperLoader
{
    public static ItemScraper ItemScraper { get; private set; }
    public static RecipeScraper RecipeScraper { get; private set; }

    public static List<Scraper> ScraperStack { get; }

    public static void LoadScrapers()
    {
        ItemScraper = new ItemScraper();
        RecipeScraper = new RecipeScraper();

        ScraperStack = new()
        {
            ItemScraper,
            RecipeScraper
        };
    }
}
