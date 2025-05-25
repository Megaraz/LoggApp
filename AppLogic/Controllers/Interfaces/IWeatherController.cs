using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models;
using AppLogic.Models.DTOs.Summary;
using AppLogic.Models.Weather;

namespace AppLogic.Controllers.Interfaces
{
    public interface IWeatherController
    {
        Task<WeatherData> GetWeatherDataAsync(string lat, string lon, string date);
        Task<GeoResultResponse> LocationGeoResultList(string cityName);
        Task<GeoResultResponse> UserGeoResultList(UserInputModel input);
        WeatherDataSummary ConvertToDTO(WeatherData weatherData);
    }
}
