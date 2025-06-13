using System;
using System.Collections.Generic;
using System.Text;
using TerraScraper.Data;

namespace TerraScraper.Utility;

public static class DataTools
{
    public static ItemData GetItemData(Item item)
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

    public static ItemData GetWorkstationData(int tileId)
    {
        Item item = new Item();

        int itemId = TileLoader.GetItemDropFromTypeAndStyle(tileId);
        item.SetDefaults(itemId);

        return GetItemData(item);
    }
}
