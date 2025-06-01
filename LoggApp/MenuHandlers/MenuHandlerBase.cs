using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Controllers;
using Microsoft.Identity.Client;
using Presentation.MenuState_Enums;

namespace Presentation.MenuHandlers
{
    /// <summary>
    /// Base class for handling menu states in the console application.
    /// </summary>
    public abstract class MenuHandlerBase
    {
        /// <summary>
        /// Handles the menu state for the given session context.
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="sessionContext"></param>
        /// <returns></returns>
        public abstract Task<TContext> HandleMenuState<TContext>(TContext sessionContext) where TContext : SessionContext;

        /// <summary>
        /// Resets the menu states and clears the session context properties to their default values.
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="sessionContext"></param>
        /// <returns></returns>
        protected static TContext ResetMenuStates<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            sessionContext.CurrentPrompt = string.Empty;
            sessionContext.MainHeader = string.Empty;
            sessionContext.SubHeader = string.Empty;
            sessionContext.MainContent = string.Empty;
            sessionContext.ErrorMessage = string.Empty;
            sessionContext.Footer = string.Empty;

            sessionContext.MainMenuState = MainMenuState.None;
            sessionContext.UserMenuState = UserMenuState.None;
            sessionContext.DayCardMenuState = DayCardMenuState.None;
            sessionContext.IntakeMenuState = IntakeMenuState.None;
            sessionContext.ActivityMenuState = ActivityMenuState.None;
            sessionContext.SleepMenuState = SleepMenuState.None;
            sessionContext.WellnessCheckInMenuState = WellnessCheckInMenuState.None;

            return sessionContext;
        }

        
    }
}
