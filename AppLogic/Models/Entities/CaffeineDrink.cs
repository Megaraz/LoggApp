using AppLogic.Interfaces;
using AppLogic.Models.Enums;

namespace AppLogic.Models.Entities
{
    public class CaffeineDrink : ITimeOfEntry
    {
        public int Id { get; set; }
        public int DayCardId { get; set; }
        public virtual DayCard DayCard { get; set; } = null!;
        public TimeOnly? TimeOf { get; set; }
        public int? EstimatedMgCaffeine { get; set; }
        public string? TypeOfDrink { get; set; } 

        public CaffeineDrink(int dayCardId, TimeOnly? timeOfIntake, SizeOfDrink? sizeOfDrink, string? typeOfDrink)
        {
            DayCardId = dayCardId;
            TimeOf = timeOfIntake;
            TypeOfDrink = typeOfDrink;
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
