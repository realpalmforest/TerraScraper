using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraScraper.Scrapers;

public class ItemScraper
{
    private string itemsPath = Path.Combine(TerraScraper.SavePath, "Items");

    public void ScrapeAllItems(CommandCaller caller)
    {
        SoundEngine.PlaySound(SoundID.Duck);

        //if (Directory.Exists(itemsPath))
        //    Directory.Delete(itemsPath, true);

        for (int i = 0; i < ItemLoader.ItemCount; i++)
        {
            string itemName = Lang.GetItemNameValue(i);
            if (!string.IsNullOrEmpty(itemName))
            {
                if (i >= ItemID.Count)
                {
                    Item item = new Item();
                    item.SetDefaults(i);

                    if (item.ModItem != null)
                    {
                        ScrapeItem(caller, i, itemName, item.ModItem.Mod.Name);
                        continue;
                    }
                }

                ScrapeItem(caller, i, itemName, "Vanilla");
            }
        }

        caller.Reply($"\nAll items have been succesfully saved to '{itemsPath}'", Color.LimeGreen);


        SoundEngine.PlaySound(SoundID.AchievementComplete);
    }

    public void ScrapeItem(CommandCaller caller, int id, string name, string folder)
    {
        var asset = TextureAssets.Item[id];

        if (!asset.IsLoaded)
        {
            Main.instance.LoadItem(id);
        }

        Directory.CreateDirectory(Path.Combine(itemsPath, folder));

        string path = Path.Combine(itemsPath, folder, $"{TerraScraper.ValidateFilename(name)}.png");

        Texture2D texture = asset.Value;
        using (FileStream stream = File.Create(path))
        {
            texture.SaveAsPng(stream, texture.Width, texture.Height);
            stream.Close();
        }

        // caller.Reply($"Saved '{name}.png' to '{path}'", Color.Green);
    }
}