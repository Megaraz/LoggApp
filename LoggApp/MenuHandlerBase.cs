using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Controllers;
using AppLogic.Controllers.Interfaces;
using Presentation.MenuState_Enums;

namespace Presentation
{
    public abstract class MenuHandlerBase
    {

        public abstract Task<TContext> HandleMenuState<TContext>(TContext sessionContext) where TContext : SessionContext;


        protected static TContext ResetMenuStates<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            sessionContext.CurrentPrompt = string.Empty;
            sessionContext.MainHeader = string.Empty;
            sessionContext.SubHeader = string.Empty;
            sessionContext.ErrorMessage = string.Empty;

            sessionContext.MainMenuState = MainMenuState.None;
            sessionContext.UserMenuState = UserMenuState.None;
            sessionContext.DayCardMenuState = DayCardMenuState.None;
            sessionContext.IntakeMenuState = IntakeMenuState.None;
            return sessionContext;
        }

        protected static T? GetMenuValue<T>(List<T> currentMenu, SessionContext sessionContext)
        {
            List<string> currentMenuStringList = currentMenu.Select(x => x?.ToString()).ToList()!;


            ConsoleKey keyPress;
            int currentIndex = 0;
            do
            {

                View.DisplayMenu(currentMenuStringList, ref currentIndex, sessionContext.CurrentPrompt!, sessionContext.MainHeader!, sessionContext.SubHeader, sessionContext.ErrorMessage);
                keyPress = View.InputToMenuIndex(ref currentIndex);

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
                            sessionContext.MainMenuState = MainMenuState.Back;

                        }
                    }
                    if (sessionContext.UserMenuState != UserMenuState.None)
                    {
                        sessionContext.UserMenuState = UserMenuState.Back;
                    }
                    if (sessionContext.DayCardMenuState != DayCardMenuState.None)
                    {
                        sessionContext.DayCardMenuState = DayCardMenuState.Back;
                    }
                    if (sessionContext.IntakeMenuState != IntakeMenuState.None)
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
