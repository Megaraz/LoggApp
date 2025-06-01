using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.DTOs.Summary;
using AppLogic.Models.Entities.WeatherAndAQI;

namespace AppLogic.Services.Interfaces
{
    /// <summary>
    /// Service interface for retrieving air quality data based on geographical coordinates and date.
    /// </summary>
    public interface IAirQualityService
    {
        Task<AirQualityData> GetAirQualityDataAsync(string lat, string lon, string date);

    }
}
