using System;
using Flurl.Http;
using Flurl;
using static System.Console;
using GetWeatherApp.Models;
using System.Linq;

// Add comment

namespace GetWeatherApp
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            string url = "http://api.angara.net/meteo/st/";

            //var result = $"{url}near?ll=104.3297685,52.2797803&limit=10".GetAsync().ReceiveJson().Result;
            var result = url
                .AppendPathSegment("near")
                .SetQueryParams(new
                {
                    ll = "104.3297685,52.2797803",
                })
                .GetAsync()
                .ReceiveJson<WeatherModel>()
                .Result;

            WriteLine("Где хотите узнать погду?");


            var strList = result.data.Where(x => !string. IsNullOrWhiteSpace(x.addr) || !string.IsNullOrWhiteSpace(x.descr)).ToList();
            for (int sti = 0; sti < strList.Count(); ++sti)
                WriteLine($"{sti + 1} - " + (result.data[sti].addr ?? result.data[sti].descr));
            //for (var i = 0; i < result.data.Length; i++)
            //{
            //    if (result.data[i].addr != null)
            //        WriteLine($"{i + 1} - " + result.data[i].addr);
            //}

            int choice = int.Parse(ReadLine());

            //var result1 = $"{url}info?st={stantionId[choice]}".GetAsync().ReceiveJson().Result;
            var result1 = url
                .AppendPathSegment("info")
                .SetQueryParam("st", strList[choice - 1])
                .GetAsync()
                .ReceiveJson<WeatherModel>()
                .Result;
            var t = result1.data.FirstOrDefault()?.last.t.ToString() ?? "неизвестно";
            WriteLine($"Сейчас на улице {t}°С");

            ReadKey(true);
        }
    }
}
