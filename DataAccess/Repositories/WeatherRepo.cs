using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BusinessLogic.Models;
using BusinessLogic.Models.Weather;

namespace DataAccess.Repositories
{
    public class WeatherRepo
    {

        private readonly string _appId = "958d5f0e3839c6548c8b6c09883c79e9";
        //private readonly string _geoCodeURL = $"http://api.openweathermap.org/geo/1.0/direct?q={input.CityName}&limit=5&appid=958d5f0e3839c6548c8b6c09883c79e9";
        public string? CityName { get; set; }
        public DayCard? DayCard { get; set; }

        public WeatherRepo()
        {
            
        }

        public WeatherRepo(string cityName)
        {
            CityName = cityName;
        }

        public WeatherRepo(DayCard dayCard)
        {
            DayCard = dayCard;
        }


        public async Task<string> GetWeatherDataAsync()
        {
            HttpClient client = new HttpClient();
            string url = $"https://api.openweathermap.org/data/3.0/onecall/day_summary?lat={DayCard.User.Lat}&lon={DayCard.User!.Lon}&date={DayCard.Date}&appid=958d5f0e3839c6548c8b6c09883c79e9";
            return await client.GetStringAsync(url);
        }

        public async Task<string> GetGeoCodeAsync()
        {
            HttpClient client = new HttpClient();
            string url = $"http://api.openweathermap.org/geo/1.0/direct?q={CityName}&limit=5&appid=958d5f0e3839c6548c8b6c09883c79e9";
            //string url = $"http://api.openweathermap.org/geo/1.0/direct?q=Ljungskile&limit=5&appid=958d5f0e3839c6548c8b6c09883c79e9";

            return await client.GetStringAsync(url);

        }
    }
}
