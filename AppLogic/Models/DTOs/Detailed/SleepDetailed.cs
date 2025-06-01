using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.Entities;

namespace AppLogic.Models.DTOs.Detailed
{
    public class SleepDetailed
    {

        
        public int Id { get; set; }

        public int DayCardId { get; set; }
        public DateTime? SleepStart { get; set; }
        public DateTime? SleepEnd { get; set; }

        public TimeSpan? TotalSleepTime { get; set; }
        //public TimeSpan? TotalSleepTime =>
        //    SleepStart.HasValue && SleepEnd.HasValue ? SleepEnd - SleepStart : null;

        public TimeSpan? DeepSleepDuration { get; set; }
        public TimeSpan? LightSleepDuration { get; set; }
        public TimeSpan? RemSleepDuration { get; set; }

        public int? SleepScore { get; set; }
        public int? TimesWokenUp { get; set; }
        public int? AvgBPM { get; set; }
        public int? Avg02 { get; set; }
        public int? AvgBreathsPerMin { get; set; }
        public PerceivedSleepQuality? PerceivedSleepQuality { get; set; }

        public SleepDetailed(Sleep sleep)
        {
            Id = sleep.Id;
            DayCardId = sleep.DayCardId;
            SleepStart = sleep.SleepStart;
            SleepEnd = sleep.SleepEnd;
            TotalSleepTime = sleep.TotalSleepTime;
            DeepSleepDuration = sleep.DeepSleepDuration;
            LightSleepDuration = sleep.LightSleepDuration;
            RemSleepDuration = sleep.RemSleepDuration;
            SleepScore = sleep.SleepScore;
            TimesWokenUp = sleep.TimesWokenUp;
            AvgBPM = sleep.AvgBPM;
            Avg02 = sleep.Avg02;
            AvgBreathsPerMin = sleep.AvgBreathsPerMin;
            PerceivedSleepQuality = sleep.PerceivedSleepQuality;
        }


        public SleepDetailed()
        {

        }


        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Id: {Id}");
            sb.AppendLine($"DayCardId: {DayCardId}");
            sb.AppendLine($"TotalSleepTime: {(TotalSleepTime.HasValue ? TotalSleepTime.Value.ToString() : "null")}");
            sb.AppendLine($"SleepStart: {(SleepStart.HasValue ? SleepStart.Value.ToString("o") : "null")}");
            sb.AppendLine($"SleepEnd: {(SleepEnd.HasValue ? SleepEnd.Value.ToString("o") : "null")}");
            sb.AppendLine($"DeepSleepDuration: {(DeepSleepDuration.HasValue ? DeepSleepDuration.Value.ToString() : "null")}");
            sb.AppendLine($"LightSleepDuration: {(LightSleepDuration.HasValue ? LightSleepDuration.Value.ToString() : "null")}");
            sb.AppendLine($"RemSleepDuration: {(RemSleepDuration.HasValue ? RemSleepDuration.Value.ToString() : "null")}");
            sb.AppendLine($"SleepScore: {(SleepScore.HasValue ? SleepScore.Value.ToString() : "null")}");
            sb.AppendLine($"TimesWokenUp: {(TimesWokenUp.HasValue ? TimesWokenUp.Value.ToString() : "null")}");
            sb.AppendLine($"AvgBPM: {(AvgBPM.HasValue ? AvgBPM.Value.ToString() : "null")}");
            sb.AppendLine($"Avg02: {(Avg02.HasValue ? Avg02.Value.ToString() : "null")}");
            sb.AppendLine($"AvgBreathsPerMin: {(AvgBreathsPerMin.HasValue ? AvgBreathsPerMin.Value.ToString() : "null")}");
            sb.AppendLine($"PerceivedSleepQuality: {(PerceivedSleepQuality?.ToString() ?? "null")}");
            return sb.ToString();
        }
    }
}
