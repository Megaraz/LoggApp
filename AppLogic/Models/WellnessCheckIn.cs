using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Interfaces;

namespace AppLogic.Models
{
    
    public class WellnessCheckIn : ITimeOfEntry
    {
        public int Id { get; set; }
        public int DayCardId { get; set; }
        public virtual DayCard? DayCard { get; set; }
        public TimeOnly? TimeOf { get; set; }
        public RatingLevel? EnergyLevel { get; set; }
        public RatingLevel? MoodLevel { get; set; }
    }

    public enum RatingLevel
    {
        VeryLow,
        Low,
        Medium,
        High,
        VeryHigh
    }
    
}
