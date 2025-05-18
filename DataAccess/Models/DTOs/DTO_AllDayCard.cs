using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Models.DTOs
{
    public class DTO_AllDayCard
    {
        public int DayCardId { get; set; }
        public int? UserId { get; set; }
        public DateOnly? Date { get; set; }

        public int? Entries { get; set; }

        public override string ToString()
        {
            return $"[{DayCardId}]\t\t{Date}\t{Entries}";
        }
    }
}
