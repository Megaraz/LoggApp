using AppLogic.Models.Entities;
using System.Globalization;
using AppLogic.Models.Enums;
using AppLogic.Models.InputModels;
using Microsoft.IdentityModel.Tokens;
using Presentation.MenuState_Enums;

namespace Presentation
{
    public class MenuRouterService
    {
        public MainMenuHandler MainMenuHandler { get; }
        public UserMenuHandler UserMenuHandler { get; }
        public DayCardMenuHandler DayCardMenuHandler { get; }
        public IntakeMenuHandler IntakeMenuHandler { get; }
        public ActivityMenuHandler ActivityMenuHandler { get; }
        public SleepMenuHandler SleepMenuHandler { get; }

        public MenuRouterService(
            MainMenuHandler mainMenuHandler, 
            UserMenuHandler userMenuHandler, 
            DayCardMenuHandler menuHandler, 
            IntakeMenuHandler intakeMenuHandler, 
            ActivityMenuHandler activityMenuHandler, 
            SleepMenuHandler sleepMenuHandler)
        {
            MainMenuHandler = mainMenuHandler;
            UserMenuHandler = userMenuHandler;
            DayCardMenuHandler = menuHandler;
            IntakeMenuHandler = intakeMenuHandler;
            ActivityMenuHandler = activityMenuHandler;
            SleepMenuHandler = sleepMenuHandler;
        }



        // Routes to correct menu handler based on session context menu states.
        public async Task<TContext> MenuRouter<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            if (sessionContext.MainMenuState != MainMenuState.None)
            {
                sessionContext = await MainMenuHandler.HandleMenuState(sessionContext);
            }

            if (sessionContext.UserMenuState != UserMenuState.None)
            {
                sessionContext = await UserMenuHandler.HandleMenuState(sessionContext);
            }

            if (sessionContext.DayCardMenuState != DayCardMenuState.None)
            {
                sessionContext = await DayCardMenuHandler.HandleMenuState(sessionContext);
            }

            if (sessionContext.IntakeMenuState != IntakeMenuState.None)
            {
                sessionContext = await IntakeMenuHandler.HandleMenuState(sessionContext);
            }

            if (sessionContext.ActivityMenuState != ActivityMenuState.None)
            {
                sessionContext = await ActivityMenuHandler.HandleMenuState(sessionContext);
            }
            if (sessionContext.SleepMenuState != SleepMenuState.None)
            {
                sessionContext = await SleepMenuHandler.HandleMenuState(sessionContext);
            }

            return sessionContext;
        }

        



    }



}

