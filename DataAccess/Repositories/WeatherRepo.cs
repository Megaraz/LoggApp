using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class WeatherRepo
    {

        private readonly string _appId = "958d5f0e3839c6548c8b6c09883c79e9";
        //private readonly string _geoCodeURL = $"http://api.openweathermap.org/geo/1.0/direct?q={input.CityName}&limit=5&appid=958d5f0e3839c6548c8b6c09883c79e9";
        public string CityName { get; set; }    


        public WeatherRepo(string cityName)
        {
            CityName = cityName;
        }

        public async Task<string> GetGeoCode()
        {
            HttpClient client = new HttpClient();
            string url = $"http://api.openweathermap.org/geo/1.0/direct?q={CityName}&limit=5&appid=958d5f0e3839c6548c8b6c09883c79e9";

            return await client.GetStringAsync(url);

        }
    }
}
