using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    public class Menu
    {
        // MENUSTATE ENUM
        public static MenuState CurrentMenuState { get; set; } = MenuState.InitMenu;
        public static async Task InitMenuHandler()
        {
            var initMenuChoice = BaseMenuHandler(MenuData.s_InitMenu);
            switch (initMenuChoice)
            {
                case $"[LOG IN]":
                    string username = ViewInput.GetValidUserInput("Username");
                    await View.ReadUserSingleAsync(username);

                    if (MenuData.CurrentUserMenu == null)
                    {
                        MenuData.PageHeader = "NO USER WITH THAT ID FOUND";
                        CurrentMenuState = MenuState.InitMenu;
                        break;
                    }
                    CurrentMenuState = MenuState.SpecificUser;
                    break;
                case "[GET ALL USERS]":
                    MenuData.AllUsersMenu = await View.ReadAllUsersAsync()!;
                    CurrentMenuState = MenuState.AllUsers;
                    break;
                case "[CREATE NEW USER ACCOUNT]":
                    await View.CreateNewUserAsync();
                    CurrentMenuState = MenuState.SpecificUser;
                    break;
            }
        }

        public static T BaseMenuHandler<T>(ICollection<T> currentMenu)
        {
            var currentMenuList = currentMenu.ToList();

            ConsoleKeyInfo keyPress;
            int currentIndex = 0;
            do
            {
                Console.Clear();
                Console.WriteLine(Header);
                Console.WriteLine();

                DisplayMenu(currentMenuList, ref currentIndex);
                keyPress = Console.ReadKey(true);

                InputHandler(keyPress, ref currentIndex);
            } while (keyPress.Key != ConsoleKey.Enter && keyPress.Key != ConsoleKey.Escape);

            return currentMenuList[currentIndex];
        }

        public static void DisplayMenu<T>(List<T> currentMenu, ref int currentIndex)
        {


            if (currentMenu != null && currentMenu.Count > 0)
            {
                if (currentIndex > currentMenu.Count - 1)
                {
                    currentIndex = 0;
                }
                if (currentIndex < 0)
                {
                    currentIndex = currentMenu.Count - 1;
                }

                for (int i = 0; i < currentMenu.Count; i++)
                {

                    var item = currentMenu[i];
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
                }

            }
        }

        
    }
}
