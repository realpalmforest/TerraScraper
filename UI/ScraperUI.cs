﻿using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using TerraScraper.Components;
using TerraScraper.Scrapers;
using Microsoft.Xna.Framework;

namespace TerraScraper.UI;
public class ScraperUI : UIState
{

    public override void OnInitialize()
    {
        UIPanel panel = new UIPanel();
        
        CreatePanel(panel);
        CreateButtons(panel);
    }

    private void CreatePanel(UIPanel panel)
    {
        panel.Width.Set(70, 0);
        panel.Height.Set((ScraperLoader.ScraperStack.Count * 60) + 10, 0);
        panel.HAlign = 0.74f;
        panel.Top.Set(30, 0);
        panel.SetPadding(0);

        panel.BorderColor = new Color(0, 0, 0, 0);
        panel.BackgroundColor = new Color(0, 0, 0, 0);

        Append(panel);
    }

    private void CreateButtons(UIPanel panel)
    {
        foreach (Scraper scraper in ScraperLoader.ScraperStack)
        {
            ScraperButton button = new ScraperButton(scraper);
            button.Top.Set(ScraperLoader.ScraperStack.IndexOf(scraper) * 60 + 10, 0);

            panel.Append(button);
        }
    }
}