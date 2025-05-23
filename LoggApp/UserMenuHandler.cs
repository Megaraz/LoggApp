using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Controllers;
using AppLogic.Models;
using Presentation.MenuState_Enums;

namespace Presentation
{
    public class UserMenuHandler : MenuHandlerBase
    {
        public UserMenuHandler(Controller controller) : base(controller)
        {
        }

        public override async Task<TContext> HandleMenuState<TContext>(TContext sessionContext)
        {
            switch (sessionContext.UserMenuState)
            {
                case UserMenuState.AllDayCards:
                    await AllDayCardsMenuHandler(sessionContext);
                    break;

                case UserMenuState.CreateNewDayCard:
                    await CreateDayCardMenuHandler(sessionContext);
                    break;
                case UserMenuState.SearchDayCard:
                    break;

                case UserMenuState.Back:
                    ResetMenuStates(sessionContext);
                    sessionContext.MainMenuState = MainMenuState.SpecificUser;
                    break;
            }

            return sessionContext;

        }

        public async Task<TContext> AllDayCardsMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {

            ResetMenuStates(sessionContext);
            if (sessionContext.DTO_CurrentUser!.DTO_AllDayCards == null || sessionContext.DTO_CurrentUser.DTO_AllDayCards.Count == 0)
            {
                sessionContext.ErrorMessage = MenuText.Error.NoDayCardsFound;
                sessionContext.UserMenuState = UserMenuState.Back;
            }
            else
            {
                sessionContext.MainHeader = sessionContext.MainHeader = MenuText.Header.SpecificUser + $"{sessionContext.DTO_CurrentUser!.ToString()}";
                sessionContext.SubHeader = MenuText.Header.AllDayCards;

                var dayCardChoice = GetMenuValue(sessionContext!.DTO_CurrentUser!.DTO_AllDayCards!, sessionContext);

                if (dayCardChoice != null)
                {
                    sessionContext.DTO_CurrentDayCard = await _controller.ReadDayCardSingleAsync(dayCardChoice.DayCardId, sessionContext.DTO_CurrentUser.Id);
                    sessionContext.DayCardMenuState = DayCardMenuState.Overview;
                }
                else
                {
                    sessionContext.UserMenuState = UserMenuState.Back;
                }


            }

            return sessionContext;
        }

        public async Task<TContext> CreateDayCardMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);
            //sessionContext.MainHeader = MenuText.Prompt.CreateDayCard;
            DayCardInputModel? dayCardInputModel = View.Input_DayCard(sessionContext);

            if (dayCardInputModel != null)
            {
                sessionContext.DTO_CurrentDayCard = await _controller.CreateNewDayCardAsync(sessionContext.DTO_CurrentUser!.Id, dayCardInputModel);
                sessionContext.DayCardMenuState = DayCardMenuState.Overview;

            }
            else
            {
                sessionContext.UserMenuState = UserMenuState.Back;
            }

            return sessionContext;
        }

    }
}
