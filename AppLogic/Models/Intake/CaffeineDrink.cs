using AppLogic.Interfaces;
using AppLogic.Models.Intake.Enums;

namespace AppLogic.Models.Intake
{
    public class CaffeineDrink : ITimeOfEntry
    {
        public int Id { get; set; }
        public int? DayCardId { get; set; }
        public virtual DayCard? DayCard { get; set; }
        public TimeOnly? TimeOf { get; set; }
        public int? EstimatedMgCaffeine { get; set; }
        public string? TypeOfDrink { get; set; } 

        public CaffeineDrink(TimeOnly timeOfIntake, SizeOfDrink sizeOfDrink, string typeOfDrink)
        {
            TimeOf = timeOfIntake;
            TypeOfDrink = typeOfDrink ?? throw new ArgumentNullException(nameof(typeOfDrink));
            EstimatedMgCaffeine = sizeOfDrink switch
            {
                SizeOfDrink.Small => 110,
                SizeOfDrink.Medium => 140,
                SizeOfDrink.Large => 180,
                _ => 0

            };
        }

        public CaffeineDrink()
        {
        }
    }
}
