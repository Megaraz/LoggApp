using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models;
using AppLogic.Models.DTOs.Summary;
using AppLogic.Models.DTOs.Detailed;

namespace AppLogic.Controllers.Interfaces
{
    public interface IDayCardController
    {
        Task<DayCardDetailed> CreateNewDayCardAsync(int userId, DayCardInputModel input);

        Task<List<DayCardSummary>?> ReadAllDayCardsAsync(int userId);
        Task<DayCardDetailed?> ReadDayCardSingleAsync(int id, int userId);
        Task<DayCardDetailed?> ReadDayCardSingleAsync(DateOnly date, int userId);
    }
}
