using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.DTOs.Detailed;
using AppLogic.Models.Entities;
using AppLogic.Models.Enums;

namespace AppLogic.Models.DTOs.Summary
{
    public class ExerciseSummary
    {
        public int Id { get; set; }
        public int DayCardId { get; set; }
        public TimeSpan? Duration { get; set; }
        public PerceivedIntensity? PerceivedIntensity { get; set; }

        public int? ActiveKcalBurned { get; set; }

        public ExerciseSummary(Exercise exercise)
        {
            Id = exercise.Id;
            DayCardId = exercise.DayCardId;
            Duration = exercise.Duration;
            ActiveKcalBurned = exercise.ActiveKcalBurned;
            PerceivedIntensity = exercise.PerceivedIntensity;
        }

        public ExerciseSummary(ExerciseDetailed exerciseDetailed)
        {
            Id = exerciseDetailed.Id;
            DayCardId = exerciseDetailed.DayCardId;
            Duration = exerciseDetailed.Duration;
            ActiveKcalBurned = exerciseDetailed.ActiveKcalBurned;
            PerceivedIntensity = exerciseDetailed.PerceivedIntensity;
        }

        public ExerciseSummary()
        {
            
        }

        public override string ToString()
        {

            var sb = new StringBuilder();
            
            sb.AppendLine($"{Id}\t{Duration}\t{PerceivedIntensity}\n");

            return sb.ToString().TrimEnd();

        }


    }
}
