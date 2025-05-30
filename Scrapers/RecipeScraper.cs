using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ModLoader;

namespace TerraScraper.Scrapers;

public class RecipeScraper
{
    private string itemsPath = Path.Combine(TerraScraper.SavePath, "Recipes");

    public void ScrapeAllRecipes(CommandCaller caller)
    {
        foreach (Recipe recipe in Main.recipe)
        {
            ScrapeRecipe(caller, recipe);
        }
    }

    public void ScrapeRecipe(CommandCaller caller, Recipe recipe)
    {
        Item result = recipe.createItem;
        Item[] ingredients = recipe.requiredItem.ToArray();
        List<int> workstations = recipe.requiredTile;
    }
}