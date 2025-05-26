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

        Task<UserDetailed> RegisterNewUserAsync(UserInputModel inputModel);

        Task<List<UserSummary>?> GetAllUsersIncludeAsync();

        Task<UserDetailed?> GetUserByIdIncludeAsync(int userId);

        Task<UserDetailed?> GetUserByUsernameIncludeAsync(string username);

        Task<UserDetailed> UpdateUserAsync(int userId, UserInputModel inputModel);

        Task<bool> DeleteUserAsync(int userId);
    }
}
