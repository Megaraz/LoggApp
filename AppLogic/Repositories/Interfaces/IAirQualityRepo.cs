using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Repositories.Interfaces
{
    /// <summary>
    /// Interface for interacting with air quality data repositories.
    /// </summary>
    public interface IAirQualityRepo
    {
        Task<string> GetAirQualityDataAsync(string lat, string lon, string date);
    }
}
