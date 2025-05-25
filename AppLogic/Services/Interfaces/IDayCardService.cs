using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models;
using AppLogic.Models.DTOs.Summary;
using AppLogic.Models.DTOs.Detailed;

namespace AppLogic.Services.Interfaces
{
    public interface IDayCardService
    {
        Task<DayCardDetailed> CreateNewDayCardAsync(int userId, DayCardInputModel dayCardInputModel);
        Task<List<DayCardSummary>?> ReadAllDayCardsAsync(int userId);
        Task<DayCardDetailed?> ReadSingleDayCardAsync(int id, int userId);
        Task<DayCardDetailed?> ReadSingleDayCardAsync(DateOnly date, int userId);
    }
}
