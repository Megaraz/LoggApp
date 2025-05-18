using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.Weather.AirQuality;
using BusinessLogic.Models;
using BusinessLogic.Models.Activity;
using BusinessLogic.Models.Intake;
using BusinessLogic.Models.Weather;

namespace AppLogic.Models
{
    public class DayCardInputModel
    {
        public DateOnly? Date { get; set; } = DateOnly.FromDateTime(DateTime.Today);
        public int? UserId { get; set; }
        public double? Lat { get; set; }
        public double? Lon { get; set; }
        public ICollection<Activity>? Activities { get; set; } = new List<Activity>();
        //public virtual ICollection<Medication>? Medications { get; set; } = new List<Medication>();
        public ICollection<CaffeineDrink>? CaffeineDrinks { get; set; } = new List<CaffeineDrink>();
        //public virtual ICollection<Food>? Foods { get; set; } = new List<Food>();
        public ICollection<Supplement>? Supplements { get; set; } = new List<Supplement>();
        //public virtual ICollection<WeatherData>? WeatherData { get; set; } = new List<WeatherData>();
        public ICollection<WeatherData>? WeatherData { get; set; } = new List<WeatherData>();
        public ICollection<AirQualityData> AirQualities { get; set; } = new List<AirQualityData>();

    }
}
