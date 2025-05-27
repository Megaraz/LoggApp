using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Controllers;
using AppLogic.Models.Intake.Enums;
using AppLogic.Models.Intake.InputModels;
using Presentation.MenuState_Enums;

namespace Presentation
{
    public class IntakeMenuHandler : MenuHandlerBase
    {
        private readonly CaffeineDrinkController _caffeineDrinkController;

        public IntakeMenuHandler(CaffeineDrinkController caffeineDrinkController)
        {
            _caffeineDrinkController = caffeineDrinkController;
        }

        public override async Task<TContext> HandleMenuState<TContext>(TContext sessionContext)
        {
            switch (sessionContext.IntakeMenuState)
            {
                case IntakeMenuState.CaffeineOverview:
                    sessionContext = CaffeineOverviewMenuHandler(sessionContext);
                    break;

                case IntakeMenuState.ShowAllCaffeineDrinks:
                    sessionContext = CaffeineShowAllMenuHandler(sessionContext);
                    break;

                case IntakeMenuState.CaffeineDrinkDetails:
                    sessionContext = CaffeineDrinkDetailsMenuHandler(sessionContext);
                    break;

                case IntakeMenuState.AddCaffeineDrink:
                    sessionContext = await CreateCaffeineDrinkAsync(sessionContext);
                    break;

                case IntakeMenuState.UpdateCaffeineDrink:
                    sessionContext = await UpdateCaffeineDrinkAsync(sessionContext);
                    break;

                case IntakeMenuState.DeleteCaffeineDrink:
                    sessionContext = await DeleteCaffeineDrinkAsyc(sessionContext); 
                    break;

                case IntakeMenuState.CaffeineEmpty:
                    sessionContext = CaffeineEmptyMenuHandler(sessionContext);
                    break;


                case IntakeMenuState.SupplementsOverview:
                    sessionContext = SupplementsOverviewMenuHandler(sessionContext);
                    break;


            }

            return sessionContext;
        }

        private async Task<TContext> DeleteCaffeineDrinkAsyc<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            // Get user input for confirmation
            bool confirmDelete = View.Input_Confirmation(MenuText.Prompt.DeleteCaffeineDrinkConfirmation);

            bool drinkDeleted = false;

            if (confirmDelete)
            {
                sessionContext?.CurrentDayCard?.CaffeineDrinksSummary?.CaffeineDrinksDetails.Remove(sessionContext.CurrentCaffeineDrink!);
                drinkDeleted = await _caffeineDrinkController.DeleteCaffeineDrinkAsync(sessionContext!.CurrentCaffeineDrink!.CaffeineDrinkId);

                if (drinkDeleted)
                {
                    sessionContext.CurrentCaffeineDrink = null;
                    Console.Clear();
                    Console.WriteLine(MenuText.Header.CaffeineDrinkDeleted);
                    Thread.Sleep(1500);
                    sessionContext.MainMenuState = MainMenuState.Main;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine(MenuText.Error.CaffeineDrinkDeleteFailed);
                    Thread.Sleep(1500);
                }

            }
            else
            {
                sessionContext.IntakeMenuState = IntakeMenuState.CaffeineDrinkDetails;
            }

            return sessionContext;
        }

        private async Task<TContext> UpdateCaffeineDrinkAsync<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            throw new NotImplementedException();
        }

        private TContext SupplementsOverviewMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            throw new NotImplementedException();
        }

        private TContext CaffeineOverviewMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);

            var caffeineMenuChoice = GetMenuValue(MenuText.NavOption.s_CaffeineOverviewMenu.ToList(), sessionContext);

            if (caffeineMenuChoice != null)
            {
                switch (caffeineMenuChoice)
                {
                    case MenuText.NavOption.ShowAllCaffeineDrinks:
                        sessionContext.IntakeMenuState = IntakeMenuState.ShowAllCaffeineDrinks;
                        break;

                    case MenuText.NavOption.AddCaffeine:
                        sessionContext.IntakeMenuState = IntakeMenuState.AddCaffeineDrink;
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

        private static TContext CaffeineShowAllMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);
            if (sessionContext!.CurrentDayCard!.CaffeineDrinksSummary != null && sessionContext.CurrentDayCard.CaffeineDrinksSummary.CaffeineDrinksDetails.Count > 0)
            {
                sessionContext.MainHeader = "Caffeine Drinks " + sessionContext.CurrentDayCard.Date + ":\n";

                var caffeineMenuChoice = GetMenuValue(sessionContext.CurrentDayCard.CaffeineDrinksSummary.CaffeineDrinksDetails, sessionContext);

                if (caffeineMenuChoice != null)
                {
                    sessionContext.CurrentCaffeineDrink = caffeineMenuChoice;
                    sessionContext.IntakeMenuState = IntakeMenuState.CaffeineDrinkDetails;
                }
                else
                {
                    sessionContext.IntakeMenuState = IntakeMenuState.CaffeineOverview;
                }

            }
            else
            {
                sessionContext.IntakeMenuState = IntakeMenuState.CaffeineEmpty;

            }
            return sessionContext;
        }

        private static TContext CaffeineEmptyMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);

            sessionContext.MainHeader = "No Caffeine Drinks found for this daycard.\n";

            var emptyCaffeineMenuChoice = GetMenuValue(MenuText.NavOption.s_CaffeineOverviewMenu.ToList(), sessionContext);

            if (emptyCaffeineMenuChoice != null)
            {
                switch (emptyCaffeineMenuChoice)
                {
                    case MenuText.NavOption.AddCaffeine:
                        sessionContext.IntakeMenuState = IntakeMenuState.AddCaffeineDrink;
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

        private TContext CaffeineDrinkDetailsMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {

            ResetMenuStates(sessionContext);
            var drinkDetailsChoice = GetMenuValue(MenuText.NavOption.s_CaffeineDetailsMenu.ToList(), sessionContext);

            if (drinkDetailsChoice is not null)
            {

                switch (drinkDetailsChoice)
                {
                    case MenuText.NavOption.UpdateCaffeineDrink:
                        sessionContext.IntakeMenuState = IntakeMenuState.UpdateCaffeineDrink;
                        break;
                    case MenuText.NavOption.DeleteCaffeineDrink:
                        sessionContext.IntakeMenuState = IntakeMenuState.DeleteCaffeineDrink;
                        break;

                    case MenuText.NavOption.Back:
                        sessionContext.IntakeMenuState = IntakeMenuState.CaffeineOverview;
                        break;
                }

            }
            else
            {
                sessionContext.IntakeMenuState = IntakeMenuState.ShowAllCaffeineDrinks;
            }
            return sessionContext;
        }

        private async Task<TContext> CreateCaffeineDrinkAsync<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);

            string? timeOf = View.Input_TimeOfIntake();

            if (timeOf != null)
            {
                var sizeOfDrinkChoice = GetMenuValue(MenuText.NavOption.s_DrinkSize.ToList(), sessionContext);

                if (sizeOfDrinkChoice != null)
                {

                    CaffeineDrinkInputModel caffeineDrinkInputModel = new CaffeineDrinkInputModel()
                    {
                        TimeOf = TimeOnly.Parse(timeOf)
                    };
                    switch (sizeOfDrinkChoice)
                    {
                        case MenuText.NavOption.DrinkMedium:
                            caffeineDrinkInputModel.SizeOfDrink = SizeOfDrink.Medium;
                            break;

                        case MenuText.NavOption.DrinkSmall:
                            caffeineDrinkInputModel.SizeOfDrink = SizeOfDrink.Small;
                            break;

                        case MenuText.NavOption.DrinkLarge:
                            caffeineDrinkInputModel.SizeOfDrink = SizeOfDrink.Large;
                            break;
                    }

                    //sessionContext
                    //    .DayCardDetailed!
                    //        .CaffeineDrinksSummary!
                    //            .CaffeineDrinksDetails
                    //                .Add
                    //                (await _caffeineDrinkController
                    //                    .AddCaffeineDrinkToDayCardAsync
                    //                    (sessionContext.DayCardDetailed!.DayCardId,
                    //                        caffeineDrinkInputModel
                    //                    )
                    //                );
                    sessionContext.CurrentCaffeineDrink = await _caffeineDrinkController
                                        .AddCaffeineDrinkToDayCardAsync(sessionContext
                                                                        .CurrentDayCard!.DayCardId,
                                                                        caffeineDrinkInputModel
                                                                        );

                    sessionContext
                        .CurrentDayCard!
                            .CaffeineDrinksSummary!
                                .CaffeineDrinksDetails
                                    .Add(sessionContext.CurrentCaffeineDrink!);

                    sessionContext.CurrentDayCard!.CaffeineDrinksSummary.TotalCaffeineInMg += sessionContext.CurrentCaffeineDrink.EstimatedMgCaffeine;
                }
                else
                {
                    sessionContext.IntakeMenuState = IntakeMenuState.CaffeineDrinkDetails;

                }

            }

            sessionContext.IntakeMenuState = IntakeMenuState.CaffeineOverview;

            return sessionContext;
        }
    }
}
