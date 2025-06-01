using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.Enums;

namespace AppLogic.Models.InputModels
{
    public class ExerciseInputModel
    {
        // Activity properties
        public int DayCardId { get; set; }
        public TimeOnly? TimeOf { get; set; }
        public TimeOnly? EndTime { get; set; }
        public TimeSpan? Duration { get; set; }

        // Exercise-specific properties
        public ExerciseType? ExerciseType { get; set; }
        public PerceivedIntensity? PerceivedIntensity { get; set; }
        public int? TrainingLoad { get; set; }
        public int? AvgHeartRate { get; set; }
        public int? ActiveKcalBurned { get; set; }
        public double? DistanceInKm { get; set; }
        public TimeSpan? AvgKmTempo { get; set; }
        public int? Steps { get; set; }
        public int? AvgStepLengthInCm { get; set; }
        public int? AvgStepPerMin { get; set; }

        
    }

}
