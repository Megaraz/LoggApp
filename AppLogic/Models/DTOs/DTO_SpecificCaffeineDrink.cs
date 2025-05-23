using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.Intake.Enums;

namespace AppLogic.Models.DTOs
{
    public class DTO_SpecificCaffeineDrink
    {

        public int? DayCardId { get; set; }
        public int CaffeineDrinkId { get; set; }

        public TimeOnly? TimeOf { get; set; }

        public SizeOfDrink? SizeOfDrink { get; set; }

        public int? EstimatedMgCaffeine { get; set; }


        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append($"{TimeOf.ToString()}, {EstimatedMgCaffeine}mg");

            return sb.ToString();
        }

    }
}
