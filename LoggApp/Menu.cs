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

        public static MenuData MenuCollections { get; set; } = new MenuData();

        public static List<T> CurrentMenuData { get; set; } 


        //private static readonly string[] s_initMenu = { "[LOG IN]", "[GET ALL USERS]", "[CREATE NEW USER ACCOUNT]" };





        public static int DisplayMenu(int currentIndex)
        {


            if (currentIndex > currentMenuValues.Count - 1)
            {
                currentIndex = 0;
            }
            if (currentIndex < 0)
            {
                currentIndex = currentMenuValues.Count - 1;
            }
            int i = 0;
            foreach (var item in currentMenuValues)
            {

                if (currentIndex == i)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(item);
                    Console.ResetColor();

                }
                else
                {
                    Console.WriteLine(item);
                }
                i++;

            }
            return currentIndex;
        }

        public static (ConsoleKeyInfo keyPress, int currentMenuIndex) DisplayAndNavigate(Object? obj)
        {
            int currentMenuIndex = 0;
            ConsoleKeyInfo keyPress;

            do
            {
                Console.Clear();

                switch (CurrentMenuState)
                {
                    case MenuState.InitMenu:
                        Console.WriteLine("WELCOME TO LOGGAPP\n");
                        CurrentMenuData = MenuCollections.InitMenu;
                        break;
                    case MenuState.SpecificUser:
                        Console.WriteLine("CHOOSE ACTION FOR user: " + (obj is User user ? user.Username : "") );
                        CurrentMenuData = MenuCollections.SpecificUserMenu;
                        break;
                    case MenuState.AllUsers:
                        Console.WriteLine("ALL USERS IN DB\n");
                        break;

                }


                currentMenuIndex = DisplayMenu(currentMenuIndex);

                keyPress = Console.ReadKey(true);

                currentMenuIndex = InputHandler(keyPress, currentMenuIndex);


            }
            while (keyPress.Key != ConsoleKey.Escape && keyPress.Key != ConsoleKey.Enter);

            return (keyPress, currentMenuIndex);
        }
        public static int InputHandler(ConsoleKeyInfo keyPress, int currentIndex)
        {
            switch (keyPress.Key)
            {
                case ConsoleKey.DownArrow:
                    ++currentIndex;
                    break;
                case ConsoleKey.UpArrow:
                    --currentIndex;
                    break;
                case ConsoleKey.LeftArrow:
                    --currentIndex;
                    break;
                case ConsoleKey.RightArrow:
                    ++currentIndex;
                    break;
                case ConsoleKey.Escape:
                    break;
                case ConsoleKey.Enter:
                    break;
                default: break;
            }
            return currentIndex;
        }
    }
}
