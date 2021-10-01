using System;
using System.Timers; //create timer name space
using RestSharp; // enable request
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Globalization;

namespace StockQuoteAlert
{
   public class StockQuote
    {
     
        HttpClient client = new HttpClient();
        static async Task Main(string[] args) {
              
            DotNetEnv.Env.Load();
            
            int delay;
            int.TryParse(Environment.GetEnvironmentVariable("DELAY"), System.Globalization.NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out delay);
              
            try{
             
              do{
                await MyElapsedMethod(args);
                await Task.Delay(TimeSpan.FromSeconds(delay));
              }while(true);
            
            }catch(Exception ex){
              System.Console.WriteLine(ex.Message);
            }
        }

        private async Task<StockData> GetStockInfo(string args){
        
            var response = await client.GetStringAsync($"https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol={args}.SA&apikey={Environment.GetEnvironmentVariable("API_KEY")}");
            
            StockData obj = JsonConvert.DeserializeObject<StockData>(response);
              
            return obj;
        }
         private async static Task MyElapsedMethod(string[] args)
        {
        
          
              var email = new Email();
                
              StockQuote program = new StockQuote();
              var stockInfo = await program.GetStockInfo(args[0]);
             
              if(stockInfo.globalQuote.price==null){
                System.Console.WriteLine($"No price reference was found for the stock '{args[0]}'");
                return;
              }
             
              
              var price = ParseValue(stockInfo.globalQuote.price);
              var shouldSell = ParseValue(args[1]);
              var shouldBuy = ParseValue(args[2]);
            
              if (price >shouldSell)
              {
                  await email.Send(email.CreateMailBody(args[0], "sell"));
              }

                if (price < shouldBuy)
              {
                  await email.Send(email.CreateMailBody(args[0], "buy"));
              }
        }

       public static Double ParseValue(string value){
           double parsedValue;
           double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out parsedValue); //adjust input according to US
          
           return parsedValue;
        }
    }
}
