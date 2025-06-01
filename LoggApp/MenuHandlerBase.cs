using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Controllers;
using Microsoft.Identity.Client;
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
            sessionContext.MainContent = string.Empty;
            sessionContext.ErrorMessage = string.Empty;
            sessionContext.Footer = string.Empty;

            sessionContext.MainMenuState = MainMenuState.None;
            sessionContext.UserMenuState = UserMenuState.None;
            sessionContext.DayCardMenuState = DayCardMenuState.None;
            sessionContext.IntakeMenuState = IntakeMenuState.None;
            sessionContext.ActivityMenuState = ActivityMenuState.None;
            sessionContext.SleepMenuState = SleepMenuState.None;
            return sessionContext;
        }

        
    }
}
