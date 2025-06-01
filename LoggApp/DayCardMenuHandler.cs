using System.Globalization;
using AppLogic.Controllers;
using AppLogic.Models;
using AppLogic.Models.DTOs;
using AppLogic.Models.DTOs.Summary;
using AppLogic.Models.InputModels;
using AppLogic.Services;
using Microsoft.IdentityModel.Tokens;
using Presentation.Input;
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
                    sessionContext = DayCardOverviewMenuHandler(sessionContext);
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
            DayCardInputModel? dayCardInputModel = ConsoleInput.Input_DayCard(sessionContext);

            if (dayCardInputModel != null)
            {
                
                sessionContext.CurrentDayCard = await _dayCardController.UpdateDayCardDateAsync(sessionContext.CurrentDayCard!.DayCardId, dayCardInputModel);

                // Update client-side on the current user aswell
                sessionContext.CurrentUser!.AllDayCardsSummary!
                    .FirstOrDefault(x => x.DayCardId == sessionContext.CurrentDayCard.DayCardId)!.Date = sessionContext.CurrentDayCard.Date;

                sessionContext.DayCardMenuState = DayCardMenuState.Overview;
            }
            else
            {
                sessionContext.DayCardMenuState = DayCardMenuState.Overview;
            }
            return sessionContext;
        }

        private async Task<TContext> DayCardDeleteMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);

            // Get user input for confirmation
            bool confirmDelete = ConsoleInput.Input_Confirmation(MenuText.Prompt.DeleteDayCardConfirmation);

            if (confirmDelete)
            {
                bool dayCardDelete = await _dayCardController.DeleteDayCardAsync(sessionContext.CurrentDayCard!.DayCardId);

                if (dayCardDelete)
                {
                    sessionContext.CurrentDayCard = null;
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

        public static TContext DayCardOverviewMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
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

            if (sessionContext.CurrentDayCard.SleepDetails is null)
            {
                specificDayCardMenu.Add(MenuText.NavOption.AddSleep);
            }
            else
            {
                specificDayCardMenu.Add(MenuText.NavOption.SleepDetails);
            }


            if (sessionContext.CurrentDayCard.ExercisesSummaries != null && sessionContext.CurrentDayCard.ExercisesSummaries.Count > 0)
            {
                specificDayCardMenu.Add(MenuText.NavOption.Exercise);
            }
            else
            {
                specificDayCardMenu.Add(MenuText.NavOption.AddExercise);
            }

            if (sessionContext.CurrentDayCard.CaffeineDrinksSummaries != null && sessionContext.CurrentDayCard.CaffeineDrinksSummaries.Count > 0)
            {
                specificDayCardMenu.Add(MenuText.NavOption.CaffeineDrinks);
            }
            else
            {
                specificDayCardMenu.Add(MenuText.NavOption.AddCaffeine);
            }


            specificDayCardMenu.Add(MenuText.NavOption.Back);
            specificDayCardMenu.Add(MenuText.NavOption.ChangeDayCardDate);
            specificDayCardMenu.Add(MenuText.NavOption.DeleteDayCard);


            var specUserChoice = MenuNavigation.GetMenuValue(specificDayCardMenu, sessionContext);

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

                    case MenuText.NavOption.AddSleep:
                        sessionContext.SleepMenuState = SleepMenuState.AddSleep;
                        break;

                    case MenuText.NavOption.SleepDetails:
                        sessionContext.SleepMenuState = SleepMenuState.SleepDetails;
                        break;

                    case MenuText.NavOption.CaffeineDrinks:
                        sessionContext.IntakeMenuState = IntakeMenuState.CaffeineOverview;
                        break;

                    case MenuText.NavOption.AddCaffeine:
                        sessionContext.IntakeMenuState = IntakeMenuState.AddCaffeineDrink;
                        break;

                    case MenuText.NavOption.AddExercise:
                        sessionContext.ActivityMenuState = ActivityMenuState.AddExercise;
                        break;

                    case MenuText.NavOption.Exercise:
                        sessionContext.ActivityMenuState = ActivityMenuState.ExerciseOverview;
                        break;


                    case MenuText.NavOption.ChangeDayCardDate:
                        sessionContext.DayCardMenuState = DayCardMenuState.UpdateDayCard;
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
            ResetMenuStates(sessionContext);
            Console.Clear();
            Console.WriteLine(MenuText.Header.SpecificUser + $"{sessionContext.CurrentUser!.ToString()}");
            Console.WriteLine(sessionContext.CurrentDayCard.WeatherSummary.ToString());
            Console.ReadLine();
            sessionContext.DayCardMenuState = DayCardMenuState.Overview;
            return sessionContext;
        }
        private TContext AirQualityDetailsMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);
            Console.Clear();
            Console.WriteLine(MenuText.Header.SpecificUser + $"{sessionContext.CurrentUser!.ToString()}");
            Console.WriteLine(sessionContext.CurrentDayCard.AirQualitySummary.ToString());
            Console.ReadLine();
            sessionContext.DayCardMenuState = DayCardMenuState.Overview;
            return sessionContext;
        }

        private TContext PollenDetailsMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);
            Console.Clear();
            Console.WriteLine(MenuText.Header.SpecificUser + $"{sessionContext.CurrentUser!.ToString()}");
            Console.WriteLine(sessionContext.CurrentDayCard.PollenSummary.ToString());
            Console.ReadLine();
            sessionContext.DayCardMenuState = DayCardMenuState.Overview;
            return sessionContext;
        }





    }

}
