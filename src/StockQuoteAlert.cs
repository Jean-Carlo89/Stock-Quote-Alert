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
                Console.WriteLine("Entrou no loop");
                await MyElapsedMethod(args);
                Console.WriteLine($"Enviou //  inico de espera {Environment.GetEnvironmentVariable("DELAY")} s");
                await Task.Delay(TimeSpan.FromSeconds(delay));
                Console.WriteLine($"Fim dos {delay} s");
              }while(true);
            
            }catch(Exception ex){
              System.Console.WriteLine(ex.Message);
            }
        }

        private async Task<StockData> GetStockInfo(string args){
         // var response3 = await client.GetStringAsync($"https://financialmodelingprep.com/api/v3/quote-short/{args}.SA?apikey={Environment.GetEnvironmentVariable("API_KEY")}");
         var response3 = await client.GetStringAsync($"https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol={args}.SA&apikey={Environment.GetEnvironmentVariable("API_KEY")}");
       System.Console.WriteLine(response3);
       
        // https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol=BRBI11.SA&apikey=NN9O5HXEPOUGIT9H
          //List<StockData> list = JsonConvert.DeserializeObject<List<StockData>>(response3);

          StockData obj = JsonConvert.DeserializeObject<StockData>(response3);
         
        System.Console.WriteLine(obj.globalQuote.price);
        
      
         return obj;
         
          // if(list.Count>0){
          //   return list[0];
          // }
         // return null;
        }
         private async static Task MyElapsedMethod(string[] args)
        {
        
          
          var email = new Email();
            
          StockQuote program = new StockQuote();
          var stockInfo = await program.GetStockInfo(args[0]);
          System.Console.WriteLine(stockInfo.globalQuote.price);
            
          if(stockInfo.globalQuote.price==null){
            System.Console.WriteLine("is null");
            return;
          }

            
            
            // if(stockInfo==null){
            //   return;

            // }

         
          
          var price = ParseValue(stockInfo.globalQuote.price);
          var shouldSell = ParseValue(args[1]);
          var shouldBuy = ParseValue(args[2]);
        
          System.Console.WriteLine(price);

          
        
          if (price >shouldSell)
          {
              Console.WriteLine("Send 'sell' email");
              await email.Send(email.CreateMailBody(args[0], "sell"));
          }

            if (price < shouldBuy)
          {
              Console.WriteLine("Send 'buy' email");
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
