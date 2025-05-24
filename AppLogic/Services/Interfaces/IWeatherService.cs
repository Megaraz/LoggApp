using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models;
using AppLogic.Models.DTOs;
using AppLogic.Models.Weather;

namespace AppLogic.Services.Interfaces
{
    public interface IWeatherService
    {

        Task<GeoResultResponse> GetGeoResultAsync(string city);

        Task<WeatherData> GetWeatherDataAsync(string lat, string lon, string date);

        DTO_AllWeatherData ConvertToDTO(WeatherData weatherData);
    }
}
