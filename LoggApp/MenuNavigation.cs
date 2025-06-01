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
    internal class MenuNavigation
    {

        public static T? GetMenuValue<T>(List<T> currentMenu, SessionContext sessionContext)
        {
            //List<string> currentMenuStringList = currentMenu.Select(x => x?.ToString()).ToList()!;


            ConsoleKey keyPress;
            int currentIndex = 0;
            do
            {

                ConsoleViewRenderer.DisplayMenu(currentMenu, ref currentIndex, sessionContext);
                keyPress = ConsoleInput.InputToMenuIndex(ref currentIndex);

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
