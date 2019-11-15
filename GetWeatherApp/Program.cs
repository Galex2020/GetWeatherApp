using System;
using Flurl.Http;
using static System.Console;

namespace GetWeatherApp
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            string url = "http://api.angara.net/meteo/st/";

            var result = $"{url}near?ll=104.3297685,52.2797803&limit=10".GetAsync().ReceiveJson().Result;

            WriteLine("Где хотите узнать погду?");

            var stantionId = new string[10];
            int k = 1;

            for (var i = 0; i < 10; i++)
                try
                {
                    WriteLine($"{k} - " + result.data[i].addr);
                    stantionId[k] = result.data[i].last.st;
                    k++;
                }
                catch (Exception)
                {
                }

            int choice = int.Parse(ReadLine());

            var result1 = $"{url}info?st={stantionId[choice]}".GetAsync().ReceiveJson().Result;
            WriteLine($"Сейчас на улице {result1.data[0].trends.t.avg:#.#}°С");

            ReadKey(true);
        }
    }
}
