using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Controllers;
using AppLogic.Models.DTOs.Detailed;
using AppLogic.Models.DTOs.Summary;
using AppLogic.Models.Enums;
using AppLogic.Models.InputModels;
using Presentation.Input;
using Presentation.MenuState_Enums;

namespace Presentation.MenuHandlers
{
    /// <summary>
    /// Handles the intake-related menu states in the console application, specifically for caffeine drinks(for now).
    /// </summary>
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


                //case IntakeMenuState.SupplementsOverview:
                //    sessionContext = SupplementsOverviewMenuHandler(sessionContext);
                //    break;


            }

            return sessionContext;
        }

        private async Task<TContext> DeleteCaffeineDrinkAsyc<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            // Get user input for confirmation
            bool confirmDelete = ConsoleInput.Input_Confirmation(MenuText.Prompt.DeleteCaffeineDrinkConfirmation);

            bool drinkDeleted = false;

            if (confirmDelete)
            {
                var drinkSummary = sessionContext?.CurrentDayCard?.CaffeineDrinksSummaries?
                    .FirstOrDefault(cd => cd.Id == sessionContext!.CurrentCaffeineDrink!.Id)!;

                sessionContext!.CurrentDayCard!.CaffeineDrinksSummaries?.Remove(drinkSummary);

                drinkDeleted = await _caffeineDrinkController.DeleteCaffeineDrinkAsync(sessionContext!.CurrentCaffeineDrink!.Id);

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

        private TContext CaffeineOverviewMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);

            var caffeineMenuChoice = MenuNavigation.GetMenuValue(MenuText.NavOption.s_CaffeineOverviewMenu.ToList(), sessionContext);

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
            if (sessionContext!.CurrentDayCard!.CaffeineDrinksSummaries != null && sessionContext.CurrentDayCard.CaffeineDrinksSummaries.Count > 0)
            {
                sessionContext.MainHeader = "Caffeine Drinks " + sessionContext.CurrentDayCard.Date + ":\n";

                var caffeineMenuChoice = MenuNavigation.GetMenuValue(sessionContext.CurrentDayCard.CaffeineDrinksSummaries, sessionContext);

                if (caffeineMenuChoice != null)
                {
                    sessionContext.CurrentCaffeineDrink = new CaffeineDrinkDetailed(caffeineMenuChoice);

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

            var emptyCaffeineMenuChoice = MenuNavigation.GetMenuValue(MenuText.NavOption.s_CaffeineOverviewMenu.ToList(), sessionContext);

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

            sessionContext.MainHeader = sessionContext.CurrentCaffeineDrink!.ToString();

            var drinkDetailsChoice = MenuNavigation.GetMenuValue(MenuText.NavOption.s_CaffeineDetailsMenu.ToList(), sessionContext);

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

        private async Task<TContext> UpdateCaffeineDrinkAsync<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);

            // Re-input the standard values as a whole
            string? timeOf = ConsoleInput.Input_TimeOfIntake();

            if (timeOf != null)
            {
                if (!TimeOnly.TryParse(timeOf, out TimeOnly parsedTime))
                {
                    Console.Clear();
                    Console.WriteLine(MenuText.Error.InvalidDayCardInput);
                    Thread.Sleep(1500);
                    sessionContext.IntakeMenuState = IntakeMenuState.CaffeineDrinkDetails;
                    return sessionContext;
                }

                var sizeOfDrinkChoice = MenuNavigation.GetMenuValue(MenuText.NavOption.s_DrinkSize.ToList(), sessionContext);

                if (sizeOfDrinkChoice != null)
                {
                    var updateInputModel = new CaffeineDrinkInputModel()
                    {
                        TimeOf = parsedTime
                    };

                    switch (sizeOfDrinkChoice)
                    {
                        case MenuText.NavOption.DrinkMedium:
                            updateInputModel.SizeOfDrink = SizeOfDrink.Medium;
                            break;
                        case MenuText.NavOption.DrinkSmall:
                            updateInputModel.SizeOfDrink = SizeOfDrink.Small;
                            break;
                        case MenuText.NavOption.DrinkLarge:
                            updateInputModel.SizeOfDrink = SizeOfDrink.Large;
                            break;
                    }

                    // Assuming a controller method exists for updating a caffeine drink.
                    sessionContext.CurrentCaffeineDrink = await _caffeineDrinkController
                        .UpdateCaffeineDrinkAsync(sessionContext.CurrentCaffeineDrink!.Id, updateInputModel);

                    Console.Clear();
                    Console.WriteLine("CAFFEINE DRINK UPDATED");
                    Thread.Sleep(1500);



                    // Update the drink details in the current day card.
                    var drinkSummary = sessionContext.CurrentDayCard!.CaffeineDrinksSummaries!
                        .FirstOrDefault(x => x.Id == sessionContext.CurrentCaffeineDrink!.Id);

                    if (drinkSummary != null)
                    {
                        // Update the drink details with the new values.
                        drinkSummary.DayCardId = sessionContext.CurrentCaffeineDrink!.DayCardId;
                        drinkSummary.TimeOf = sessionContext.CurrentCaffeineDrink!.TimeOf;
                        drinkSummary.EstimatedMgCaffeine = sessionContext.CurrentCaffeineDrink!.EstimatedMgCaffeine;
                        
                    }

                    sessionContext.CurrentDayCard!.UpdateTotalValues();

                }
                else
                {
                    sessionContext.IntakeMenuState = IntakeMenuState.CaffeineDrinkDetails;
                }
            }

            sessionContext.IntakeMenuState = IntakeMenuState.CaffeineOverview;
            return sessionContext;
        }

        private async Task<TContext> CreateCaffeineDrinkAsync<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);

            string? timeOf = ConsoleInput.Input_TimeOfIntake();

            if (timeOf != null)
            {
                if (!TimeOnly.TryParse(timeOf, out TimeOnly parsedTime))
                {
                    Console.Clear();
                    Console.WriteLine(MenuText.Error.InvalidDayCardInput);
                    Thread.Sleep(1500);
                    sessionContext.IntakeMenuState = IntakeMenuState.CaffeineOverview;
                    return sessionContext;
                }

                var sizeOfDrinkChoice = MenuNavigation.GetMenuValue(MenuText.NavOption.s_DrinkSize.ToList(), sessionContext);

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

                    sessionContext.CurrentCaffeineDrink = await _caffeineDrinkController
                        .AddCaffeineDrinkToDayCardAsync(
                            sessionContext.CurrentDayCard!.DayCardId,
                            caffeineDrinkInputModel);

                    sessionContext
                        .CurrentDayCard!
                            .CaffeineDrinksSummaries!
                                    .Add(new CaffeineDrinkSummary(sessionContext.CurrentCaffeineDrink));

                    sessionContext.CurrentDayCard!.UpdateTotalValues();

                    sessionContext.CurrentUser!.AllDayCardsSummary!
                        .FirstOrDefault(x => x.DayCardId == sessionContext.CurrentDayCard!.DayCardId)!.Entries++;
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
