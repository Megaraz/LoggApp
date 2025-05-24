using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.DTOs;
using AppLogic.Models;

namespace AppLogic.Controllers.Interfaces
{
    public interface IUserController
    {
        Task<DTO_SpecificUser> CreateNewUserAsync(UserInputModel input);
        Task<List<DTO_AllUser>?> ReadAllUsersAsync();
        Task<DTO_SpecificUser?> ReadUserSingleAsync(int id);
        Task<DTO_SpecificUser?> ReadUserSingleAsync(string username);
    }
}
