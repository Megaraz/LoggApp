using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.DTOs;
using BusinessLogic.Models;

namespace Presentation
{
    public enum MenuState { InitMenu, AllUsers, SpecificUser, CreateNewUser, CreateNewDayCard, AllDayCards, SpecificDayCard, SearchDayCard };

    public static class MenuData
    {
       


        // PAGE HEADERS
        public static readonly string s_InitMenuHeader = "WELCOME TO LOGGAPP!";
        public static readonly string s_AllUsersMenuHeader = "ALL USERS IN DB";
        public static readonly string s_SpecificUserMenuHeader = "CHOOSE ACTION FOR USER";
        public static readonly string s_CreateDayCardHeader = "ENTER A DATE FOR THE NEW DAYCARD, OR LEAVE EMPTY FOR TODAYS DATE";
        // MAIN PAGE HEADER VARIABLE, UPDATE DYAMICALLY
        public static string PageHeader = s_InitMenuHeader;

        // STATIC MENU OPTIONS
        public static List<string> s_InitMenu = new() { "[LOG IN]", "[GET ALL USERS]", "[CREATE NEW USER ACCOUNT]" };
        public static List<string> s_SpecificUserMenu = new() { "[CREATE NEW DAYCARD]", "[SEARCH DAYCARD]", "[SHOW ALL DAYCARDS]" };


        // DYNAMIC MENU OPTIONS
        public static SpecificUserMenuDto? CurrentUserMenu { get; set; }
        public static SpecificDayCardMenuDto? CurrentDayCardMenu { get; set; }
        public static List<AllUserMenuDto>? AllUsersMenu { get; set; }
        public static List<AllDayCardsMenuDto>? AllDayCardsMenu { get; set; }


    }
}
