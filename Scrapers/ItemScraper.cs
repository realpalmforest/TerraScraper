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
        if (!TextureAssets.Item[id].IsLoaded)
            Main.instance.LoadItem(id);

        Texture2D texture = TextureAssets.Item[id].Value;

        Rectangle sourceRect;
        Color[] data;
        var animation = Main.itemAnimations[id];
        Texture2D firstFrame;

        if (animation != null)
        {
            int frameHeight = texture.Height / animation.FrameCount;
            sourceRect = new Rectangle(0, 0, texture.Width, frameHeight);
            data = new Color[sourceRect.Width * sourceRect.Height];

            texture.GetData(0, sourceRect, data, 0, data.Length);
            firstFrame = new Texture2D(Main.graphics.GraphicsDevice, sourceRect.Width, sourceRect.Height);
            firstFrame.SetData(data);
        }
        else firstFrame = texture;


        Directory.CreateDirectory(Path.Combine(itemsPath, folder));

        string path = Path.Combine(itemsPath, folder, $"{TerraScraper.ValidateFilename(name)}.png");
        using (FileStream stream = File.Create(path))
        {
            firstFrame.SaveAsPng(stream, firstFrame.Width, firstFrame.Height);
        }


        // caller.Reply($"Saved '{name}.png' to '{path}'", Color.Green);
    }
}