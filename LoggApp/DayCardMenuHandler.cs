using System.Globalization;
using AppLogic.Controllers;
using AppLogic.Models;
using AppLogic.Models.DTOs;
using AppLogic.Models.InputModels;
using AppLogic.Services;
using Microsoft.IdentityModel.Tokens;
using Presentation.MenuState_Enums;

namespace Presentation
{
    public class DayCardMenuHandler : MenuHandlerBase
    {
        private readonly DayCardController _dayCardController;

        public DayCardMenuHandler(DayCardController dayCardController)
        {
            _dayCardController = dayCardController;
        }

        public override async Task<TContext> HandleMenuState<TContext>(TContext sessionContext)
        {

            switch (sessionContext.DayCardMenuState)
            {
                case DayCardMenuState.Overview:
                    sessionContext = SpecificDayCardMenuHandler(sessionContext);
                    break;


                case DayCardMenuState.AirQualityDetails:
                    sessionContext = AirQualityDetailsMenuHandler(sessionContext);
                    break;

                case DayCardMenuState.PollenDetails:
                    sessionContext = PollenDetailsMenuHandler(sessionContext);
                    break;

                case DayCardMenuState.WeatherDetails:
                    sessionContext = WeatherDetailsMenuHandler(sessionContext);
                    break;
                //// EXERCISE
                //case DayCardMenuState.AddExercise:
                //    sessionContext = await ExerciseCreateMenuHandler(sessionContext);
                //    break;

                //case DayCardMenuState.ExerciseDetails:
                //    sessionContext = await ExerciseDetailsMenuHandler(sessionContext);
                //    break;

                //// SLEEP
                //case DayCardMenuState.AddSleep:
                //    sessionContext = await SleepCreateMenuHandler(sessionContext);
                //    break;
                //case DayCardMenuState.SleepDetails:
                //    sessionContext = await SleepDetailsMenuHandler;
                //    break;

                case DayCardMenuState.UpdateDayCard:
                    sessionContext = await UpdateDayCardDateAsync(sessionContext);
                    break;

                case DayCardMenuState.DeleteDayCard:
                    sessionContext = await DayCardDeleteMenuHandler(sessionContext);
                    break;

            }

            return sessionContext;

        }

        private async Task<TContext> UpdateDayCardDateAsync<TContext>(TContext sessionContext) where TContext : SessionContext
        {

            ResetMenuStates(sessionContext);

            // Get user input for date
            DayCardInputModel? dayCardInputModel = View.Input_DayCard(sessionContext);

            if (dayCardInputModel != null)
            {
                sessionContext.CurrentDayCard = await _dayCardController.CreateNewDayCardAsync(sessionContext.CurrentUser!.Id, dayCardInputModel);
                sessionContext.DayCardMenuState = DayCardMenuState.Overview;

            }
            else
            {
                sessionContext.MainMenuState = MainMenuState.SpecificUser;
            }
            return sessionContext;
        }

        private async Task<TContext> DayCardDeleteMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {

            // Get user input for confirmation
            bool confirmDelete = View.Input_Confirmation(MenuText.Prompt.DeleteDayCardConfirmation);

            if (confirmDelete)
            {
                bool dayCardDelete = await _dayCardController.DeleteDayCardAsync(sessionContext.CurrentDayCard!.DayCardId);

                if (dayCardDelete)
                {
                    sessionContext.CurrentUser = null;
                    Console.Clear();
                    Console.WriteLine(MenuText.Header.DayCardDeleted);
                    Thread.Sleep(1500);
                    sessionContext.MainMenuState = MainMenuState.Main;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine(MenuText.Error.DayCardDeleteFailed);
                    Thread.Sleep(1500);
                    sessionContext.UserMenuState = UserMenuState.SpecificDayCard;
                }
            }
            else
            {
                sessionContext.DayCardMenuState = DayCardMenuState.Overview;
            }
            return sessionContext;
        }

        public static TContext SpecificDayCardMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);
            sessionContext.MainHeader = MenuText.Header.SpecificUser + $"{sessionContext.CurrentUser!.ToString()}\n";
            sessionContext.SubHeader = MenuText.Header.CurrentDayCard + "(OVERVIEW):\n\n" + sessionContext.CurrentDayCard!.ToString() + "\n";

            List<string>? specificDayCardMenu = new List<string>()
            {
                MenuText.NavOption.Weather,
                MenuText.NavOption.AirQuality,
                MenuText.NavOption.Pollen,

            };

            if (sessionContext.CurrentDayCard.SupplementsSummary != null)
            {
                specificDayCardMenu.Add(MenuText.NavOption.Supplements);
            }
            else
            {
                specificDayCardMenu.Add(MenuText.NavOption.AddSupplements);
            }

            if (sessionContext.CurrentDayCard.CaffeineDrinksSummary != null && sessionContext.CurrentDayCard.CaffeineDrinksSummary.CaffeineDrinksDetails.Count > 0)
            {
                specificDayCardMenu.Add(MenuText.NavOption.CaffeineDrinks);
            }
            else
            {
                specificDayCardMenu.Add(MenuText.NavOption.AddCaffeine);
            }


            specificDayCardMenu.Add(MenuText.NavOption.Back);
            specificDayCardMenu.Add(MenuText.NavOption.DeleteDayCard);


            var specUserChoice = GetMenuValue(specificDayCardMenu, sessionContext);

            if (specUserChoice != null)
            {
                switch (specUserChoice)
                {
                    case MenuText.NavOption.Weather:
                        sessionContext.DayCardMenuState = DayCardMenuState.WeatherDetails;
                        break;

                    case MenuText.NavOption.AirQuality:
                        sessionContext.DayCardMenuState = DayCardMenuState.AirQualityDetails;
                        break;

                    case MenuText.NavOption.Pollen:
                        sessionContext.DayCardMenuState = DayCardMenuState.PollenDetails;
                        break;

                    case MenuText.NavOption.Supplements:
                        ResetMenuStates(sessionContext);
                        sessionContext.IntakeMenuState = IntakeMenuState.SupplementsOverview;
                        break;

                    case MenuText.NavOption.AddSupplements:
                        ResetMenuStates(sessionContext);
                        sessionContext.IntakeMenuState = IntakeMenuState.AddSupplements;
                        break;

                    case MenuText.NavOption.CaffeineDrinks:
                        ResetMenuStates(sessionContext);
                        sessionContext.IntakeMenuState = IntakeMenuState.CaffeineOverview;
                        break;

                    case MenuText.NavOption.AddCaffeine:
                        ResetMenuStates(sessionContext);
                        sessionContext.IntakeMenuState = IntakeMenuState.AddCaffeineDrink;
                        break;

                    case MenuText.NavOption.Exercise:
                        sessionContext.DayCardMenuState = DayCardMenuState.ExerciseDetails;
                        break;

                    case MenuText.NavOption.ComputerActivity:
                        sessionContext.DayCardMenuState = DayCardMenuState.ComputerActivity;
                        break;

                    case MenuText.NavOption.Sleep:
                        break;

                    case MenuText.NavOption.DeleteDayCard:
                        sessionContext.DayCardMenuState = DayCardMenuState.DeleteDayCard;
                        break;

                    case MenuText.NavOption.Back:
                        sessionContext.UserMenuState = UserMenuState.AllDayCards;
                        break;
                }
            }
            else
            {
                sessionContext.UserMenuState = UserMenuState.AllDayCards;
            }

            return sessionContext;

        }
        private TContext WeatherDetailsMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            Console.Clear();
            Console.WriteLine(MenuText.Header.SpecificUser + $"{sessionContext.CurrentUser!.ToString()}");
            Console.WriteLine(sessionContext.CurrentDayCard.WeatherSummary.ToString());
            Console.ReadLine();
            sessionContext.DayCardMenuState = DayCardMenuState.Overview;
            return sessionContext;
        }
        private TContext AirQualityDetailsMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            Console.Clear();
            Console.WriteLine(MenuText.Header.SpecificUser + $"{sessionContext.CurrentUser!.ToString()}");
            Console.WriteLine(sessionContext.CurrentDayCard.AirQualitySummary.ToString());
            Console.ReadLine();
            sessionContext.DayCardMenuState = DayCardMenuState.Overview;
            return sessionContext;
        }

        private TContext PollenDetailsMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            Console.Clear();
            Console.WriteLine(MenuText.Header.SpecificUser + $"{sessionContext.CurrentUser!.ToString()}");
            Console.WriteLine(sessionContext.CurrentDayCard.PollenSummary.ToString());
            Console.ReadLine();
            sessionContext.DayCardMenuState = DayCardMenuState.Overview;
            return sessionContext;
        }

        

        

    }

}
