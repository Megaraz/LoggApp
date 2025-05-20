using System.Text.Json;
using AppLogic.Models.DTOs;
using AppLogic.Models.Weather;
using AppLogic.Repositories;

namespace AppLogic.Services
{
    public static class WeatherService
    {

        public static async Task<WeatherData> GetWeatherDataAsync(string lat, string lon, string date)
        {

            var weatherResultString = await WeatherRepo.GetWeatherDataAsync(lat, lon, date);



            return JsonSerializer.Deserialize<WeatherData>(weatherResultString)!;

        }

        public static DTO_AllWeatherData ConvertToDTO(WeatherData weatherData)
        {
            // Säkerställ att HourlyBlock finns
            var block = weatherData.HourlyBlock
                        ?? throw new ArgumentNullException(nameof(weatherData.HourlyBlock));

            // Mappa varje tidpunkt + index till en HourlyWeatherData
            var hourly = block.Time
                .Select((time, i) => new HourlyWeatherData
                {
                    Time = time.Hour,  
                    Temperature2m = block.Temperature2m.ElementAtOrDefault(i),
                    ApparentTemperature = block.ApparentTemperature.ElementAtOrDefault(i),
                    RelativeHumidity2m = block.RelativeHumidity2m.ElementAtOrDefault(i),
                    DewPoint2m = block.DewPoint2m.ElementAtOrDefault(i),
                    Precipitation = block.Precipitation.ElementAtOrDefault(i),
                    Rain = block.Rain.ElementAtOrDefault(i),
                    CloudCover = block.CloudCover.ElementAtOrDefault(i),
                    UvIndex = block.UvIndex.ElementAtOrDefault(i),
                    WindSpeed10m = block.WindSpeed10m.ElementAtOrDefault(i),
                    PressureMsl = block.PressureMsl.ElementAtOrDefault(i),
                    IsDay = block.IsDay.ElementAtOrDefault(i)
                })
                .ToList();

            return new DTO_AllWeatherData
            {
                HourlyWeatherData = hourly
            };
        }
    }
}
