using System.Text.Json.Serialization;

namespace UsersCrudProject
{
    public class User
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }


        [JsonPropertyName("email")]

        public string Email { get; set; }
        
        [JsonPropertyName("name")]

        public string Name { get; set; }

        [JsonPropertyName("avatar")]

        public string Image { get; set; }
    }
}