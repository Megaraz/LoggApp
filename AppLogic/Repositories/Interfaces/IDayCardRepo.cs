using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models;

namespace AppLogic.Repositories.Interfaces
{
    public interface IDayCardRepo : IGenericRepo<DayCard>
    {
        Task<DayCard?> GetDayCardByDate(DateOnly date, int userId);
        Task<DayCard?> GetDayCardById(int id, int userId);
        Task<List<DayCard>?> GetAllDayCardsAsync(int userId);
    }

}
