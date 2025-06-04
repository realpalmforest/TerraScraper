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

        if (result == null || ingredients == null)
            return;
        if (string.IsNullOrEmpty(result.Name) || ingredients.Length == 0)
            return;


        ItemData[] ingredientDatas = new ItemData[ingredients.Length];
        for (int i = 0; i < ingredients.Length; i++)
        {
            ingredientDatas[i] = GetItemData(ingredients[i]);
        }

        //string[] workstations = new string[recipe.requiredTile.Count];
        //for (int i = 0; i < recipe.requiredTile.Count; i++)
        //{

        //}

        recipeDatas.Add(new RecipeData()
        {
            Result = GetItemData(result),
            Ingredients = ingredientDatas,

            Id = recipe.RecipeIndex
        });
    }

    private static ItemData GetItemData(Item item)
    {
        StringBuilder tooltip = new StringBuilder();

        for (int j = 0; j < item.ToolTip.Lines; j++)
        {
            if (j == item.ToolTip.Lines - 1)
            {
                tooltip.Append(item.ToolTip.GetLine(j));
                break;
            }

            tooltip.AppendLine(item.ToolTip.GetLine(j));
        }

        return new ItemData() { Name = item.Name, Tooltip = tooltip.ToString(), Quantity = item.stack };
    }
}