using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.DTOs.Detailed;
using AppLogic.Models.DTOs.Summary;
using AppLogic.Models.InputModels;

namespace AppLogic.Services.Interfaces
{
    /// <summary>
    /// Service interface for managing user accounts, including registration, retrieval, updating, and deletion of user data.
    /// </summary>
    public interface IUserService
    {

        Task<UserDetailed> RegisterNewUserAsync(UserInputModel inputModel);

        Task<List<UserSummary>?> GetAllUsersIncludeAsync();

        Task<UserDetailed?> GetUserByIdIncludeAsync(int userId);

        Task<UserDetailed?> GetUserByUsernameIncludeAsync(string username);

        Task<UserDetailed> UpdateUserAsync(int userId, UserInputModel inputModel);

        Task<bool> DeleteUserAsync(int userId);

        Task<UserDetailed?> GetUserDetailedWithStatsAsync(int userId);
        Task<UserDetailed?> GetUserDetailedWithStatsAsync(string username);
    }
}
