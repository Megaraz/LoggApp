using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AppLogic.Models.DTOs;
using AppLogic.Models;
using BusinessLogic.Models.Weather;
using DataAccess.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppLogic.Services
{
    public static class WeatherService
    {

        public static async Task<WeatherData> GetWeatherDataAsync(string lat, string lon, string date)
        {

            var weatherResultString = await WeatherRepo.GetWeatherDataAsync(lat, lon, date);



            return JsonSerializer.Deserialize<WeatherData>(weatherResultString)!;

        }
    }
}
