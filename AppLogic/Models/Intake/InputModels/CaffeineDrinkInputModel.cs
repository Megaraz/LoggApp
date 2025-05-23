using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Interfaces;
using AppLogic.Models.Intake.Enums;

namespace AppLogic.Models.Intake.InputModels
{
    public class CaffeineDrinkInputModel
    {

        public int DayCardId { get; set; }
        public TimeOnly? TimeOf { get; set; }

        public SizeOfDrink? SizeOfDrink { get; set; }

        public int? EstimatedMgCaffeine { get; set; }

        public string? TypeOfDrink { get; set; }    

    }
}
