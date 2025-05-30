using System.Text.Json.Serialization;

namespace TerraScraper.Data;

public class RecipeData
{
    [JsonPropertyName("result")]
    public RecipeData Result { get; set; }

    [JsonPropertyName("ingredients")]
    public RecipeData[] Ingredients { get; set; }
}
