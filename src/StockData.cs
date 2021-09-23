using Newtonsoft.Json;


namespace StockQuoteAlert
{
    public class StockData
    {
             [JsonProperty("symbol")]
            public string symbol { get; set; }
             [JsonProperty("price")]
            public string price  { get; set; }
             [JsonProperty("volume")]
            public string volume { get; set; }
    }
}