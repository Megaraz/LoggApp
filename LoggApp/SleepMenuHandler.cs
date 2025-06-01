using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Controllers;
using AppLogic.Models.DTOs.Detailed;
using Presentation.Input;
using Presentation.MenuState_Enums;

namespace Presentation
{
    public class SleepMenuHandler : MenuHandlerBase
    {

        private readonly SleepController _sleepController;

        public SleepMenuHandler(SleepController sleepController)
        {
            _sleepController = sleepController;
        }

        public override async Task<TContext> HandleMenuState<TContext>(TContext sessionContext)
        {
            switch (sessionContext.SleepMenuState)
            {
                case SleepMenuState.SleepDetails:
                    sessionContext = SleepDetailsMenuHandler(sessionContext);
                    break;

                case SleepMenuState.AddSleep:
                    sessionContext = await CreateSleepAsync(sessionContext);
                    break;

                case SleepMenuState.UpdateSleep:
                    sessionContext = await UpdateSleepMenuHandler(sessionContext);
                    break;

                case SleepMenuState.DeleteSleep:
                    sessionContext = await DeleteSleepMenuHandler(sessionContext);
                    break;



            }

            return sessionContext;
        }

        private async Task<TContext> DeleteSleepMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            throw new NotImplementedException();
        }

        private async Task<TContext> UpdateSleepMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            throw new NotImplementedException();
        }

        private async Task<TContext> CreateSleepAsync<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);

            var sleepInputModel = ConsoleInput.Input_Sleep();

            sessionContext.CurrentDayCard!.SleepDetails ??= new SleepDetailed();

            sessionContext.CurrentDayCard.SleepDetails = await _sleepController.AddSleepToDayCardAsync(sessionContext.CurrentDayCard.DayCardId, sleepInputModel);

            Console.Clear();
            Console.WriteLine("SLEEP ADDED");
            Thread.Sleep(1500);

            sessionContext.SleepMenuState = SleepMenuState.SleepDetails;

            return sessionContext;
        }

        private TContext SleepDetailsMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);


            sessionContext.MainHeader = "SLEEP STATS";
            sessionContext.SubHeader = sessionContext.CurrentDayCard?.SleepDetails!.ToString() ?? "No sleep data available.";

            var sleepChoice = MenuNavigation.GetMenuValue(MenuText.NavOption.s_SleepDetailsMenu.ToList(), sessionContext);

            if (sleepChoice is not null)
            {
                switch (sleepChoice)
                {
                    case MenuText.NavOption.UpdateSleep:
                        sessionContext.SleepMenuState = SleepMenuState.UpdateSleep;
                        break;

                    case MenuText.NavOption.DeleteSleep:
                        sessionContext.SleepMenuState = SleepMenuState.DeleteSleep;
                        break;

                    case MenuText.NavOption.Back:
                        sessionContext.DayCardMenuState = DayCardMenuState.Overview;
                        break;
                }
            }
            else
            {
                sessionContext.DayCardMenuState = DayCardMenuState.Overview;
            }

            return sessionContext;
        }
    } 
}
