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
    public string Description { get; set; } = "Scraper module.";

    /// <summary>Runs the setup method before the main scrape process. To run full scrape please use <see cref="ScrapeAll(CommandCaller)"/></summary>
    public virtual void PreScrape(CommandCaller caller)
    {
        if (caller == null || string.IsNullOrEmpty(SavePath) || string.IsNullOrEmpty(Command))
            throw new ArgumentNullException($"{nameof(this.PreScrape)} couldn't execute because either {nameof(caller)} or a Scraper property was null.");

        Directory.CreateDirectory(SavePath);
        SoundEngine.PlaySound(SoundID.Duck);
    }

    /// <summary>Runs main scrape process. To run full scrape please use <see cref="ScrapeAll(CommandCaller)"/>.</summary>
    public virtual void RunScrape(CommandCaller caller)
    {
    }

    /// <summary>Runs final cleanup after main scrape step. To run full scrape please use <see cref="ScrapeAll(CommandCaller)"/></summary>
    public virtual void PostScrape(CommandCaller caller)
    {
        SoundEngine.PlaySound(SoundID.Unlock);
    }

    public void ScrapeAll(CommandCaller caller)
    {
        try
        {
            this.PreScrape(caller);
            this.RunScrape(caller);
            this.PostScrape(caller);
        }
        catch (Exception e)
        {
            caller.Reply($"Encountered error while running scrape steps of {this.GetType().Name}:\n{e.Message}");
        }
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
