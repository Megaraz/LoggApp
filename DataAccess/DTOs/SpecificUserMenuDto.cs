using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.DTOs
{
    public class SpecificUserMenuDto
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? CityName { get; set; }
        public double? Lat { get; set; }
        public double? Lon { get; set; }

        public List<AllDayCardsMenuDto>? AllDayCardsMenu { get; set; }


        public override string ToString()
        {
            return $"[{Id}].\t\t{Username}\t\t{CityName}\n";
        }
    }
}
