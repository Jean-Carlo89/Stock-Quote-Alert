﻿using System;
using System.Timers; //create timer name space
using RestSharp; // enable request
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Globalization;

namespace Inoa
{
    class Program
    {
          public static Timer timer = new Timer(5000);//create setInterval

      HttpClient client = new HttpClient();
        
        static async Task Main(string[] args) {
            DotNetEnv.Env.Load();
           
          //  timer.Elapsed += async (sender, e) => await MyElapsedMethod(sender, e,args); 
          //   timer.AutoReset = true;
          //   timer.Enabled = true;  
          //   timer.Start();
          //   Console.Read();

          while(true){

            Console.WriteLine("Entrou no loop");
           await MyElapsedMethod(args);
           Console.WriteLine("Enviou //  inico de espera 15 s");
           await Task.Delay(TimeSpan.FromSeconds(15));
           Console.WriteLine("Fim dos 5 s");
          }
            
        }

        private async Task<ObjectTest> MyFunction(string args){
          string response = await client.GetStringAsync($"https://api.twelvedata.com/time_series?symbol={args}&interval=1min&apikey=ff4dfb66a51c4c77a59163cbb2adc5b6&source=docs");
          return JsonConvert.DeserializeObject<ObjectTest>(response);

        }

        public class ObjectTest {
            public object meta { get; set; }
            public List<Values> values { get; set; }
            public string status { get; set; }
        }
   
         public class Values {
            [JsonProperty("datetime")]
            public string datetime { get; set; }
            [JsonProperty("open")]
            public double open { get; set; }
            [JsonProperty("high")]
            public double high { get; set; }
           [JsonProperty("low")]
             public double low { get; set; }
            [JsonProperty("close")]
             public double close { get; set; }
            [JsonProperty("volume")]
             public double volume { get; set; }
        }

         private async static Task MyElapsedMethod(string[] args)
        {

             var email = new EmailSender();
            
             Program program = new Program();
          var y = await program.MyFunction(args[0]);

  
       double shouldSell;  
       double shouldBuy; 
       double.TryParse(args[1], System.Globalization.NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out shouldSell); //adjust input according to US
       double.TryParse(args[2], System.Globalization.NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out shouldBuy);
        
         var  average = ((y.values[0].high+y.values[0].low)/2);
  //System.Console.WriteLine(average);
  //return;
           if (average >shouldSell)
           {
               Console.WriteLine("Enviar email de vender");
               await email.SendEmail(args[0], "sell");
           }

            if (average < shouldBuy)
           {
               Console.WriteLine("Enviar email de comprar");
               await email.SendEmail(args[0], "buy");
           }

         
        }
    }
}
