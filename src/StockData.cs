using Newtonsoft.Json;


namespace StockQuoteAlert
{
    // public class StockData
    // {
    //          [JsonProperty("symbol")]
    //         public string symbol { get; set; }
    //          [JsonProperty("price")]
    //         public string price  { get; set; }
    //          [JsonProperty("volume")]
    //         public string volume { get; set; }
    // }
    
    public class StockData
    {
            [JsonProperty("Global Quote")]
            public GlobalQuote globalQuote { get; set; }

            //  [JsonProperty("01. symbol")]
            // public string symbol { get; set; }

            //  [JsonProperty("05. price")]
            //  public string price { get; set; }
          
    }


    public class GlobalQuote
{
    [JsonProperty("01. symbol")]
    public string symbol { get; set; }  

     [JsonProperty("02. open")]
    public string open { get; set; }  

    [JsonProperty("03. high")]
    public string high { get; set; } 

    [JsonProperty("04. low")]
    public string low { get; set; } 

    [JsonProperty("05. price")]
    public string price { get; set; } 

    [JsonProperty("06. volume")]
    public string volume { get; set; } 

     [JsonProperty("07. latest trading day")]
    public string latest_trading_day { get; set; } 

    [JsonProperty("09. change")]
    public string change { get; set; } 

    [JsonProperty("10. change percent")]
    public string change_percent { get; set; } 
   

    // "01. symbol": "MATD3.SA",
    // "02. open": "18.8000",
    // "03. high": "18.9000",
    // "04. low": "18.1000",
    // "05. price": "18.4000",
    // "06. volume": "662600",
    // "07. latest trading day": "2021-09-30",
    // "08. previous close": "18.8000",
    // "09. change": "-0.4000",
    // "10. change percent": "-2.1277%"
}

}