using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.Entities;

namespace AppLogic.Models.InputModels
{
    public class SleepInputModel
    {
        public int DayCardId { get; set; }
        public DateTime? SleepStart { get; set; }
        public DateTime? SleepEnd { get; set; }
        public TimeSpan? TotalSleepTime { get; set; }
        public TimeSpan? DeepSleepDuration { get; set; }
        public TimeSpan? LightSleepDuration { get; set; }
        public TimeSpan? RemSleepDuration { get; set; }
        public int? SleepScore { get; set; }
        public int? TimesWokenUp { get; set; }
        public int? AvgBPM { get; set; }
        public int? Avg02 { get; set; }
        public int? AvgBreathsPerMin { get; set; }
        public PerceivedSleepQuality? PerceivedSleepQuality { get; set; }

        
    }
}
