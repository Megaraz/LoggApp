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
    public interface IWeatherService
    {

        Task<GeoResultResponse> GetGeoResultAsync(string city);

        Task<WeatherData> GetWeatherDataAsync(string lat, string lon, string date);

        WeatherDataSummary ConvertToDTO(WeatherData weatherData);
    }
}
