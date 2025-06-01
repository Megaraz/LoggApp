using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.DTOs.Summary;
using AppLogic.Models.Entities;

namespace AppLogic.Models.DTOs.Detailed
{
    /// <summary>
    /// DTO for detailed wellness check-in information, including time of check-in and energy/mood levels.
    /// </summary>
    public class WellnessCheckInDetailed
    {
        public int Id { get; set; }
        public int DayCardId { get; set; }
        public TimeOnly? TimeOf { get; set; }
        public RatingLevel? EnergyLevel { get; set; }
        public RatingLevel? MoodLevel { get; set; }


        public WellnessCheckInDetailed(WellnessCheckIn wellnessCheckIn)
        {
            Id = wellnessCheckIn.Id;
            DayCardId = wellnessCheckIn.DayCardId;
            TimeOf = wellnessCheckIn.TimeOf;
            EnergyLevel = wellnessCheckIn.EnergyLevel;
            MoodLevel = wellnessCheckIn.MoodLevel;
        }

        public WellnessCheckInDetailed(WellnessCheckInSummary checkInSummary)
        {
            Id = checkInSummary.Id;
            DayCardId = checkInSummary.DayCardId;
            TimeOf = checkInSummary.TimeOf;
            EnergyLevel = checkInSummary.EnergyLevel;
            MoodLevel = checkInSummary.MoodLevel;
        }


        public override string ToString()
        {
            return $"[{Id.ToString()}]" + string.Empty.PadRight(3) +
                   $"{TimeOf?.ToString().PadRight(10)}{MoodLevel?.ToString().PadRight(10)}{EnergyLevel?.ToString().PadRight(10)}";
        }
    }
}
