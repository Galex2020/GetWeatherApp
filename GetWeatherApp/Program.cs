using System;
using System.Net.Http;
using Flurl.Http;
using Newtonsoft.Json;
using static System.Console;

namespace GetWeatherApp
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            string url = "http://api.angara.net/meteo/st/";

            var result = $"{url}near?ll=104.3297685,52.2797803&limit=5".GetAsync().ReceiveJson().Result;

            WriteLine("Где хотите узнать погду?");

            for (int i = 0; i < 5; i++)
                WriteLine($"{i + 1} - " + result.data[i].addr);

            string stantionId;

            int choice = int.Parse(ReadLine());
            stantionId = result.data[choice - 1].last.st;

            var result1 = $"{url}info?st={stantionId}".GetAsync().ReceiveJson().Result;
            WriteLine($"Сейчас на улице {result1.data[0].trends.t.avg:#.#}°С");

            ReadKey(true);
        }
    }
}
