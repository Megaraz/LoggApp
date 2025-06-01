using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models;
using AppLogic.Models.DTOs.Detailed;
using AppLogic.Models.DTOs.Summary;
using AppLogic.Models.Entities.WeatherAndAQI;

namespace AppLogic.Services.Interfaces
{
    /// <summary>
    /// Service interface for fetching weather data and geographical information based on city names.
    /// </summary>
    public interface IWeatherService
    {
        Task<GeoResultResponse> GetGeoResultAsync(string city);
        Task<WeatherData> GetWeatherDataAsync(string lat, string lon, string date);
    }
}
