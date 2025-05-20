using AppLogic.Models;

namespace AppLogic.Repositories
{
    public static class AirQualityRepository
    {
        private static readonly string _baseUrl = "https://air-quality-api.open-meteo.com/v1/air-quality";
        private static readonly string _hourlyParams = "alder_pollen,birch_pollen,grass_pollen,mugwort_pollen,ragweed_pollen,uv_index,european_aqi,pm2_5,ozone,carbon_monoxide,nitrogen_dioxide,dust";

        public static DayCard? DayCard { get; set; }


        public static async Task<string> GetAirQualityDataAsync(string lat, string lon, string date)
        {
            HttpClient client = new HttpClient();

            string fullUrl = _baseUrl +
                $"?&latitude={lat}&longitude={lon}" +
                $"&hourly={_hourlyParams}" +
                $"&start_date={date}&end_date={date}";

            return await client.GetStringAsync(fullUrl);

        }
        
    }
}
