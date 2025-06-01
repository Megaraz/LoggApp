using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Repositories.Interfaces
{
    /// <summary>
    /// Interface for weather-related data retrieval, including fetching weather data by coordinates and geocoding city names.
    /// </summary>
    public interface IWeatherRepo
    {
        Task<string> GetWeatherDataAsync(string lat, string lon, string date);

        Task<string> GetGeoCodeAsync(string city);
    }
}
