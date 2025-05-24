using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models;
using AppLogic.Models.DTOs;

namespace AppLogic.Services.Interfaces
{
    public interface IUserService
    {

        Task<DTO_SpecificUser> RegisterNewUserAsync(UserInputModel input);

        Task<List<DTO_AllUser>?> ReadAllUsersAsync();

        Task<DTO_SpecificUser?> ReadSingleUserAsync(int id);

        Task<DTO_SpecificUser?> ReadSingleUserAsync(string username);
    }
}
