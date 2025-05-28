using AppLogic.Models.Entities;
using AppLogic.Models.Entities.WeatherAndAQI;

namespace AppLogic.Models.InputModels
{
    public class DayCardInputModel
    {
        public DateOnly? Date { get; set; } = DateOnly.FromDateTime(DateTime.Today);
        public int UserId { get; set; }
        public double? Lat { get; set; }
        public double? Lon { get; set; }
        public ICollection<Entities.Activity>? Activities { get; set; } = new List<Entities.Activity>();
        //public virtual ICollection<Medication>? Medications { get; set; } = new List<Medication>();
        public ICollection<CaffeineDrink>? CaffeineDrinks { get; set; } = new List<CaffeineDrink>();
        //public virtual ICollection<Food>? Foods { get; set; } = new List<Food>();
        public ICollection<Supplement>? Supplements { get; set; } = new List<Supplement>();
        //public virtual ICollection<WeatherData>? WeatherData { get; set; } = new List<WeatherData>();
        public WeatherData? WeatherData { get; set; }
        public AirQualityData? AirQualityData { get; set; }

    }
}
