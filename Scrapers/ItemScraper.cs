using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
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
        if (!TextureAssets.Item[id].IsLoaded)
            Main.instance.LoadItem(id);

        Texture2D texture = TextureAssets.Item[id].Value;




        Rectangle sourceRect = new SpriteFrame(1, 1).GetSourceRectangle(texture);

        // Crop to first frame
        Color[] data = new Color[sourceRect.Width * sourceRect.Height];
        texture.GetData(0, sourceRect, data, 0, data.Length);
        Texture2D croppedTexture = new Texture2D(Main.graphics.GraphicsDevice, sourceRect.Width, sourceRect.Height);
        croppedTexture.SetData(data);


        Directory.CreateDirectory(Path.Combine(itemsPath, folder));

        string path = Path.Combine(itemsPath, folder, $"{TerraScraper.ValidateFilename(name)}.png");
        using (FileStream stream = File.Create(path))
        {
            croppedTexture.SaveAsPng(stream, texture.Width, texture.Height);
        }


        // caller.Reply($"Saved '{name}.png' to '{path}'", Color.Green);
    }
}