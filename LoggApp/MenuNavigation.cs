using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Display;
using Presentation.Input;
using Presentation.MenuState_Enums;

namespace Presentation
{
    /// <summary>
    /// Handles the navigation through menus in the console application.
    /// </summary>
    internal class MenuNavigation
    {
        /// <summary>
        /// Displays the current menu and allows the user to select an option.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="currentMenu"></param>
        /// <param name="sessionContext"></param>
        /// <returns></returns>
        public static T? GetMenuValue<T>(List<T> currentMenu, SessionContext sessionContext)
        {
            ConsoleKey keyPress;
            int currentIndex = 0;

            // Loop until user selects an option(with Enter) or presses Escape
            do
            {
                // Display the current menu and highlight the current index.
                ConsoleViewRenderer.DisplayMenu(currentMenu, ref currentIndex, sessionContext);
                // Get user input to navigate the menu.
                keyPress = ConsoleInput.InputToMenuIndex(ref currentIndex);

                // Ugly but functional "Escape to go back" logic.
                if (keyPress == ConsoleKey.Escape)
                {
                    if (sessionContext.MainMenuState != MainMenuState.None)
                    {
                        if (sessionContext.MainMenuState == MainMenuState.Main)
                        {
                            sessionContext.MainMenuState = MainMenuState.Exit;
                        }
                        else
                        {
                            sessionContext.MainMenuState = MainMenuState.Main;
                        }
                    }
                    if (sessionContext.UserMenuState != UserMenuState.None)
                    {
                        sessionContext.UserMenuState = UserMenuState.Overview;
                    }

                    if (sessionContext.DayCardMenuState != DayCardMenuState.None)
                    {
                        sessionContext.UserMenuState = UserMenuState.AllDayCards;
                    }
                    if (sessionContext.IntakeMenuState != IntakeMenuState.None)
                    {
                        sessionContext.DayCardMenuState = DayCardMenuState.Overview;
                    }
                    if (sessionContext.ActivityMenuState != ActivityMenuState.None)
                    {
                        sessionContext.DayCardMenuState = DayCardMenuState.Overview;
                    }
                    if (sessionContext.SleepMenuState != SleepMenuState.None)
                    {
                        sessionContext.DayCardMenuState = DayCardMenuState.Overview;
                    }

                    if (sessionContext.WellnessCheckInMenuState != WellnessCheckInMenuState.None)
                    {
                        sessionContext.DayCardMenuState = DayCardMenuState.Overview;
                    }
                }

            } while (keyPress != ConsoleKey.Enter && keyPress != ConsoleKey.Escape);

            if (keyPress != ConsoleKey.Escape)
            {
                return currentMenu[currentIndex]!;

            }
            else
            {
                return default;
            }

        }
    }
}
