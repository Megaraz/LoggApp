using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.DTOs
{
    public class AllDayCardsMenuDto
    {
        public int DayCardId { get; set; }
        public int? UserId { get; set; }
        public DateOnly? Date { get; set; }


        public override string ToString()
        {
            return $"[{DayCardId}]\t\t{Date}";
        }
    }
}
