using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Interfaces;
using AppLogic.Models.InputModels;

namespace AppLogic.Models.Entities
{
    public enum RatingLevel
    {
        VeryLow,
        Low,
        Medium,
        High,
        VeryHigh
    }
    /// <summary>
    /// Entity representing a wellness check-in associated with a day card, capturing the user's energy and mood levels at a specific time.
    /// </summary>
    public class WellnessCheckIn : ITimeOfEntry
    {
        public int Id { get; set; }
        public int DayCardId { get; set; }
        public virtual DayCard DayCard { get; set; } = null!;
        public TimeOnly? TimeOf { get; set; }
        public RatingLevel? EnergyLevel { get; set; }
        public RatingLevel? MoodLevel { get; set; }


        public WellnessCheckIn(int dayCardId, WellnessCheckInInputModel inputModel)
        {
            DayCardId = dayCardId;
            TimeOf = inputModel.TimeOf;
            EnergyLevel = inputModel.EnergyLevel;
            MoodLevel = inputModel.MoodLevel;
        }

        public WellnessCheckIn()
        {
            
        }

    }

    
}
