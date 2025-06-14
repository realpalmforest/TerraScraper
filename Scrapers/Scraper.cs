using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerraScraper.Utility;

namespace TerraScraper.Scrapers;

public abstract class Scraper
{
    public string SavePath { get; private set; } = TerraScraper.SavePath;
    public string Command { get; set; }
    public string Description { get; set; } = "Scraper module.";

    /// <summary>Runs the setup method before the main scrape process. To run full scrape please use <see cref="ScrapeAll(CommandCaller)"/></summary>
    public virtual void PreScrape(Player player)
    {
        if (player == null || string.IsNullOrEmpty(SavePath) || string.IsNullOrEmpty(Command))
            throw new ArgumentNullException($"{nameof(this.PreScrape)} couldn't execute because either the Player or a Scraper property was null.");

        Directory.CreateDirectory(SavePath);
        SoundEngine.PlaySound(SoundID.Duck);
    }

    /// <summary>Runs main scrape process. To run full scrape please use <see cref="ScrapeAll(CommandCaller)"/>.</summary>
    public virtual void RunScrape(Player player)
    {
    }

    /// <summary>Runs final cleanup after main scrape step. To run full scrape please use <see cref="ScrapeAll(CommandCaller)"/></summary>
    public virtual void PostScrape(Player player)
    {
        SoundEngine.PlaySound(SoundID.Unlock);
    }

    public void ScrapeAll(Player player)
    {
        try
        {
            this.PreScrape(player);
            this.RunScrape(player);
            this.PostScrape(player);
        }
        catch (Exception e)
        {
            PlayerTools.SendMessage(player, $"Encountered error while running scrape steps of {this.GetType().Name}:\n{e.Message}", Color.Firebrick);
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
