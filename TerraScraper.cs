using System;
using System.IO;
using Terraria.ModLoader;
using TerraScraper.Scrapers;

namespace TerraScraper;

// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
public class TerraScraper : Mod
{
    public static readonly string SavePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TerraScraper");

    public TerraScraper() : base()
    {

    }

    public static ItemScraper ItemScraper = new ItemScraper();
    public static RecipeScraper RecipeScraper = new RecipeScraper();
}