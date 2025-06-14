using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.UI.Elements;
using TerraScraper.Scrapers;

namespace TerraScraper.UI;

public class ScraperButton : UIPanel
{
    private Scraper scraper;
    private Texture2D iconTexture;

    public ScraperButton(Scraper scraper)
    {
        this.scraper = scraper;

        SetTexture();
        CreateButton();
        OnLeftClick += (_, _) => scraper.ScrapeAll();
    }

    private void CreateButton()
    {
        Width.Set(50, 0);
        Height.Set(50, 0);
        HAlign = 0.5f;
        SetPadding(9);

        UIImage icon = new UIImage(iconTexture);
        icon.HAlign = 0.5f;
        icon.VAlign = 0.5f;
        icon.Width.Set(0, 1);
        icon.Height.Set(0, 1);
        Append(icon);
    }

    private void SetTexture()
    {
        switch (scraper.Command)
        {
            case "recipes":
                iconTexture = Main.Assets.Request<Texture2D>("Images/Item_903", AssetRequestMode.ImmediateLoad).Value;
                break;
            case "items":
                iconTexture = Main.Assets.Request<Texture2D>("Images/Item_3507", AssetRequestMode.ImmediateLoad).Value;
                break;
            default:
                iconTexture = Main.Assets.Request<Texture2D>("Images/Item_4016", AssetRequestMode.ImmediateLoad).Value;
                break;
        }
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        base.DrawSelf(spriteBatch);
        // If this code is in the panel or container element, check it directly
        if (ContainsPoint(Main.MouseScreen))
        {
            Main.LocalPlayer.mouseInterface = true;
        }
        // Otherwise, we can check a child element instead
        if (ContainsPoint(Main.MouseScreen))
        {
            Main.LocalPlayer.mouseInterface = true;
        }

        if (IsMouseHovering)
        {
            Main.hoverItemName = "Scrape " + scraper.Command;
        }
    }
}
