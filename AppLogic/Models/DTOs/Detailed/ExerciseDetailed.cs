using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.DTOs.Summary;
using AppLogic.Models.Entities;
using AppLogic.Models.Enums;

namespace AppLogic.Models.DTOs.Detailed
{
    public class ExerciseDetailed
    {
        public int Id { get; set; }
        public int DayCardId { get; set; }

        // Inherited Activity properties
        public TimeOnly? TimeOf { get; set; }
        public TimeOnly? EndTime { get; set; }
        public TimeSpan? Duration { get; set; }

        // Exercise-specific properties
        public ExerciseType? ExerciseType { get; set; }
        public PerceivedIntensity? PerceivedIntensity { get; set; }
        public int? TrainingLoad { get; set; }
        public int? AvgHeartRate { get; set; }
        public CLOCK_Intensity? Intensity { get; set; }
        public int? ActiveKcalBurned { get; set; }
        public int? Distance { get; set; }
        public int? AvgKmTempo { get; set; }
        public int? Steps { get; set; }
        public int? AvgStepLength { get; set; }
        public int? AvgStepPerMin { get; set; }

        public ExerciseDetailed(Exercise exercise)
        {
            DayCardId = exercise.DayCardId;
            Id = exercise.Id;
            TimeOf = exercise.TimeOf;
            EndTime = exercise.EndTime;
            Duration = exercise.Duration;
            ExerciseType = exercise.ExerciseType;
            PerceivedIntensity = exercise.PerceivedIntensity;
            TrainingLoad = exercise.TrainingLoad;
            AvgHeartRate = exercise.AvgHeartRate;
            Intensity = exercise.Intensity;
            ActiveKcalBurned = exercise.ActiveKcalBurned;
            Distance = exercise.Distance;
            AvgKmTempo = exercise.AvgKmTempo;
            Steps = exercise.Steps;
            AvgStepLength = exercise.AvgStepLength;
            AvgStepPerMin = exercise.AvgStepPerMin;
        }

        public ExerciseDetailed(ExerciseSummary exerciseSummary)
        {
            Id = exerciseSummary.Id;
            DayCardId = exerciseSummary.DayCardId;
            Duration = exerciseSummary.Duration;
            ActiveKcalBurned = exerciseSummary.ActiveKcalBurned;
            PerceivedIntensity = exerciseSummary.PerceivedIntensity;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"EXERCISE ID: {Id}");
            sb.AppendLine($"TIME OF: {(TimeOf.HasValue ? TimeOf.ToString() : "null")}");
            sb.AppendLine($"END TIME: {(EndTime.HasValue ? EndTime.ToString() : "null")}");
            sb.AppendLine($"DURATION: {(Duration.HasValue ? Duration.ToString() : "null")}");
            sb.AppendLine($"EXERCISE TYPE: {(ExerciseType.HasValue ? ExerciseType.ToString() : "null")}");
            sb.AppendLine($"PERCEIVED INTENSITY: {(PerceivedIntensity.HasValue ? PerceivedIntensity.ToString() : "null")}");
            sb.AppendLine($"TRAINING LOAD: {(TrainingLoad.HasValue ? TrainingLoad.ToString() : "null")}");
            sb.AppendLine($"AVG HEART RATE: {(AvgHeartRate.HasValue ? AvgHeartRate.ToString() : "null")}");
            sb.AppendLine($"INTENSITY: {(Intensity.HasValue ? Intensity.ToString() : "null")}");
            sb.AppendLine($"ACTIVE KCAL BURNED: {(ActiveKcalBurned.HasValue ? ActiveKcalBurned.ToString() : "null")}");
            sb.AppendLine($"DISTANCE: {(Distance.HasValue ? Distance.ToString() : "null")}");
            sb.AppendLine($"AVG KM TEMPO: {(AvgKmTempo.HasValue ? AvgKmTempo.ToString() : "null")}");
            sb.AppendLine($"STEPS: {(Steps.HasValue ? Steps.ToString() : "null")}");
            sb.AppendLine($"AVG STEP LENGTH: {(AvgStepLength.HasValue ? AvgStepLength.ToString() : "null")}");
            sb.AppendLine($"AVG STEP PER MIN: {(AvgStepPerMin.HasValue ? AvgStepPerMin.ToString() : "null")}");
            return sb.ToString();

        }
    }
}
