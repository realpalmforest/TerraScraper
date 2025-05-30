using System;
using System.IO;
using System.Linq;
using System.Text;
using Terraria.ModLoader;
using TerraScraper.Scrapers;

namespace TerraScraper;

// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
public class TerraScraper : Mod
{
    public static readonly string SavePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TerraScraper");

    public static ItemScraper ItemScraper = new ItemScraper();
    public static RecipeScraper RecipeScraper = new RecipeScraper();

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