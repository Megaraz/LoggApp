using System.Diagnostics;
using AppLogic.Models;
using AppLogic.Models.Weather.AirQuality;
using AppLogic.Repositories.Interfaces;

namespace AppLogic.Repositories
{
    public class AirQualityRepo : GenericRepo<AirQualityData>, IAirQualityRepo
    {

        private readonly LoggAppContext _dbContext;
        private static readonly string _baseUrl = "https://air-quality-api.open-meteo.com/v1/air-quality";
        private static readonly string _hourlyParams = "alder_pollen,birch_pollen,grass_pollen,mugwort_pollen,ragweed_pollen,uv_index,european_aqi,pm2_5,ozone,carbon_monoxide,nitrogen_dioxide,dust";

        private static readonly HttpClient _httpClient = new HttpClient();

        public AirQualityRepo(LoggAppContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> GetAirQualityDataAsync(string lat, string lon, string date)
        {
            
            string fullUrl = _baseUrl +
                $"?&latitude={lat}&longitude={lon}" +
                $"&hourly={_hourlyParams}" +
                $"&start_date={date}&end_date={date}";

            
            return await _httpClient.GetStringAsync(fullUrl);

        }
        
    }
}
