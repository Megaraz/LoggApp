using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.DTOs;
using AppLogic.Models;
using AppLogic.Services.Interfaces;
using AppLogic.Controllers.Interfaces;

namespace AppLogic.Controllers
{
    public class UserController : IUserController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<DTO_SpecificUser> CreateNewUserAsync(UserInputModel input)
        {
            return await _userService.RegisterNewUserAsync(input);
        }

        public async Task<List<DTO_AllUser>?> ReadAllUsersAsync()
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

        public async Task<DTO_SpecificUser?> ReadUserSingleAsync(int id)
        {
            return await _userService.ReadSingleUserAsync(id)!;

        }

        public async Task<DTO_SpecificUser?> ReadUserSingleAsync(string username)
        {

            return await _userService.ReadSingleUserAsync(username)!;

        }
    }
}
