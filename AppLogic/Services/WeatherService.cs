using System.Text.Json;
using AppLogic.Models;
using AppLogic.Models.DTOs.Detailed;
using AppLogic.Models.DTOs.Summary;
using AppLogic.Models.Entities.WeatherAndAQI;
using AppLogic.Repositories.Interfaces;
using AppLogic.Services.Interfaces;

namespace AppLogic.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IWeatherRepo _weatherRepo;

        public WeatherService(IWeatherRepo weatherRepo)
        {
            _weatherRepo = weatherRepo;
        }

        public async Task<GeoResultResponse> GetGeoResultAsync(string city)
        {
            Task<string> TaskResultString = _weatherRepo.GetGeoCodeAsync(city);

            return JsonSerializer.Deserialize<GeoResultResponse>(await TaskResultString)!;
        }

        public async Task<WeatherData> GetWeatherDataAsync(string lat, string lon, string date)
        {

            var weatherResultString = await _weatherRepo.GetWeatherDataAsync(lat, lon, date);

            return JsonSerializer.Deserialize<WeatherData>(weatherResultString)!;

        }

    }
}
