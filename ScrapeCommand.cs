using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace TerraScraper;

public class ScrapeCommand : ModCommand
{
    public override CommandType Type => CommandType.Chat;
    public override string Command => "scrape";
    public override string Description => "Scrapes Terraria for its assets and saved them";

    public override void Action(CommandCaller caller, string input, string[] args)
    {
        if (args.Length == 0)
        {
            caller.Reply("TerraScraper mod by PalmForest. run /scrape help for a list of supported commands.", Color.LimeGreen);
            return;
        }

        switch (args[0].ToLower().Trim())
        {
            case "help":
                ModHelp(caller);
                break;
            case "items":
                TerraScraper.ItemScraper.ScrapeAllItems(caller);
                break;
            case "recipes":
                TerraScraper.RecipeScraper.ScrapeAllRecipes(caller);
                break;
        }
    }

    private void ModHelp(CommandCaller caller)
    {
        caller.Reply("/scrape items\n/scrape recipes");
    }
}