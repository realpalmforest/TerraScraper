using System;
using System.IO;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraScraper.Scrapers;

public abstract class Scraper
{
    public string SavePath { get; private set; } = TerraScraper.SavePath;
    public string Command { get; set; }

    public virtual void PreScrape(CommandCaller caller)
    {
        if (caller == null || string.IsNullOrEmpty(SavePath) || string.IsNullOrEmpty(Command))
            throw new ArgumentNullException($"{nameof(this.PreScrape)} couldn't execute because either {nameof(caller)} or a Scraper property was null.");

        Directory.CreateDirectory(SavePath);
        SoundEngine.PlaySound(SoundID.Duck);
    }

    public virtual void ScrapeAll(CommandCaller caller)
    {
        if (caller == null)
            return;
    }

    public virtual void PostScrape(CommandCaller caller)
    {

    }

    /// <summary>
    /// Sets the export path of this scraper
    /// </summary>
    /// <param name="folder">The path to the folder which which gets placed into Documnets/TerraScraper/</param>
    public void SetPath(string folder)
    {
        SavePath = Path.Combine(TerraScraper.SavePath, folder);
    }
}
