using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.DTOs;
using AppLogic.Models;

namespace AppLogic.Services.Interfaces
{
    public interface IDayCardService
    {
        Task<DTO_SpecificDayCard> CreateNewDayCardAsync(int userId, DayCardInputModel dayCardInputModel);
        Task<List<DTO_AllDayCard>?> ReadAllDayCardsAsync(int userId);
        Task<DTO_SpecificDayCard?> ReadSingleDayCardAsync(int id, int userId);
        Task<DTO_SpecificDayCard?> ReadSingleDayCardAsync(DateOnly date, int userId);
    }
}
