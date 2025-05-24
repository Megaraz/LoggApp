using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Repositories.Interfaces
{
    public interface IWeatherRepo
    {
        Task<string> GetWeatherDataAsync(string lat, string lon, string date);

        Task<string> GetGeoCodeAsync(string city);
    }
}
