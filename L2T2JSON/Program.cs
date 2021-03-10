using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace L2T2JSON
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string url = "https://restcountries.eu/rest/v2/region/americas";
            var downloadJson = new WebClient().DownloadString(url);

            //При помощи Newtonsoft.JSON загрузите JSON из файла в
            //программу, получите список объектов стран
            var parseJson = JArray.Parse(downloadJson);
            //Console.WriteLine(parseJson);
            //Console.ReadKey();

            Console.WriteLine(parseJson[2]);
            Console.ReadKey();


            //Посчитайте суммарную численность по этим странам
            var sumPopulations =  parseJson.Select(x=> (int)x["population"]).Sum();
            Console.WriteLine(sumPopulations);
            Console.ReadKey();

            //Получите перечень всех валют из файла
            var currencies = parseJson
                .Select(x => x["currencies"])
                .Children()
                .Select(x => (string)x["name"])
                .OrderBy(x => x)
                .ToArray();


            foreach (var currency in currencies)
            {
                Console.WriteLine(currency);
            }

            Console.ReadKey();
        }
    }
}
