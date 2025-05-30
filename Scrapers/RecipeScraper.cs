using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerraScraper.Data;

namespace TerraScraper.Scrapers;

public class RecipeScraper
{
    private string recipesPath = Path.Combine(TerraScraper.SavePath, "Recipes");

    private List<RecipeData> recipeDatas;

    public void ScrapeAllRecipes(CommandCaller caller)
    {
        Directory.CreateDirectory(recipesPath);
        SoundEngine.PlaySound(SoundID.Duck);

        recipeDatas = new List<RecipeData>();

        foreach (Recipe recipe in Main.recipe)
        {
            ScrapeRecipe(caller, recipe);
        }

        string json = JsonSerializer.Serialize(recipeDatas);
        File.WriteAllText(Path.Combine(recipesPath, "recipes.json"), json);


        caller.Reply($"\nAll recipes have been succesfully saved to '{recipesPath}'", Color.LimeGreen);
        SoundEngine.PlaySound(SoundID.AchievementComplete);
    }

    public void ScrapeRecipe(CommandCaller caller, Recipe recipe)
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
            ingredientDatas[i] = new ItemData() { Name = ingredients[i].Name, Quantity = ingredients[i].stack };
        }

        recipeDatas.Add(new RecipeData()
        {
            Result = new ItemData() { Name = result.Name, Quantity = result.stack },
            Ingredients = ingredientDatas,
            Id = recipe.RecipeIndex
        });
    }
}