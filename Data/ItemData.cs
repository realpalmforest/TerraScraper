using System.Text.Json.Serialization;

namespace TerraScraper.Data;

public class ItemData
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("tooltip")]
    public string Tooltip { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
}
