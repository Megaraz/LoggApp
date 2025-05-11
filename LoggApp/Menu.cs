//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using BusinessLogic.Models;

//namespace Presentation
//{
//    internal static class Menu
//    {

//        //public enum Menu_OptionsMain { Login, ReadAllUsers, CreateNewUser };
//        //public enum Menu_OptionsSpecificUser { CreateDayCard, SpecificDayCard };

//        public enum MenuState { InitMenu, AllUsers, SpecificUser, CreateNewUser, CreateNewDayCard };
//        public static MenuState CurrentMenuState { get; set; } = MenuState.InitMenu;

//        public static string header = string.Empty;

        



//        public static void MenuHandler(int currentIndex, ConsoleKeyInfo keyPress)
//        {
//            //ICollection<T> currentMenuData

//            switch (CurrentMenuState)
//            {
//                case MenuState.InitMenu:
//                    header = MenuData.s_InitMenuHeader;
//                    currentMenuData = (ICollection<T>)MenuData.s_InitMenu;
//                    break;
//                case MenuState.SpecificUser:
//                    header = MenuData.s_SpecificUserMenuHeader;
//                    currentMenuData = (ICollection<T>)MenuData.s_SpecificUserMenu;
//                    break;
//                case MenuState.AllUsers:
//                    header = MenuData.s_AllUsersMenuHeader;
//                    currentMenuData = (ICollection<T>)new MenuData().AllUsers;
//                    break;
//            }

//            if (currentIndex > currentMenuData.Count - 1)
//            {
//                currentIndex = 0;
//            }
//            if (currentIndex < 0)
//            {
//                currentIndex = currentMenuData.Count - 1;
//            }
//            int i = 0;
//            foreach (var item in currentMenuData)
//            {

//                if (currentIndex == i)
//                {
//                    Console.ForegroundColor = ConsoleColor.Green;
//                    Console.WriteLine(item);
//                    Console.ResetColor();

//                }
//                else
//                {
//                    Console.WriteLine(item);
//                }
//                i++;

//            }
//            switch (keyPress.Key)
//            {
//                case ConsoleKey.DownArrow:
//                    ++currentIndex;
//                    break;
//                case ConsoleKey.UpArrow:
//                    --currentIndex;
//                    break;
//                case ConsoleKey.LeftArrow:
//                    --currentIndex;
//                    break;
//                case ConsoleKey.RightArrow:
//                    ++currentIndex;
//                    break;
//                case ConsoleKey.Escape:
//                    break;
//                case ConsoleKey.Enter:
//                    break;
//                default: break;
//            }


//        }

//        //public static int DisplayMenu(int currentIndex)
//        //{


//        //    if (currentIndex > CurrentMenuData.Count - 1)
//        //    {
//        //        currentIndex = 0;
//        //    }
//        //    if (currentIndex < 0)
//        //    {
//        //        currentIndex = CurrentMenuData.Count - 1;
//        //    }
//        //    int i = 0;
//        //    foreach (var item in CurrentMenuData)
//        //    {

//        //        if (currentIndex == i)
//        //        {
//        //            Console.ForegroundColor = ConsoleColor.Green;
//        //            Console.WriteLine(item);
//        //            Console.ResetColor();

//        //        }
//        //        else
//        //        {
//        //            Console.WriteLine(item);
//        //        }
//        //        i++;

//        //    }
//        //    return currentIndex;
//        //}



//    }
//}
