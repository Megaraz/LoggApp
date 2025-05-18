using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Models.DTOs
{
    public class DTO_SpecificUser
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? CityName { get; set; }
        public double? Lat { get; set; }
        public double? Lon { get; set; }

        public int? DayCardCount { get; set; }

        public List<DTO_AllDayCard>? DTO_AllDayCards { get; set; }


        public override string ToString()
        {
            return $"[{Id}].\t\t{Username}\t\t{CityName}\n";
        }
    }
}
