using AppLogic.Models.Intake;
using AppLogic.Models.Weather;
using AppLogic.Models.Weather.AirQuality;

namespace AppLogic.Models
{
    public class DayCard
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Models.Activity.Activity>? Activities { get; set; } = new List<Models.Activity.Activity>();
        public virtual ICollection<CaffeineDrink>? CaffeineDrinks { get; set; } = new List<CaffeineDrink>();
        public virtual ICollection<Supplement>? Supplements { get; set; } = new List<Supplement>();
        public virtual ICollection<WellnessCheckIn>? WellnessCheckIns { get; set; } = new List<WellnessCheckIn>();
        public virtual Sleep? Sleep { get; set; }
        public virtual WeatherData? WeatherData { get; set; }
        public virtual AirQualityData? AirQualityData { get; set; }


        public DayCard()
        {
        }

        //public override string ToString()
        //{
        //    return $"";
        //}

    }
}
