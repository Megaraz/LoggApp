using System.Text.Json;
using AppLogic.Models;
using AppLogic.Models.DTOs.Detailed;
using AppLogic.Models.DTOs.Summary;
using AppLogic.Models.Entities.WeatherAndAQI;
using AppLogic.Repositories.Interfaces;
using AppLogic.Services.Interfaces;

namespace AppLogic.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IWeatherRepo _weatherRepo;

        public WeatherService(IWeatherRepo weatherRepo)
        {
            _weatherRepo = weatherRepo;
        }

        public async Task<GeoResultResponse> GetGeoResultAsync(string city)
        {
            Task<string> TaskResultString = _weatherRepo.GetGeoCodeAsync(city);

            return JsonSerializer.Deserialize<GeoResultResponse>(await TaskResultString)!;
        }

        public async Task<WeatherData> GetWeatherDataAsync(string lat, string lon, string date)
        {

            var weatherResultString = await _weatherRepo.GetWeatherDataAsync(lat, lon, date);

            return JsonSerializer.Deserialize<WeatherData>(weatherResultString)!;

        }

        public WeatherDataSummary ConvertToDTO(WeatherData weatherData)
        {
            // Säkerställ att HourlyBlock finns
            var block = weatherData.HourlyBlock
                        ?? throw new ArgumentNullException(nameof(weatherData.HourlyBlock));

            var units = weatherData.HourlyUnits
                        ?? throw new ArgumentNullException(nameof(weatherData.HourlyBlock));

            // Mappa varje tidpunkt + index till en HourlyWeatherData
            var hourly = block.Time
                .Select((time, i) => new WeatherDataDetailed
                {
                    Time = time.Hour,
                    Temperature2m = new Measurement<double?>
                    {
                        Value = block.Temperature2m.ElementAtOrDefault(i),
                        Unit = units.Temperature2m
                    },
                    ApparentTemperature = new Measurement<double?>
                    {
                        Value = block.ApparentTemperature.ElementAtOrDefault(i),
                        Unit = units.ApparentTemperature
                    },
                    RelativeHumidity2m = new Measurement<double?> 
                    { 
                        Value = block.RelativeHumidity2m.ElementAtOrDefault(i), 
                        Unit = units.RelativeHumidity2m 
                    },
                    DewPoint2m = new Measurement<double?>()
                    {
                        Value = block.DewPoint2m.ElementAtOrDefault(i),
                        Unit = units.DewPoint2m
                    },                    
                    Precipitation = new Measurement<double?> 
                    { 
                        Value = block.Precipitation.ElementAtOrDefault(i), 
                        Unit = units.Precipitation 
                    },
                    Rain = new Measurement<double?> 
                    { 
                        Value = block.Rain.ElementAtOrDefault(i), 
                        Unit = units.Rain 
                    },
                    CloudCover = new Measurement<double?> 
                    { 
                        Value = block.CloudCover.ElementAtOrDefault(i), 
                        Unit = units.CloudCover 
                    },
                    UvIndex = new Measurement<double?> 
                    { 
                        Value = block.UvIndex.ElementAtOrDefault(i), 
                        Unit = units.UvIndex 
                    },
                    WindSpeed10m = new Measurement<double?> 
                    { 
                        Value = block.WindSpeed10m.ElementAtOrDefault(i), 
                        Unit = units.WindSpeed10m 
                    },
                    PressureMsl = new Measurement<double?> 
                    { 
                        Value = block.PressureMsl.ElementAtOrDefault(i), 
                        Unit = units.PressureMsl 
                    } ,
                    IsDay = new Measurement<double?>()
                    {
                        Value = block.IsDay.ElementAtOrDefault(i),
                        

                    }
                })
                .ToList();

            WeatherDataSummary weatherDataSummary = new WeatherDataSummary()
            {
                WeatherDataDetails = hourly,
                AISummary = weatherData.AISummary

            };
            

            return weatherDataSummary;
        }
    }
}
