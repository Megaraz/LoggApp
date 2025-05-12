using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppLogic.Repositories
{
    internal class AirQualityRepository
    {
        private readonly string _baseUrl = "https://air-quality-api.open-meteo.com/v1/air-quality";
        private readonly string _hourlyParams = "alder_pollen,birch_pollen,grass_pollen,mugwort_pollen,ragweed_pollen,uv_index,european_aqi,pm2_5,ozone,carbon_monoxide,nitrogen_dioxide,dust";

        public DayCard? DayCard { get; set; }


        public AirQualityRepository(DayCard dayCard)
        {
            DayCard = dayCard;
        }

        public AirQualityRepository()
        {
            
        }


        public async Task<string> GetAirQualityDataAsync(string lat, string lon, string date)
        {
            HttpClient client = new HttpClient();

            string fullUrl = _baseUrl +
                $"?&latitude={lat}&longitude={lon}" +
                $"&hourly={_hourlyParams}" +
                $"&start_date={date}&end_date={date}";

            return await client.GetStringAsync(fullUrl);

        }
        
    }
}
