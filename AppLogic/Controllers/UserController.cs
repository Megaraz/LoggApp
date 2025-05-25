using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models;
using AppLogic.Services.Interfaces;
using AppLogic.Controllers.Interfaces;
using AppLogic.Models.DTOs.Summary;
using AppLogic.Models.DTOs.Detailed;

namespace AppLogic.Controllers
{
    public class UserController : IUserController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UserDetailed> CreateNewUserAsync(UserInputModel input)
        {
            return await _userService.RegisterNewUserAsync(input);
        }

        public async Task<List<UserSummary>?> ReadAllUsersAsync()
        {

            var users = await _userService.ReadAllUsersAsync();


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
            return await _userService.ReadSingleUserAsync(id)!;

        }

        public async Task<UserDetailed?> ReadUserSingleAsync(string username)
        {

            return await _userService.ReadSingleUserAsync(username)!;

        }
    }
}
