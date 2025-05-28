using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Interfaces;

namespace AppLogic.Models.Entities
{
    public enum PerceivedSleepQuality
    {
        Poor,
        Fair,
        Good,
        VeryGood,
        Excellent
    }
    public class Sleep
    {
        public int Id { get; set; }


        public int DayCardId { get; set; }
        public virtual DayCard DayCard { get; set; } = null!;

        public DateTime? SleepStart { get; set; }
        public DateTime? SleepEnd { get; set; }
        public TimeSpan? TotalSleepTime =>
            SleepStart.HasValue && SleepEnd.HasValue ? SleepEnd - SleepStart : null;

        public TimeSpan? DeepSleepDuration { get; set; }
        public TimeSpan? LightSleepDuration { get; set; }
        public TimeSpan? RemSleepDuration { get; set; }

        public int? SleepScore { get; set; } 
        public int? TimesWokenUp { get; set; }
        public int? AvgBPM { get; set; }
        public int? Avg02 { get; set; }
        public int? AvgBreathsPerMin { get; set; }

        public PerceivedSleepQuality? PerceivedSleepQuality { get; set; }


        public Sleep()
        {
            
        }


    }
}
