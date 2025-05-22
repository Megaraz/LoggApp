using System.Diagnostics;

namespace AppLogic.Repositories
{
    public static class WeatherRepo
    {

        private static readonly string _forecastBaseUrl = $"https://api.open-meteo.com/v1/forecast";
        private static readonly string _forecastHourlyParams =
            "temperature_2m,apparent_temperature,relative_humidity_2m,dew_point_2m," +
            "precipitation,rain,cloud_cover,uv_index,wind_speed_10m,pressure_msl,is_day";

        private static readonly string _geoCodeBaseUrl = $"https://geocoding-api.open-meteo.com/v1/search";

        private static readonly HttpClient _httpClient = new HttpClient();
        
        public static async Task<string> GetWeatherDataAsync(string lat, string lon, string date)
        {

            var url = _forecastBaseUrl +
                      $"?latitude={lat}&longitude={lon}" +
                      $"&hourly={_forecastHourlyParams}" +
                      $"&start_date={date}&end_date={date}" +
                      $"&timezone=auto" +
                      $"&wind_speed_unit=ms";


            //var stopwatch = Stopwatch.StartNew();

            //var response = await _httpClient.GetStringAsync(url);

            //stopwatch.Stop();
            //Console.WriteLine($"Total response time: {stopwatch.ElapsedMilliseconds} ms");
            //Console.ReadKey();
            //return response;

            return await _httpClient.GetStringAsync(url);
        }

        public static async Task<string> GetGeoCodeAsync(string city)
        {
            string url = _geoCodeBaseUrl + $"?name={city}";

            //var stopwatch = Stopwatch.StartNew();

            //var response = await _httpClient.GetStringAsync(url);

            //stopwatch.Stop();
            //Console.WriteLine($"Total response time: {stopwatch.ElapsedMilliseconds} ms");
            //Console.ReadKey();
            //return response;

            return await _httpClient.GetStringAsync(url);

        }
    }
}
