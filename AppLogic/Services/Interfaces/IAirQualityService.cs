using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.DTOs.Summary;
using AppLogic.Models.Weather.AirQuality;

namespace AppLogic.Services.Interfaces
{
    public interface IAirQualityService
    {
        Task<AirQualityData> GetAirQualityDataAsync(string lat, string lon, string date);

        PollenDataSummary ConvertToPollenDTO(AirQualityData airQuality);

        AirQualityDataSummary ConvertToAQDTO(AirQualityData airQuality);
    }
}
