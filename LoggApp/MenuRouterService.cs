using AppLogic.Models.Entities;
using System.Globalization;
using AppLogic.Models.Enums;
using AppLogic.Models.InputModels;
using Microsoft.IdentityModel.Tokens;
using Presentation.MenuState_Enums;
using Presentation.MenuHandlers;

namespace Presentation
{
    /// <summary>
    /// Service to route menu states to the appropriate menu handlers based on the session context.
    /// </summary>
    public class MenuRouterService
    {
        public MainMenuHandler MainMenuHandler { get; }
        public UserMenuHandler UserMenuHandler { get; }
        public DayCardMenuHandler DayCardMenuHandler { get; }
        public IntakeMenuHandler IntakeMenuHandler { get; }
        public ExerciseMenuHandler ActivityMenuHandler { get; }
        public SleepMenuHandler SleepMenuHandler { get; }
        public WellnessMenuHandler WellnessMenuHandler { get; }

        public MenuRouterService(
            MainMenuHandler mainMenuHandler,
            UserMenuHandler userMenuHandler,
            DayCardMenuHandler menuHandler,
            IntakeMenuHandler intakeMenuHandler,
            ExerciseMenuHandler activityMenuHandler,
            SleepMenuHandler sleepMenuHandler,
            WellnessMenuHandler wellnessMenuHandler)
        {
            MainMenuHandler = mainMenuHandler;
            UserMenuHandler = userMenuHandler;
            DayCardMenuHandler = menuHandler;
            IntakeMenuHandler = intakeMenuHandler;
            ActivityMenuHandler = activityMenuHandler;
            SleepMenuHandler = sleepMenuHandler;
            WellnessMenuHandler = wellnessMenuHandler;
        }



        /// <summary>
        /// Routes the session context through the appropriate menu handlers based on the current menu states.
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="sessionContext"></param>
        /// <returns></returns>
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

            if (sessionContext.WellnessCheckInMenuState != WellnessCheckInMenuState.None)
            {
                sessionContext = await WellnessMenuHandler.HandleMenuState(sessionContext);
            }

            return sessionContext;
        }

    }

}

