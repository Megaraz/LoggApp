using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Models;

namespace Presentation
{
    internal static class Menu<T>
    {

        //public enum Menu_OptionsMain { Login, ReadAllUsers, CreateNewUser };
        //public enum Menu_OptionsSpecificUser { CreateDayCard, SpecificDayCard };

        public enum MenuState { InitMenu, AllUsers, SpecificUser, CreateNewUser, CreateNewDayCard };
        public static MenuState CurrentMenuState { get; set; } = MenuState.InitMenu;

        public static string header = string.Empty;



        public static void SetMenuValues(ICollection<T> currentMenuData)
        {

            switch (CurrentMenuState)
            {
                case MenuState.InitMenu:
                    header = MenuData.s_InitMenuHeader;
                    currentMenuData = (ICollection<T>)MenuData.s_InitMenu;
                    break;
                case MenuState.SpecificUser:
                    header = MenuData.s_SpecificUserMenuHeader;
                    currentMenuData = (ICollection<T>)MenuData.s_SpecificUserMenu;
                    break;
                case MenuState.AllUsers:
                    header = MenuData.s_AllUsersMenuHeader;
                    currentMenuData = (ICollection<T>)new MenuData().AllUsers;
                    break;
            }

        }



    }
}
