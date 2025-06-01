using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.DTOs.Summary;
using AppLogic.Models.Enums;

namespace AppLogic.Models.DTOs.Detailed
{
    public class CaffeineDrinkDetailed
    {

        public int DayCardId { get; set; }
        public int Id { get; set; }

        public TimeOnly? TimeOf { get; set; }

        public int? EstimatedMgCaffeine { get; set; }


        public CaffeineDrinkDetailed(CaffeineDrinkSummary caffeineDrinkSummary)
        {
            DayCardId = caffeineDrinkSummary.DayCardId;
            Id = caffeineDrinkSummary.Id;
            TimeOf = caffeineDrinkSummary.TimeOf;
            EstimatedMgCaffeine = caffeineDrinkSummary.EstimatedMgCaffeine;
        }

        public CaffeineDrinkDetailed()
        {
            
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append($"{TimeOf.ToString()}, {EstimatedMgCaffeine}mg");

            return sb.ToString();
        }

    }
}
