namespace AppLogic.Repositories
{
    public static class WeatherRepo
    {

        private static readonly string _forecastBaseUrl = $"https://api.open-meteo.com/v1/forecast";
        private static readonly string _forecastHourlyParams =
            "temperature_2m,apparent_temperature,relative_humidity_2m,dew_point_2m," +
            "precipitation,rain,cloud_cover,uv_index,wind_speed_10m,pressure_msl,is_day";

        private static readonly string _geoCodeBaseUrl = $"https://geocoding-api.open-meteo.com/v1/search";
        
        public static async Task<string> GetWeatherDataAsync(string lat, string lon, string date)
        {
            HttpClient client = new HttpClient();

            var url = _forecastBaseUrl +
                      $"?latitude={lat}&longitude={lon}" +
                      $"&hourly={_forecastHourlyParams}" +
                      $"&start_date={date}&end_date={date}" +
                      $"&timezone=auto";

            return await client.GetStringAsync(url);
        }

        public static async Task<string> GetGeoCodeAsync(string city)
        {
            HttpClient client = new HttpClient();
            string url = _geoCodeBaseUrl + $"?name={city}";


            return await client.GetStringAsync(url);

        }
    }
}
