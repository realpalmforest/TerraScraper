using Microsoft.Xna.Framework;
using System.Text;
using Terraria;
using Terraria.ModLoader;
using TerraScraper.Scrapers;
using TerraScraper.Utility;

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
            ModHelp(caller.Player);
            return;
        }

        Scraper scraper = ScraperLoader.ScraperStack.Find(scr => scr.Command == args[0].ToLower().Trim());

        if (scraper == null)
            ModHelp(caller.Player);
        else scraper.ScrapeAll(caller.Player);
    }

    private void ModHelp(Player player)
    {
        StringBuilder help = new StringBuilder();

        foreach (Scraper scraper in ScraperLoader.ScraperStack)
        {
            help.AppendLine($"/{Command} {scraper.Command} — {scraper.Description}");
        }

        PlayerTools.SendMessage(player, help.ToString(), Color.Goldenrod);
    }
}