using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.DTOs.Detailed;
using AppLogic.Models.Entities;

namespace AppLogic.Models.DTOs.Summary
{
    public class CaffeineDrinkSummary
    {

        public int Id { get; set; }
        public int DayCardId { get; set; }
        public TimeOnly? TimeOf { get; set; }
        public int? EstimatedMgCaffeine { get; set; }

        public CaffeineDrinkSummary(CaffeineDrink drink)
        {
            Id = drink.Id;
            DayCardId = drink.DayCardId!;
            TimeOf = drink.TimeOf;
            EstimatedMgCaffeine = drink.EstimatedMgCaffeine;
        }

        public CaffeineDrinkSummary(CaffeineDrinkDetailed caffeineDrinkDetailed)
        {
            Id = caffeineDrinkDetailed.Id;
            DayCardId = caffeineDrinkDetailed.DayCardId;
            TimeOf = caffeineDrinkDetailed.TimeOf;
            EstimatedMgCaffeine = caffeineDrinkDetailed.EstimatedMgCaffeine;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append($"{Id}\t{TimeOf}\t{EstimatedMgCaffeine}mg\n");


            return sb.ToString();

        }
    }
}
