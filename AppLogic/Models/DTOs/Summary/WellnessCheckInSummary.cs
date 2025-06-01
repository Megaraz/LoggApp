using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.DTOs.Detailed;
using AppLogic.Models.Entities;
using AppLogic.Models.InputModels;

namespace AppLogic.Models.DTOs.Summary
{
    /// <summary>
    /// DTO for summarizing wellness check-in information, including time of check-in and energy/mood levels.
    /// </summary>
    public class WellnessCheckInSummary
    {
        public int Id { get; set; }
        public int DayCardId { get; set; }
        public TimeOnly? TimeOf { get; set; }
        public RatingLevel? EnergyLevel { get; set; }
        public RatingLevel? MoodLevel { get; set; }

        public WellnessCheckInSummary(WellnessCheckInDetailed checkInDetailed)
        {
            Id = checkInDetailed.Id;
            DayCardId = checkInDetailed.DayCardId;
            TimeOf = checkInDetailed.TimeOf;
            EnergyLevel = checkInDetailed.EnergyLevel;
            MoodLevel = checkInDetailed.MoodLevel;

        }

        public WellnessCheckInSummary(WellnessCheckIn wellnessCheckIn)
        {
            Id = wellnessCheckIn.Id;
            DayCardId = wellnessCheckIn.DayCardId;
            TimeOf = wellnessCheckIn.TimeOf;
            EnergyLevel = wellnessCheckIn.EnergyLevel;
            MoodLevel = wellnessCheckIn.MoodLevel;
        }

        public override string ToString()
        {

            return $"[{Id.ToString()}]" + string.Empty.PadRight(3) +
                $"{TimeOf?.ToString().PadRight(10)}{MoodLevel?.ToString().PadRight(10)}{EnergyLevel?.ToString().PadRight(10)}";

        }
    }
}
