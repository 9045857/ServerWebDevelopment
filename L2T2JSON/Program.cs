using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;

namespace L2T2JSON
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string url = "https://restcountries.eu/rest/v2/region/americas";

            var downloadJson = new WebClient().DownloadString(url);
            var parseJson = JArray.Parse(downloadJson);

            //Посчитайте суммарную численность по этим странам
            var totalPopulation = parseJson.Select(x => (int)x["population"]).Sum();

            Console.WriteLine($"Суммарная численность насления стран: {totalPopulation}.");
            Console.WriteLine();

            //Получите перечень всех валют из файла
            var currencies = parseJson
                .Select(x => x["currencies"])
                .Children()
                .Select(x => (string)x["name"])
                .OrderBy(x => x)
                .ToArray();

            Console.WriteLine($"Перечень валют стран в алфовитном порядке:");
            foreach (var currency in currencies)
            {
                Console.WriteLine(currency);
            }

            Console.ReadKey();
        }
    }
}
