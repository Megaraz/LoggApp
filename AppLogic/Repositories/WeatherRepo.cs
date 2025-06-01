using System.Diagnostics;
using AppLogic.Models.Entities.WeatherAndAQI;
using AppLogic.Repositories.Interfaces;

namespace AppLogic.Repositories
{
    /// <summary>
    /// Repository for managing weather data, including fetching weather forecasts and geographical information using external APIs.
    /// </summary>
    public class WeatherRepo : GenericRepo<WeatherData>, IWeatherRepo
    {
        private readonly LoggAppContext _dbContext;
        private readonly string _forecastBaseUrl = $"https://api.open-meteo.com/v1/forecast";
        private readonly string _forecastHourlyParams =
            "temperature_2m,apparent_temperature,relative_humidity_2m,dew_point_2m," +
            "precipitation,rain,cloud_cover,uv_index,wind_speed_10m,pressure_msl,is_day";

        private readonly string _geoCodeBaseUrl = $"https://geocoding-api.open-meteo.com/v1/search";
        private readonly HttpClient _httpClient = new HttpClient();

        public WeatherRepo(LoggAppContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }


        
        public async Task<string> GetWeatherDataAsync(string lat, string lon, string date)
        {

            var url = _forecastBaseUrl +
                      $"?latitude={lat}&longitude={lon}" +
                      $"&hourly={_forecastHourlyParams}" +
                      $"&start_date={date}&end_date={date}" +
                      $"&timezone=auto" +
                      $"&wind_speed_unit=ms";

            return await _httpClient.GetStringAsync(url);
        }

        public async Task<string> GetGeoCodeAsync(string city)
        {
            string url = _geoCodeBaseUrl + $"?name={city}";

            return await _httpClient.GetStringAsync(url);

        }
    }
}
