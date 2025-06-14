using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Terraria;
using TerraScraper.Data;
using TerraScraper.Utility;

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

    public override void RunScrape()
    {
        recipeDatas = new();

        // Get recipe data of each recipe
        foreach (Recipe recipe in Main.recipe)
        {
            if (recipe == null)
                continue;

            ScrapeRecipe(recipe, ref recipeDatas);
        }

        // Write the recipes of each mod to a seperate file
        foreach (var modRecipes in recipeDatas)
        {
            string json = JsonSerializer.Serialize(modRecipes.Value.ToArray());
            File.WriteAllText(Path.Combine(SavePath, $"{modRecipes.Key}.json"), json);
        }
    }

    public override void PostScrape()
    {
        Main.NewText($"All recipes have been succesfully saved to '{SavePath}'", Color.LimeGreen);
        base.PostScrape();
    }

    private void ScrapeRecipe(Recipe recipe, ref Dictionary<string, List<RecipeData>> datas)
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
            ingredientDatas[i] = DataTools.GetItemData(ingredients[i]);
        }

        ItemData[] workstationDatas = new ItemData[workstations.Count];
        for (int i = 0; i < workstations.Count; i++)
        {
            workstationDatas[i] = DataTools.GetWorkstationData(workstations[i]);
        }

        RecipeData data = new RecipeData()
        {
            Result = DataTools.GetItemData(result),
            Ingredients = ingredientDatas,
            Workstations = workstationDatas,
            Id = recipe.RecipeIndex
        };

        string modName = recipe.Mod == null ? "Vanilla" : recipe.Mod.Name;

        // Add the recipe data to the dictionary
        if (!datas.TryAdd(modName, [data]))
        {
            datas[modName].Add(data);
        }
    }
}