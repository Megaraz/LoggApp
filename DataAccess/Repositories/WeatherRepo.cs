using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BusinessLogic.Models;
using BusinessLogic.Models.Weather;

namespace DataAccess.Repositories
{
    public class WeatherRepo
    {

        private readonly string _forecastBaseUrl = $"https://api.open-meteo.com/v1/forecast";
        private readonly string _forecastHourlyParams =
            "temperature_2m,apparent_temperature,relative_humidity_2m,dew_point_2m," +
            "precipitation,rain,cloud_cover,uv_index,wind_speed_10m,pressure_msl,is_day";

        private readonly string _geoCodeBaseUrl = $"https://geocoding-api.open-meteo.com/v1/search";
        
        public WeatherRepo()
        {
            
        }

        public async Task<string> GetWeatherDataAsync(string lat, string lon, string date)
        {
            HttpClient client = new HttpClient();

            var url = _forecastBaseUrl +
                      $"?latitude={lat}&longitude={lon}" +
                      $"&hourly={_forecastHourlyParams}" +
                      $"&start_date={date}&end_date={date}" +
                      $"&timezone=auto";

            //Console.WriteLine("FULL URL:");
            //Console.WriteLine(url);
            //Console.ReadKey();
            //string result = await client.GetStringAsync(url);
            //Console.WriteLine(result);
            //Console.ReadLine();
            return await client.GetStringAsync(url);
        }

        public async Task<string> GetGeoCodeAsync(string city)
        {
            HttpClient client = new HttpClient();
            string url = _geoCodeBaseUrl + $"?name={city}";


            return await client.GetStringAsync(url);

        }
    }
}
