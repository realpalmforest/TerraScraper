using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerraScraper.Data;

namespace TerraScraper.Scrapers;

public class RecipeScraper : Scraper
{
    private Dictionary<string, List<RecipeData>> recipeDatas;

    public RecipeScraper()
    {
        SetPath("Recipes");
        Command = "recipes";
        Description = "Scrapes all recips and saves them to a json file.";
    }

    public override void RunScrape(CommandCaller caller)
    {
        recipeDatas = new();

        // Get recipe data of each recipe
        foreach (Recipe recipe in Main.recipe)
        {
            if (recipe == null)
                continue;

            ScrapeRecipe(caller, recipe, ref recipeDatas);
        }

        // Write the recipes of each mod to a seperate file
        foreach (var modRecipes in recipeDatas)
        {
            string json = JsonSerializer.Serialize(modRecipes.Value.ToArray());
            File.WriteAllText(Path.Combine(recipesPath, $"{modRecipes.Key}.json"), json);
        }
    }

    public override void PostScrape(CommandCaller caller)
    {
        caller.Reply($"\nAll recipes have been succesfully saved to '{recipesPath}'", Color.LimeGreen);
        base.PostScrape(caller);
    }

    private void ScrapeRecipe(CommandCaller caller, Recipe recipe, ref Dictionary<string, List<RecipeData>> datas)
    {
        Item result = recipe.createItem;
        Item[] ingredients = recipe.requiredItem.ToArray();
        List<int> workstations = recipe.requiredTile;

        if (result == null || ingredients == null)
            return;
        if (string.IsNullOrEmpty(result.Name) || ingredients.Length == 0)
            return;


        ItemData[] ingredientDatas = new ItemData[ingredients.Length];
        for (int i = 0; i < ingredients.Length; i++)
        {
            ingredientDatas[i] = GetItemData(ingredients[i]);
        }

        ItemData[] workstationDatas = new ItemData[workstations.Count];
        for (int i = 0; i < workstations.Count; i++)
        {
            workstationDatas[i] = GetWorkstationData(workstations[i]);
        }

        RecipeData data = new RecipeData()
        {
            Result = GetItemData(result),
            Ingredients = ingredientDatas,
            Workstations = workstationDatas,
            Id = recipe.RecipeIndex
        };

        string modName = recipe.Mod == null ? "Vanilla" : recipe.Mod.Name;

        // Add the recipe data to the dictionary
        datas.TryAdd(modName, [data]);
        datas[modName].Add(data);
    }
}