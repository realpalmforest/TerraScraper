using System.Text.Json.Serialization;

namespace TerraScraper.Data;

public class RecipeData
{
    [JsonPropertyName("result")]
    public ItemData Result { get; set; }

    [JsonPropertyName("ingredients")]
    public ItemData[] Ingredients { get; set; }

    [JsonPropertyName("workstations")]
    public ItemData[] Workstations { get; set; }

    [JsonPropertyName("id")]
    public int Id { get; set; }
}
