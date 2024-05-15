using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JokeApp
{
    class JokeDTO
    {
        
        [JsonPropertyName("setup")]
        public string JokeSetup {  get; set; }

        [JsonPropertyName("delivery")]
        public string JokeDelivery { get; set; }

        [JsonPropertyName("error")]
        public bool APIError { get; set; }


        [JsonPropertyName("joke")]
        public string Joke {  get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

    }
}
