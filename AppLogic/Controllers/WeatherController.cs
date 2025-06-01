using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models;
using AppLogic.Models.DTOs.Summary;
using AppLogic.Models.Entities.WeatherAndAQI;
using AppLogic.Models.InputModels;
using AppLogic.Services.Interfaces;

namespace AppLogic.Controllers
{
    /// <summary>
    /// Controller for managing weather data and geographical information.
    /// </summary>
    public class WeatherController
    {

        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public WeatherDataSummary ConvertToDTO(WeatherData weatherData)
        {
            return new WeatherDataSummary(weatherData);
        }

        public async Task<WeatherData> GetWeatherDataAsync(string lat, string lon, string date)
        {
            return await _weatherService.GetWeatherDataAsync(lat, lon, date);
        }

        public async Task<GeoResultResponse> LocationGeoResultList(string cityName)
        {
            return await _weatherService.GetGeoResultAsync(cityName);

        }

        public async Task<GeoResultResponse> UserGeoResultList(UserInputModel input)
        {
            return await _weatherService.GetGeoResultAsync(input.CityName);

        }
    }
}
