using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Controllers;
using AppLogic.Models.DTOs.Detailed;
using AppLogic.Models.DTOs.Summary;
using AppLogic.Models.Entities;
using AppLogic.Models.InputModels;
using Presentation.Input;
using Presentation.MenuState_Enums;

namespace Presentation.MenuHandlers
{
    /// <summary>
    /// Handles the wellness check-in menu states and interactions in the console application.
    /// </summary>
    public class WellnessMenuHandler : MenuHandlerBase
    {
        private readonly WellnessCheckInController _wellnessController;

        public WellnessMenuHandler(WellnessCheckInController wellnessController)
        {
            _wellnessController = wellnessController;
        }

        public override async Task<TContext> HandleMenuState<TContext>(TContext sessionContext)
        {
            switch (sessionContext.WellnessCheckInMenuState)
            {
                case WellnessCheckInMenuState.CheckInOverview:
                    sessionContext = WellnessOverviewMenuHandler(sessionContext);
                    break;

                case WellnessCheckInMenuState.ShowAllCheckIns:
                    sessionContext = WellnessShowAllMenuHandler(sessionContext);
                    break;

                case WellnessCheckInMenuState.CheckInDetails:
                    sessionContext = WellnessDetailsMenuHandler(sessionContext);
                    break;

                case WellnessCheckInMenuState.AddCheckIn:
                    sessionContext = await CreateCheckInAsync(sessionContext);
                    break;

                case WellnessCheckInMenuState.UpdateCheckIn:
                    sessionContext = await UpdateCheckInAsync(sessionContext);
                    break;

                case WellnessCheckInMenuState.DeleteCheckIn:
                    sessionContext = await DeleteCheckInAsync(sessionContext);
                    break;
            }

            return sessionContext;
        }

        private TContext WellnessOverviewMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);

            var menuChoice = MenuNavigation.GetMenuValue(MenuText.NavOption.s_WellnessCheckInOverviewMenu.ToList(), sessionContext);

            if (menuChoice != null)
            {
                switch (menuChoice)
                {
                    case MenuText.NavOption.ShowAllCheckins:
                        sessionContext.WellnessCheckInMenuState = WellnessCheckInMenuState.ShowAllCheckIns;
                        break;
                    case MenuText.NavOption.AddWellnessCheckIn:
                        sessionContext.WellnessCheckInMenuState = WellnessCheckInMenuState.AddCheckIn;
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

        private static TContext WellnessShowAllMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);

            if (sessionContext!.CurrentDayCard?.WellnessCheckInsSummaries != null &&
                sessionContext.CurrentDayCard.WellnessCheckInsSummaries.Count > 0)
            {
                sessionContext.MainHeader = "Wellness Check-Ins " + sessionContext.CurrentDayCard.Date + ":\n";

                sessionContext.SubHeader = MenuText.Header.AllCheckins;

                var checkInMenuChoice = MenuNavigation.GetMenuValue(sessionContext.CurrentDayCard!.WellnessCheckInsSummaries, sessionContext);

                if (checkInMenuChoice != null)
                {
                    sessionContext.CurrentWellnessCheckIn = new WellnessCheckInDetailed(checkInMenuChoice);
                    sessionContext.WellnessCheckInMenuState = WellnessCheckInMenuState.CheckInDetails;
                }
                else
                {
                    sessionContext.WellnessCheckInMenuState = WellnessCheckInMenuState.CheckInOverview;
                }
            }
            else
            {
                sessionContext.WellnessCheckInMenuState = WellnessCheckInMenuState.CheckInOverview;
            }
            return sessionContext;
        }

        private TContext WellnessDetailsMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);

            sessionContext.MainHeader = sessionContext.CurrentWellnessCheckIn?.ToString() ?? "No check-in details available.";

            var detailsChoice = MenuNavigation.GetMenuValue(MenuText.NavOption.s_WellnessCheckInDetailsMenu.ToList(), sessionContext);

            if (detailsChoice is not null)
            {
                switch (detailsChoice)
                {
                    case MenuText.NavOption.UpdateWellnessCheckIn:
                        sessionContext.WellnessCheckInMenuState = WellnessCheckInMenuState.UpdateCheckIn;
                        break;
                    case MenuText.NavOption.DeleteWellnessCheckIn:
                        sessionContext.WellnessCheckInMenuState = WellnessCheckInMenuState.DeleteCheckIn;
                        break;
                    case MenuText.NavOption.Back:
                        sessionContext.WellnessCheckInMenuState = WellnessCheckInMenuState.CheckInOverview;
                        break;
                }
            }
            else
            {
                sessionContext.WellnessCheckInMenuState = WellnessCheckInMenuState.ShowAllCheckIns;
            }
            return sessionContext;
        }

        private async Task<TContext> CreateCheckInAsync<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);

            var timeOfCheckIn = ConsoleInput.Input_TimeOfCheckIn();
            if (timeOfCheckIn != null)
            {

                if (!TimeOnly.TryParseExact(timeOfCheckIn, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out TimeOnly parsedTimeOfCheckIn))
                {
                    Console.Clear();
                    Console.WriteLine("Invalid time input.");
                    Thread.Sleep(1500);
                    sessionContext.DayCardMenuState = DayCardMenuState.Overview;
                    return sessionContext;
                }
                sessionContext.CurrentPrompt = "Energy level:";
                var energyLevelChoice = MenuNavigation.GetMenuValue(MenuText.NavOption.s_WellnessRating.ToList(), sessionContext);
                sessionContext.CurrentPrompt = "Mood level:";
                var moodLevelChoice = MenuNavigation.GetMenuValue(MenuText.NavOption.s_WellnessRating.ToList(), sessionContext);

                if (energyLevelChoice != null && moodLevelChoice != null)
                {
                    var checkInInputModel = new WellnessCheckInInputModel
                    {
                        TimeOf = parsedTimeOfCheckIn,
                    };
                    checkInInputModel.MoodLevel = GetRatingLevelChoice(moodLevelChoice);
                    checkInInputModel.EnergyLevel = GetRatingLevelChoice(energyLevelChoice);

                    if (checkInInputModel is null)
                    {
                        sessionContext.DayCardMenuState = DayCardMenuState.Overview;
                        return sessionContext;
                    }

                    sessionContext.CurrentWellnessCheckIn = await _wellnessController.AddCheckInToDayCardAsync(sessionContext.CurrentDayCard!.DayCardId, checkInInputModel);

                    sessionContext.CurrentDayCard!.WellnessCheckInsSummaries ??= new List<WellnessCheckInSummary>();
                    sessionContext.CurrentDayCard!.WellnessCheckInsSummaries
                        .Add(new WellnessCheckInSummary(sessionContext.CurrentWellnessCheckIn!));

                    sessionContext.CurrentDayCard.UpdateTotalValues();
                }
                else
                {
                    sessionContext.WellnessCheckInMenuState = WellnessCheckInMenuState.CheckInDetails;
                }
            }
            sessionContext.WellnessCheckInMenuState = WellnessCheckInMenuState.CheckInOverview;

            return sessionContext;
        }

        private static RatingLevel? GetRatingLevelChoice(string choice)
        {
            return choice switch
            {
                MenuText.NavOption.WellnessRatingVeryLow => RatingLevel.VeryLow,
                MenuText.NavOption.WellnessRatingLow => RatingLevel.Low,
                MenuText.NavOption.WellnessRatingMedium => RatingLevel.Medium,
                MenuText.NavOption.WellnessRatingHigh => RatingLevel.High,
                MenuText.NavOption.WellnessRatingVeryHigh => RatingLevel.VeryHigh,
                _ => null
            };
        }

        private async Task<TContext> UpdateCheckInAsync<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);

            var timeOfCheckIn = ConsoleInput.Input_TimeOfCheckIn();
            if (timeOfCheckIn != null)
            {

                if (!TimeOnly.TryParseExact(timeOfCheckIn, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out TimeOnly parsedTimeOfCheckIn))
                {
                    Console.Clear();
                    Console.WriteLine("Invalid time input.");
                    Thread.Sleep(1500);
                    sessionContext.DayCardMenuState = DayCardMenuState.Overview;
                    return sessionContext;
                }
                sessionContext.CurrentPrompt = "Energy level:";
                var energyLevelChoice = MenuNavigation.GetMenuValue(MenuText.NavOption.s_WellnessRating.ToList(), sessionContext);
                sessionContext.CurrentPrompt = "Mood level:";
                var moodLevelChoice = MenuNavigation.GetMenuValue(MenuText.NavOption.s_WellnessRating.ToList(), sessionContext);

                if (energyLevelChoice != null && moodLevelChoice != null)
                {
                    var updateInputModel = new WellnessCheckInInputModel
                    {
                        TimeOf = parsedTimeOfCheckIn,
                    };
                    updateInputModel.MoodLevel = GetRatingLevelChoice(moodLevelChoice);
                    updateInputModel.EnergyLevel = GetRatingLevelChoice(energyLevelChoice);

                    if (updateInputModel is null)
                    {
                        sessionContext.DayCardMenuState = DayCardMenuState.Overview;
                        return sessionContext;
                    }

                    sessionContext.CurrentWellnessCheckIn = await _wellnessController.UpdateCheckInAsync(sessionContext.CurrentWellnessCheckIn!.Id, updateInputModel);

                    Console.Clear();
                    Console.WriteLine("WELLNESS CHECK-IN UPDATED");
                    Thread.Sleep(1500);

                    var checkInSummary = sessionContext.CurrentDayCard!.WellnessCheckInsSummaries!
                        .FirstOrDefault(x => x.Id == sessionContext.CurrentWellnessCheckIn!.Id);

                    if (checkInSummary != null)
                    {
                        checkInSummary.Id = sessionContext.CurrentWellnessCheckIn!.Id;
                        checkInSummary.DayCardId = sessionContext.CurrentDayCard.DayCardId;
                        checkInSummary.TimeOf = sessionContext.CurrentWellnessCheckIn!.TimeOf;
                        checkInSummary.EnergyLevel = sessionContext.CurrentWellnessCheckIn!.EnergyLevel;
                        checkInSummary.MoodLevel = sessionContext.CurrentWellnessCheckIn!.MoodLevel;
                    }

                    sessionContext.CurrentDayCard.UpdateTotalValues();
                }
                else
                {
                    sessionContext.WellnessCheckInMenuState = WellnessCheckInMenuState.CheckInDetails;
                }
            }

            sessionContext.WellnessCheckInMenuState = WellnessCheckInMenuState.CheckInOverview;
            return sessionContext;
        }

        private async Task<TContext> DeleteCheckInAsync<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            bool confirmDelete = ConsoleInput.Input_Confirmation(MenuText.Prompt.DeleteCheckInConfirmation);
            bool checkInDeleted = false;

            if (confirmDelete)
            {
                var checkInSummary = sessionContext!.CurrentDayCard!.WellnessCheckInsSummaries!
                    .FirstOrDefault(e => e.Id == sessionContext.CurrentWellnessCheckIn!.Id);

                checkInDeleted = await _wellnessController.DeleteCheckInAsync(sessionContext!.CurrentWellnessCheckIn!.Id);

                if (checkInDeleted)
                {
                    sessionContext.CurrentWellnessCheckIn = null;

                    if (checkInSummary != null)
                    {
                        sessionContext.CurrentDayCard!.WellnessCheckInsSummaries!.Remove(checkInSummary!);
                        sessionContext.CurrentDayCard!.UpdateTotalValues();
                    }

                    Console.Clear();
                    Console.WriteLine(MenuText.Header.CheckInDeleted);
                    Thread.Sleep(1500);
                    sessionContext.DayCardMenuState = DayCardMenuState.Overview;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine(MenuText.Error.CheckInDeleteFailed);
                    Thread.Sleep(1500);
                }
            }
            else
            {
                sessionContext.WellnessCheckInMenuState = WellnessCheckInMenuState.CheckInDetails;
            }

            return sessionContext;
        }
    }


}
