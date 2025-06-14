using Microsoft.Xna.Framework;
using System.Text;
using Terraria;
using Terraria.ModLoader;
using TerraScraper.Scrapers;

namespace TerraScraper.Components;

public class CommandHandler : ModCommand
{
    public override CommandType Type => CommandType.Chat;
    public override string Command => "scrape";
    public override string Description => "Scrapes Terraria for its assets and saved them";

    public override void Action(CommandCaller caller, string input, string[] args)
    {
        if (args.Length != 1)
        {
            ModHelp();
            return;
        }

        Scraper scraper = ScraperLoader.ScraperStack.Find(scr => scr.Command == args[0].ToLower().Trim());

        if (scraper == null)
            ModHelp();
        else scraper.ScrapeAll();
    }

    private void ModHelp()
    {
        StringBuilder help = new StringBuilder();

        foreach (Scraper scraper in ScraperLoader.ScraperStack)
        {
            help.AppendLine($"/{Command} {scraper.Command} — {scraper.Description}");
        }

        Main.NewText(help.ToString(), Color.Goldenrod);
    }
}