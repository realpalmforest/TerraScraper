using Microsoft.Xna.Framework;
using System;
using Terraria.ModLoader;

namespace TerraScraper.Utility;

public class CommandHandler : ModCommand
{
    public override CommandType Type => CommandType.Chat;
    public override string Command => "scrape";
    public override string Description => "Scrapes Terraria for its assets and saved them";

    public Dictionary<string, Scraper>

    public override void Action(CommandCaller caller, string input, string[] args)
    {
        if (args.Length == 0)
        {
            caller.Reply("TerraScraper mod by PalmForest. run /scrape help for a list of supported commands.", Color.LimeGreen);
            return;
        }

        try
        {
            switch (args[0].ToLower().Trim())
            {
                case "items":
                    TerraScraper.ItemScraper.ScrapeAllItems(caller);
                    break;
                case "recipes":
                    TerraScraper.RecipeScraper.ScrapeAllRecipes(caller);
                    break;
                default:
                    ModHelp(caller);
                    break;
            }
        }
        catch (Exception e)
        {
            caller.Reply("An error occured: " + e.Message);
        }

    }

    private void ModHelp(CommandCaller caller)
    {
        caller.Reply("/scrape items\n/scrape recipes");
    }
}