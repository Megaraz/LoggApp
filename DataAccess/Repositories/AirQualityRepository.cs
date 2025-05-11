using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppLogic.Repositories
{
    internal class AirQualityRepository
    {
        private readonly string _url = "https://air-quality-api.open-meteo.com/v1/air-quality";
        private readonly string _hourlyParams = "alder_pollen,birch_pollen,grass_pollen,mugwort_pollen,ragweed_pollen,uv_index,european_aqi,pm2_5,ozone,carbon_monoxide,nitrogen_dioxide,dust";

        public DayCard? DayCard { get; set; }


        public AirQualityRepository(DayCard dayCard)
        {
            DayCard = dayCard;
        }


        public async Task<string> GetAirQualityData()
        {
            HttpClient client = new HttpClient();

            string fullUrl = _url +
                $"&latitude={DayCard.User.Lat}&longitude={DayCard.User.Lon}" +
                $"&hourly={_hourlyParams}" +
                $"&start_date={DayCard.Date}&end_date={DayCard.Date}" +
                $"&timezone=auto";

            return await client.GetStringAsync(fullUrl);

        }
        
    }
}
