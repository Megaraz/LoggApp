using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models;
using AppLogic.Models.DTOs.Detailed;
using AppLogic.Models.DTOs.Summary;

namespace AppLogic.Services.Interfaces
{
    public interface IUserService
    {

        Task<UserDetailed> RegisterNewUserAsync(UserInputModel input);

        Task<List<UserSummary>?> ReadAllUsersAsync();

        Task<UserDetailed?> ReadSingleUserAsync(int id);

        Task<UserDetailed?> ReadSingleUserAsync(string username);
    }
}
