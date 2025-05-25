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
    public interface IUserController
    {
        Task<UserDetailed> CreateNewUserAsync(UserInputModel input);
        Task<List<UserSummary>?> ReadAllUsersAsync();
        Task<UserDetailed?> ReadUserSingleAsync(int id);
        Task<UserDetailed?> ReadUserSingleAsync(string username);
    }
}
