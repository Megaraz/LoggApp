using AppLogic.Models.Intake;
using AppLogic.Models.Weather;
using AppLogic.Models.Weather.AirQuality;

namespace AppLogic.Models
{
    public class DayCardInputModel
    {
        public DateOnly? Date { get; set; } = DateOnly.FromDateTime(DateTime.Today);
        public int UserId { get; set; }
        public double? Lat { get; set; }
        public double? Lon { get; set; }
        public ICollection<Activity.Activity>? Activities { get; set; } = new List<Activity.Activity>();
        //public virtual ICollection<Medication>? Medications { get; set; } = new List<Medication>();
        public ICollection<CaffeineDrink>? CaffeineDrinks { get; set; } = new List<CaffeineDrink>();
        //public virtual ICollection<Food>? Foods { get; set; } = new List<Food>();
        public ICollection<Supplement>? Supplements { get; set; } = new List<Supplement>();
        //public virtual ICollection<WeatherData>? WeatherData { get; set; } = new List<WeatherData>();
        public WeatherData? WeatherData { get; set; }
        public AirQualityData? AirQualityData { get; set; }

    }
}
