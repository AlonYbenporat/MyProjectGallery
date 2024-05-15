using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tradecalculator
{
    class CurrencyInfo
    {
        [JsonPropertyName("ticker")]
        public string Ticker { get; set; }


        [JsonPropertyName("name")]
        public string Name { get; set; }

        //[JsonPropertyName("market")]
        //public string Market { get; set; }

        //[JsonPropertyName("locale")]
        //public string Locale { get; set; }

        //[JsonPropertyName("active")]
        //public bool Active { get; set; }


        //[JsonPropertyName("currency_symbol")]
        //public string CurrencySymbol { get; set; }

        //[JsonPropertyName("currency_name")]
        //public string CurrencyName { get; set; }

        //[JsonPropertyName("base_currency_symbol")]
        //public string BaseCurrencySymbol { get; set; }

        //[JsonPropertyName("base_currency_name")]
        //public string base_currency_name { get; set; }

        [JsonPropertyName("last_updated_utc")]
        public DateTime LastUpdatedUtc { get; set; }
    }
}
