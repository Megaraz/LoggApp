using System.Globalization;
using AppLogic.Controllers;
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
        public DayCardMenuHandler(Controller controller) : base(controller)
        {
        }

        public override async Task<TContext> HandleMenuState<TContext>(TContext sessionContext)
        {

            switch (sessionContext.DayCardMenuState)
            {
                case DayCardMenuState.Overview:
                    SpecificDayCardMenuHandler(sessionContext);
                    break;

                case DayCardMenuState.AllData:
                    //sessionContext = ;
                    break;

                case DayCardMenuState.AirQualityDetails:
                    await AirQualityDetailsMenuHandler(sessionContext);
                    break;

                case DayCardMenuState.PollenDetails:
                    await PollenDetailsMenuHandler(sessionContext);
                    break;

                case DayCardMenuState.WeatherDetails:
                    await WeatherDetailsMenuHandler(sessionContext);
                    break;

                case DayCardMenuState.AddCaffeineDrink:
                    await CaffeineCreateMenuHandler(sessionContext);
                    break;

                case DayCardMenuState.Sleep:
                    //sessionContext = ;
                    break;

                case DayCardMenuState.Supplements:
                    //sessionContext = ;
                    break;

                case DayCardMenuState.Back:
                    ResetMenuStates(sessionContext);
                    sessionContext.UserMenuState = UserMenuState.AllDayCards;
                    break;
            }

            return sessionContext;

        }

        private async Task<TContext> CaffeineCreateMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
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

                    sessionContext
                        .DTO_CurrentDayCard!
                            .CaffeineDrinksSummary!
                                .HourlyCaffeineData
                                    .Add
                                    (await _controller
                                        .AddCaffeineDrinkToDayCardAsync
                                        (sessionContext.DTO_CurrentDayCard!.DayCardId,
                                            caffeineDrinkInputModel
                                        )
                                    );

                }
            }
            sessionContext.DayCardMenuState = DayCardMenuState.Overview;

            return sessionContext;
        }


        public static TContext SpecificDayCardMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);
            sessionContext.MainHeader = MenuText.Header.SpecificUser + $"{sessionContext.DTO_CurrentUser!.ToString()}";
            sessionContext.SubHeader = MenuText.Header.CurrentDayCard + "\n[OVERVIEW]\n" + sessionContext.DTO_CurrentDayCard!.ToString();

            List<string>? specificDayCardMenu = new List<string>()
            {
                MenuText.NavOption.Weather,
                MenuText.NavOption.AirQuality,
                MenuText.NavOption.Pollen,

            };

            if (sessionContext.DTO_CurrentDayCard.SupplementsSummary != null)
            {
                specificDayCardMenu.Add(MenuText.NavOption.Supplements);
            }
            else
            {
                specificDayCardMenu.Add(MenuText.NavOption.AddSupplements);
            }

            if (sessionContext.DTO_CurrentDayCard.CaffeineDrinksSummary != null)
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
                        sessionContext.DayCardMenuState = DayCardMenuState.Supplements;
                        break;

                    case MenuText.NavOption.AddCaffeine:
                        sessionContext.DayCardMenuState = DayCardMenuState.AddCaffeineDrink;
                        break;

                    case MenuText.NavOption.CaffeineDrinks:
                        sessionContext.DayCardMenuState = DayCardMenuState.CaffeineDrinkDetails;
                        break;

                    case MenuText.NavOption.Exercise:
                        sessionContext.DayCardMenuState = DayCardMenuState.Exercise;
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
            Console.WriteLine(MenuText.Header.SpecificUser + $"{sessionContext.DTO_CurrentUser!.ToString()}");
            Console.WriteLine(sessionContext.DTO_CurrentDayCard.WeatherSummary.ToString());
            Console.ReadLine();
            sessionContext.DayCardMenuState = DayCardMenuState.Overview;
            return sessionContext;
        }
        private async Task<TContext> AirQualityDetailsMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            Console.Clear();
            Console.WriteLine(MenuText.Header.SpecificUser + $"{sessionContext.DTO_CurrentUser!.ToString()}");
            Console.WriteLine(sessionContext.DTO_CurrentDayCard.AirQualitySummary.ToString());
            Console.ReadLine();
            sessionContext.DayCardMenuState = DayCardMenuState.Overview;
            return sessionContext;
        }

        private async Task<TContext> PollenDetailsMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            Console.Clear();
            Console.WriteLine(sessionContext.DTO_CurrentDayCard.PollenSummary.ToString());
            Console.WriteLine(MenuText.Header.SpecificUser + $"{sessionContext.DTO_CurrentUser!.ToString()}");
            Console.ReadLine();
            sessionContext.DayCardMenuState = DayCardMenuState.Overview;
            return sessionContext;
        }

        

        

    }

}
