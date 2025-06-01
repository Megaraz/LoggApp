using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.Entities;

namespace AppLogic.Models.InputModels
{
    public class WellnessCheckInInputModel
    {
        public int DayCardId { get; set; }
        public TimeOnly? TimeOf { get; set; }
        public RatingLevel? EnergyLevel { get; set; }
        public RatingLevel? MoodLevel { get; set; }

    }
}
