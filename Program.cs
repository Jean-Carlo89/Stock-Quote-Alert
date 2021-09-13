using System;
using System.Timers; //create timer name space
using RestSharp; // enable request
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Inoa
{
    class Program
    {
        public static int seconds =0;
        public string api = "JYH46XOQTRSPVY8Y";

        public string twelveDataApiKey = "ff4dfb66a51c4c77a59163cbb2adc5b6";
      public static Timer timer = new Timer(5000);//create setInterval

      HttpClient client = new HttpClient();
        
        static void Main(string[] args) {
        // {    Program program = new Program();
        //       await program.MyFunction();
            
            
            timer.Elapsed += (sender, e) => MyElapsedMethod(sender, e,args); 
            timer.AutoReset = true;
            timer.Enabled = true;  
            timer.Start();
            Console.Read();
             //string url = "https://api.twelvedata.com/time_series?symbol=PETR4&interval=1min&apikey=ff4dfb66a51c4c77a59163cbb2adc5b6&source=docs";

            // var client = new RestClient(url);

            // var request = new RestRequest();

            // var response = client.Get(request);
            //  Console.WriteLine();

            // //Console.WriteLine(response.Content[1].ToString());
            // Console.Read();

            


        }

        private async Task<ObjectTest> MyFunction(string args){
            string response = await client.GetStringAsync($"https://api.twelvedata.com/time_series?symbol={args}&interval=1min&apikey=ff4dfb66a51c4c77a59163cbb2adc5b6&source=docs");
        
         Console.WriteLine("chegou aqui");

          return JsonConvert.DeserializeObject<ObjectTest>(response);
          
            //  Console.WriteLine(x.values);
            // Console.WriteLine(x.values[0].datetime);
            // Console.WriteLine(x.values[0].open);
            // Console.WriteLine(x.values[0].high);
            // Console.WriteLine(x.values[0].low);
            // Console.WriteLine(x.values[0].volume);  
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

         private async static void MyElapsedMethod(object sender, ElapsedEventArgs e, string[] args)
        {
            Program program = new Program();
           var y = await program.MyFunction(args[0]);
          var sell = Double.Parse(args[1]);
          var buy =  Double.Parse(args[2]);

           Console.WriteLine(y.values[0].high);
           Console.WriteLine(y.values[0].low);
           
           if (y.values[0].high > sell)
           {
               Console.WriteLine("Enviar email de vender");
           }

            if (y.values[0].low < buy)
           {
               Console.WriteLine("Enviar email de comprar");
           }

           
          
           

        
         
        }
    }
}
