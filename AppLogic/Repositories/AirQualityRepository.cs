using System.Diagnostics;
using AppLogic.Models;

namespace AppLogic.Repositories
{
    public static class AirQualityRepository
    {
        private static readonly string _baseUrl = "https://air-quality-api.open-meteo.com/v1/air-quality";
        private static readonly string _hourlyParams = "alder_pollen,birch_pollen,grass_pollen,mugwort_pollen,ragweed_pollen,uv_index,european_aqi,pm2_5,ozone,carbon_monoxide,nitrogen_dioxide,dust";

        private static readonly HttpClient _httpClient = new HttpClient();
        public static DayCard? DayCard { get; set; }


        public static async Task<string> GetAirQualityDataAsync(string lat, string lon, string date)
        {
            
            string fullUrl = _baseUrl +
                $"?&latitude={lat}&longitude={lon}" +
                $"&hourly={_hourlyParams}" +
                $"&start_date={date}&end_date={date}";

            //var stopwatch = Stopwatch.StartNew();

            //var response = await _httpClient.GetStringAsync(fullUrl);

            //stopwatch.Stop();
            //Console.WriteLine($"Total response time: {stopwatch.ElapsedMilliseconds} ms");
            //Console.ReadKey();
            //return response;
            return await _httpClient.GetStringAsync(fullUrl);

        }
        
    }
}
