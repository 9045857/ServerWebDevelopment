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

            //Посчитайте суммарную численность по этим странам
            var sumPopulations =  parseJson.Select(x=> (int)x[""]);


            //Получите перечень всех валют из файла


        }
    }
}
