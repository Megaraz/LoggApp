using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Repositories.Interfaces
{
    public interface IAirQualityRepo
    {
        Task<string> GetAirQualityDataAsync(string lat, string lon, string date);
    }
}
