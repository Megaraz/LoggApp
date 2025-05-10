using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.DTOs;
using BusinessLogic.Models;

namespace Presentation
{
    public class MenuData
    {
        public static readonly string s_InitMenuHeader = "WELCOME TO LOGGAPP!";
        public static List<string> s_InitMenu = new() { "[LOG IN]", "[GET ALL USERS]", "[CREATE NEW USER ACCOUNT]" };


        public static readonly string s_SpecificUserMenuHeader = "CHOOSE ACTION FOR USER";
        public static List<string> s_SpecificUserMenu = new() { "[CREATE NEW DAYCARD]", "[SEARCH DAYCARD]" };
        public User? CurrentUser { get; set; }



        public static readonly string s_AllUsersMenuHeader = "ALL USERS IN DB";
        public List<AllUserMenuDto>? AllUsers { get; set; }

    }
}
