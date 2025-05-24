using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.DTOs;
using AppLogic.Models.Weather.AirQuality;

namespace AppLogic.Services.Interfaces
{
    public interface IAirQualityService
    {
        Task<AirQualityData> GetAirQualityDataAsync(string lat, string lon, string date);

        DTO_AllPollenData ConvertToPollenDTO(AirQualityData airQuality);

        DTO_AllAirQualityData ConvertToAQDTO(AirQualityData airQuality);
    }
}
