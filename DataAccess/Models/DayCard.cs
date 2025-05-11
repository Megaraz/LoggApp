using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Models.Intake;
using BusinessLogic.Models.Activity;
using BusinessLogic.Models.Weather;
using AppLogic.Models.Weather.AirQuality;

namespace BusinessLogic.Models
{
    public class DayCard
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public int? UserId { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<Models.Activity.Activity>? Activities { get; set; } = new List<Models.Activity.Activity>();
        //public virtual ICollection<Medication>? Medications { get; set; } = new List<Medication>();
        public virtual ICollection<CaffeineDrink>? CaffeineDrinks { get; set; } = new List<CaffeineDrink>();
        //public virtual ICollection<Food>? Foods { get; set; } = new List<Food>();
        public virtual ICollection<Supplement>? Supplements { get; set; } = new List<Supplement>();
        //public virtual ICollection<WeatherData>? WeatherData { get; set; } = new List<WeatherData>();
        public virtual WeatherData? WeatherData { get; set; }
        public virtual ICollection<AirQuality> AirQualities { get; set; } = new List<AirQuality>();

        public DayCard()
        {
        }

        //public override string ToString()
        //{
        //    return $"";
        //}

    }
}
