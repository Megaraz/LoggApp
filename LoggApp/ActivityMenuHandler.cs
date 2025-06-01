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

namespace Presentation
{
    public class ActivityMenuHandler : MenuHandlerBase
    {
        private readonly ExerciseController _exerciseController;

        public ActivityMenuHandler(ExerciseController exerciseController)
        {
            _exerciseController = exerciseController;
        }

        public override async Task<TContext> HandleMenuState<TContext>(TContext sessionContext)
        {
            switch (sessionContext.ActivityMenuState)
            {
                case ActivityMenuState.ExerciseOverview:
                    sessionContext = ExerciseOverviewMenuHandler(sessionContext);
                    break;

                case ActivityMenuState.ShowAllExercises:
                    sessionContext = ExerciseShowAllMenuHandler(sessionContext);
                    break;

                case ActivityMenuState.ExerciseDetails:
                    sessionContext = ExerciseDetailsMenuHandler(sessionContext);
                    break;

                case ActivityMenuState.AddExercise:
                    sessionContext = await CreateExerciseAsync(sessionContext);
                    break;

                case ActivityMenuState.UpdateExercise:
                    sessionContext = await UpdateExerciseAsync(sessionContext);
                    break;

                case ActivityMenuState.DeleteExercise:
                    sessionContext = await DeleteExerciseAsync(sessionContext);
                    break;

                    // Future implementation for Sleep can be added here.
            }

            return sessionContext;
        }

        private TContext ExerciseOverviewMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);

            var exerciseMenuChoice = MenuNavigation.GetMenuValue(MenuText.NavOption.s_ExerciseOverviewMenu.ToList(), sessionContext);

            if (exerciseMenuChoice != null)
            {
                switch (exerciseMenuChoice)
                {
                    case MenuText.NavOption.ShowAllExercise:
                        sessionContext.ActivityMenuState = ActivityMenuState.ShowAllExercises;
                        break;

                    case MenuText.NavOption.AddExercise:
                        sessionContext.ActivityMenuState = ActivityMenuState.AddExercise;
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

        private static TContext ExerciseShowAllMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);

            // Assuming CurrentDayCard has an ExercisesSummary with ExerciseDetails list.
            if (sessionContext!.CurrentDayCard?.ExercisesSummaries != null &&
                sessionContext.CurrentDayCard.ExercisesSummaries.Count > 0)
            {
                sessionContext.MainHeader = "Exercises " + sessionContext.CurrentDayCard.Date + ":\n";

                var exerciseMenuChoice = MenuNavigation.GetMenuValue(sessionContext.CurrentDayCard!.ExercisesSummaries, sessionContext);

                if (exerciseMenuChoice != null)
                {
                    sessionContext.CurrentExercise = new ExerciseDetailed(exerciseMenuChoice);

                    sessionContext.ActivityMenuState = ActivityMenuState.ExerciseDetails;
                }
                else
                {
                    sessionContext.ActivityMenuState = ActivityMenuState.ExerciseOverview;
                }
            }
            else
            {
                sessionContext.ActivityMenuState = ActivityMenuState.ExerciseOverview;
            }
            return sessionContext;
        }

        private TContext ExerciseDetailsMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);
            // Display exercise details
            sessionContext.Footer = sessionContext.CurrentExercise?.ToString() ?? "No exercise details available.";

            var detailsChoice = MenuNavigation.GetMenuValue(MenuText.NavOption.s_ExerciseDetailsMenu.ToList(), sessionContext);

            if (detailsChoice is not null)
            {
                switch (detailsChoice)
                {
                    case MenuText.NavOption.UpdateExercise:
                        sessionContext.ActivityMenuState = ActivityMenuState.UpdateExercise;
                        break;
                    case MenuText.NavOption.DeleteExercise:
                        sessionContext.ActivityMenuState = ActivityMenuState.DeleteExercise;
                        break;
                    case MenuText.NavOption.Back:
                        sessionContext.ActivityMenuState = ActivityMenuState.ExerciseOverview;
                        break;
                }
            }
            else
            {
                sessionContext.ActivityMenuState = ActivityMenuState.ShowAllExercises;
            }
            return sessionContext;
        }

        private async Task<TContext> CreateExerciseAsync<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);

            // Obtain input details for the exercise
            var (timeOf, endTime, duration) = ConsoleInput.Input_ActivityTime();

            if (timeOf != null || endTime != null || duration != null)
            {
                var intensityChoice = MenuNavigation.GetMenuValue(MenuText.NavOption.s_ExercisePerceivedIntensity.ToList(), sessionContext);

                if (intensityChoice != null)
                {
                    var exerciseInputModel = new ExerciseInputModel
                    {
                        TimeOf = timeOf,
                        EndTime = endTime,
                        Duration = duration
                    };
                    exerciseInputModel.PerceivedIntensity = GetPercIntensityChoice(intensityChoice);

                    exerciseInputModel = ConsoleInput.Input_Exercise(exerciseInputModel);

                    sessionContext.CurrentExercise = await _exerciseController.AddExerciseToDayCardAsync(sessionContext.CurrentDayCard!.DayCardId, exerciseInputModel);

                    sessionContext.CurrentDayCard!.ExercisesSummaries ??= new List<ExerciseSummary>();

                    sessionContext.CurrentDayCard!.ExercisesSummaries!
                        .Add(new ExerciseSummary(sessionContext.CurrentExercise!));

                    sessionContext.CurrentDayCard.UpdateTotalValues();

                }
                else
                {
                    sessionContext.ActivityMenuState = ActivityMenuState.ExerciseDetails;
                }
            }

            sessionContext.ActivityMenuState = ActivityMenuState.ExerciseOverview;
            return sessionContext;
        }

        private static PerceivedIntensity? GetPercIntensityChoice(string intensityChoice)
        {
            PerceivedIntensity? perceivedIntensity = null;
            switch (intensityChoice)
            {
                case MenuText.NavOption.ExerciseModerate:
                    perceivedIntensity = PerceivedIntensity.Moderate;
                    break;

                case MenuText.NavOption.ExerciseLight:
                    perceivedIntensity = PerceivedIntensity.Light;
                    break;

                case MenuText.NavOption.ExerciseIntense:
                    perceivedIntensity = PerceivedIntensity.Intense;
                    break;

                case MenuText.NavOption.ExerciseRelaxed:
                    perceivedIntensity = PerceivedIntensity.Relaxed;
                    break;

                case MenuText.NavOption.ExerciseMaxEffort:
                    perceivedIntensity = PerceivedIntensity.MaxEffort;
                    break;
            }

            return perceivedIntensity;
        }

        private async Task<TContext> UpdateExerciseAsync<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);

            var (timeOf, endTime, duration) = ConsoleInput.Input_ActivityTime();

            if (timeOf != null || endTime != null || duration != null)
            {
                var intensityChoice = MenuNavigation.GetMenuValue(MenuText.NavOption.s_ExercisePerceivedIntensity.ToList(), sessionContext);

                if (intensityChoice != null)
                {
                    var updateInputModel = new ExerciseInputModel
                    {
                        TimeOf = timeOf,
                        EndTime = endTime,
                        Duration = duration
                    };

                    updateInputModel.PerceivedIntensity = GetPercIntensityChoice(intensityChoice);

                    updateInputModel = ConsoleInput.Input_Exercise(updateInputModel);

                    sessionContext.CurrentExercise = await _exerciseController.UpdateExerciseAsync(sessionContext.CurrentExercise!.Id, updateInputModel);

                    Console.Clear();
                    Console.WriteLine("EXERCISE UPDATED");
                    Thread.Sleep(1500);

                    var exerciseSummary = sessionContext.CurrentDayCard!.ExercisesSummaries!
                        .FirstOrDefault(x => x.Id == sessionContext.CurrentExercise!.Id);

                    if (exerciseSummary != null)
                    {
                        exerciseSummary.Id = sessionContext.CurrentExercise!.Id;
                        exerciseSummary.DayCardId = sessionContext.CurrentDayCard.DayCardId;
                        exerciseSummary.Duration = sessionContext.CurrentExercise!.Duration;
                        exerciseSummary.ActiveKcalBurned = sessionContext.CurrentExercise.ActiveKcalBurned;
                        exerciseSummary.PerceivedIntensity = sessionContext.CurrentExercise.PerceivedIntensity;

                    }

                    sessionContext.CurrentDayCard.UpdateTotalValues();
                }
                else
                {
                    sessionContext.ActivityMenuState = ActivityMenuState.ExerciseDetails;
                }
            }

            sessionContext.ActivityMenuState = ActivityMenuState.ExerciseOverview;
            return sessionContext;
        }

        private async Task<TContext> DeleteExerciseAsync<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            // Confirm deletion with the user
            bool confirmDelete = ConsoleInput.Input_Confirmation(MenuText.Prompt.DeleteExerciseConfirmation);

            bool exerciseDeleted = false;

            if (confirmDelete)
            {
                //sessionContext?.CurrentDayCard?.ExercisesSummaries?.Remove(sessionContext.CurrentExercise!);

                var exerciseSummary = sessionContext!.CurrentDayCard!.ExercisesSummaries!.FirstOrDefault(e => e.Id == sessionContext.CurrentExercise!.Id);

                exerciseDeleted = await _exerciseController.DeleteExerciseAsync(sessionContext!.CurrentExercise!.Id);

                if (exerciseDeleted)
                {
                    sessionContext.CurrentExercise = null;

                    if (exerciseSummary != null)
                    {
                        sessionContext.CurrentDayCard!.ExercisesSummaries!.Remove(exerciseSummary!);
                        sessionContext.CurrentDayCard!.UpdateTotalValues();
                    }

                    Console.Clear();
                    Console.WriteLine(MenuText.Header.ExerciseDeleted);
                    Thread.Sleep(1500);
                    sessionContext.DayCardMenuState = DayCardMenuState.Overview;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine(MenuText.Error.ExerciseDeleteFailed);
                    Thread.Sleep(1500);
                }
            }
            else
            {
                sessionContext.ActivityMenuState = ActivityMenuState.ExerciseDetails;
            }

            return sessionContext;
        }
    }
}
