using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TestWebApi
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            var values = new Dictionary<string, string>
            {
               { "id", "1" },
               { "from", "2017-01-01" },
               { "to", "2018-01-01" }
            };

            var content = new FormUrlEncodedContent(values);

            var response =  client.PostAsync("http://localhost:53970/api/values", content).Result;

            var responseString =  response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(responseString);
            Console.ReadLine();
        }
    }
}
