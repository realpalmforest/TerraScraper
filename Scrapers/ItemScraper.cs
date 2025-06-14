using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using TerraScraper.Utility;

namespace TerraScraper.Scrapers;

public class ItemScraper : Scraper
{
    public ItemScraper()
    {
        SetPath("Items");
        Command = "items";
        Description = "Scrapes all the game's items for their textures and saves them as PNGs.";
    }

    public override void RunScrape(Player player)
    {
        for (int i = 0; i < ItemLoader.ItemCount; i++)
        {
            string itemName = Lang.GetItemNameValue(i);

            if (string.IsNullOrEmpty(itemName))
                continue;

            if (i >= ItemID.Count)
            {
                Item item = new Item();
                item.SetDefaults(i);

                if (item.ModItem != null)
                {
                    ScrapeItem(i, itemName, item.ModItem.Mod.Name);
                    continue;
                }
            }

            ScrapeItem(i, itemName, "Vanilla");
        }
    }

    public override void PostScrape(Player player)
    {
        PlayerTools.SendMessage(player, $"\nAll items have been succesfully saved to '{SavePath}'", Color.LimeGreen);
        base.PostScrape(player);
    }

    private void ScrapeItem(int id, string name, string folder)
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


        Directory.CreateDirectory(Path.Combine(SavePath, folder));

        string path = Path.Combine(SavePath, folder, $"{FileTools.ValidateFilename(name)}.png");
        using (FileStream stream = File.Create(path))
        {
            firstFrame.SaveAsPng(stream, firstFrame.Width, firstFrame.Height);
        }
    }
}