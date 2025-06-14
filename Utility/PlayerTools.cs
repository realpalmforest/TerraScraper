using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.Localization;

namespace TerraScraper.Utility;

public static class PlayerTools
{
    public static void SendMessage(Player player, string message, Color color)
    {
        ChatHelper.SendChatMessageToClient(NetworkText.FromLiteral(message), color, player.whoAmI);
    }
}
