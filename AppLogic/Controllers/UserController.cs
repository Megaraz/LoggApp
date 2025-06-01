using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Services.Interfaces;
using AppLogic.Models.DTOs.Summary;
using AppLogic.Models.DTOs.Detailed;
using AppLogic.Models.InputModels;

namespace AppLogic.Controllers
{
    /// <summary>
    /// Controller for managing user accounts, including creating, updating, deleting, and retrieving user data.
    /// </summary>
    public class UserController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UserDetailed> CreateNewUserAsync(UserInputModel inputModel)
        {
            return await _userService.RegisterNewUserAsync(inputModel);
        }

        public async Task<UserDetailed> UpdateUserAsync(int userId, UserInputModel inputModel)
        {
            return await _userService.UpdateUserAsync(userId, inputModel);
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            return await _userService.DeleteUserAsync(userId);
        }

        public async Task<List<UserSummary>?> GetAllUsersIncludeAsync()
        {

            var users = await _userService.GetAllUsersIncludeAsync();


            if (users != null && users.Count > 0)
            {
                return users;
            }
            else
            {
                return null;
            }


        }




        public async Task<UserDetailed?> ReadUserSingleAsync(int id)
        {
            return await _userService.GetUserByIdIncludeAsync(id)!;

        }

        public async Task<UserDetailed?> ReadUserSingleAsync(string username)
        {

            return await _userService.GetUserByUsernameIncludeAsync(username)!;

        }

        public async Task<UserDetailed?> GetUserDetailed(int userId)
        {
            return await _userService.GetUserDetailedWithStatsAsync(userId);
        }
        public async Task<UserDetailed?> GetUserDetailed(string username)
        {
            return await _userService.GetUserDetailedWithStatsAsync(username);
        }
    }
}
