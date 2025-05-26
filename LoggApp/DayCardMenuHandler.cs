using System.Globalization;
using AppLogic.Controllers;
using AppLogic.Controllers.Interfaces;
using AppLogic.Models;
using AppLogic.Models.DTOs;
using AppLogic.Models.Intake.Enums;
using AppLogic.Models.Intake.InputModels;
using AppLogic.Services;
using Microsoft.IdentityModel.Tokens;
using Presentation.MenuState_Enums;

namespace Presentation
{
    public class DayCardMenuHandler : MenuHandlerBase
    {

        public override async Task<TContext> HandleMenuState<TContext>(TContext sessionContext)
        {

            switch (sessionContext.DayCardMenuState)
            {
                case DayCardMenuState.Overview:
                    sessionContext = SpecificDayCardMenuHandler(sessionContext);
                    break;


                case DayCardMenuState.AirQualityDetails:
                    sessionContext = await AirQualityDetailsMenuHandler(sessionContext);
                    break;

                case DayCardMenuState.PollenDetails:
                    sessionContext = await PollenDetailsMenuHandler(sessionContext);
                    break;

                case DayCardMenuState.WeatherDetails:
                    sessionContext = await WeatherDetailsMenuHandler(sessionContext);
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
                

                case DayCardMenuState.Back:
                    ResetMenuStates(sessionContext);
                    sessionContext.UserMenuState = UserMenuState.AllDayCards;
                    break;
            }

            return sessionContext;

        }



        


        public static TContext SpecificDayCardMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);
            sessionContext.MainHeader = MenuText.Header.SpecificUser + $"{sessionContext.UserDetailed!.ToString()}\n";
            sessionContext.SubHeader = MenuText.Header.CurrentDayCard + "(OVERVIEW):\n\n" + sessionContext.DayCardDetailed!.ToString() + "\n";

            List<string>? specificDayCardMenu = new List<string>()
            {
                MenuText.NavOption.Weather,
                MenuText.NavOption.AirQuality,
                MenuText.NavOption.Pollen,

            };

            if (sessionContext.DayCardDetailed.SupplementsSummary != null)
            {
                specificDayCardMenu.Add(MenuText.NavOption.Supplements);
            }
            else
            {
                specificDayCardMenu.Add(MenuText.NavOption.AddSupplements);
            }

            if (sessionContext.DayCardDetailed.CaffeineDrinksSummary != null && sessionContext.DayCardDetailed.CaffeineDrinksSummary.CaffeineDrinksDetails.Count > 0)
            {
                specificDayCardMenu.Add(MenuText.NavOption.CaffeineDrinks);
            }
            else
            {
                specificDayCardMenu.Add(MenuText.NavOption.AddCaffeine);
            }


            specificDayCardMenu.Add(MenuText.NavOption.Back);


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

                    case MenuText.NavOption.Back:
                        sessionContext.DayCardMenuState = DayCardMenuState.Back;
                        break;

                }

            }
            else
            {
                sessionContext.DayCardMenuState = DayCardMenuState.Back;
            }

            return sessionContext;

        }
        private async Task<TContext> WeatherDetailsMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            Console.Clear();
            Console.WriteLine(MenuText.Header.SpecificUser + $"{sessionContext.UserDetailed!.ToString()}");
            Console.WriteLine(sessionContext.DayCardDetailed.WeatherSummary.ToString());
            Console.ReadLine();
            sessionContext.DayCardMenuState = DayCardMenuState.Overview;
            return sessionContext;
        }
        private async Task<TContext> AirQualityDetailsMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            Console.Clear();
            Console.WriteLine(MenuText.Header.SpecificUser + $"{sessionContext.UserDetailed!.ToString()}");
            Console.WriteLine(sessionContext.DayCardDetailed.AirQualitySummary.ToString());
            Console.ReadLine();
            sessionContext.DayCardMenuState = DayCardMenuState.Overview;
            return sessionContext;
        }

        private async Task<TContext> PollenDetailsMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            Console.Clear();
            Console.WriteLine(MenuText.Header.SpecificUser + $"{sessionContext.UserDetailed!.ToString()}");
            Console.WriteLine(sessionContext.DayCardDetailed.PollenSummary.ToString());
            Console.ReadLine();
            sessionContext.DayCardMenuState = DayCardMenuState.Overview;
            return sessionContext;
        }

        

        

    }

}
