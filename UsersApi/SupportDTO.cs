using System.Text.Json.Serialization;

public class SupportDTO
{
    [JsonPropertyName("url")]
    public string URL { get; set; }
    [JsonPropertyName("text")]

    public string Text { get; set; }
}