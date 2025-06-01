using AppLogic;
using AppLogic.Controllers;
using AppLogic.Models.InputModels;
using AppLogic.Repositories;
using AppLogic.Repositories.Interfaces;
using AppLogic.Services;
using AppLogic.Services.Interfaces;
using Presentation.MenuHandlers;
using Presentation.MenuState_Enums;

namespace Presentation
{
    /// <summary>
    /// Main application class which initializes and runs the LoggApp application.
    /// </summary>
    public class App
    {
        #region FIELDS
        // Presentation, Context and Menu Handlers
        private LoggAppContext _dbContext;
        private SessionContext _sessionContext;
        private MenuRouterService _menuRouterService;
        private MainMenuHandler _mainMenuHandler;
        private UserMenuHandler _userMenuHandler;
        private DayCardMenuHandler _dayCardMenuHandler;
        private IntakeMenuHandler _intakeMenuHandler;
        private ExerciseMenuHandler _activityMenuHandler;
        private SleepMenuHandler _sleepMenuHandler;
        private WellnessMenuHandler _wellnessMenuHandler;
        private OpenAiResponseClient _openAiResponseClient;

        // Repos
        private IUserRepo _userRepo;
        private IDayCardRepo _dayCardRepo;
        private ICaffeineDrinkRepo _caffeineDrinkRepo;
        private IWeatherRepo _weatherRepo;
        private IAirQualityRepo _airQualityRepo;
        private ISleepRepo _sleepRepo;
        private IExerciseRepo _exerciseRepo;
        private IWellnessCheckInRepo _wellnessCheckInRepo;


        // Services
        private IUserService _userService;
        private IWeatherService _weatherService;
        private IAirQualityService _airQualityService;
        private IDayCardService _dayCardService;
        private ICaffeineDrinkService _caffeineDrinkService;
        private ISleepService _sleepService;
        private IExerciseService _exerciseService;
        private IWellnessCheckInService _wellnessCheckInService;

        // Controllers
        private UserController _userController;
        private WeatherController _weatherController;
        private DayCardController _dayCardController;
        private CaffeineDrinkController _caffeineDrinkController;
        private SleepController _sleepController;
        private ExerciseController _exerciseController;
        private WellnessCheckInController _wellnessCheckInController;
        #endregion

        public App(LoggAppContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region INITIALIZATION METHODS
        /// <summary>
        /// Initializes the repositories by creating instances of each repository with the database context.
        /// </summary>
        public void InitRepos()
        {
            _userRepo = new UserRepo(_dbContext);
            _dayCardRepo = new DayCardRepo(_dbContext);
            _caffeineDrinkRepo = new CaffeineDrinkRepo(_dbContext);
            _weatherRepo = new WeatherRepo(_dbContext);
            _airQualityRepo = new AirQualityRepo(_dbContext);
            _openAiResponseClient = new OpenAiResponseClient();

            _sleepRepo = new SleepRepo(_dbContext);
            _exerciseRepo = new ExerciseRepo(_dbContext);
            _wellnessCheckInRepo = new WellnessCheckInRepo(_dbContext);
        }
        /// <summary>
        /// Initializes the services by creating instances of each service with their respective repositories.
        /// </summary>
        public void InitServices()
        {
            _userService = new UserService(_userRepo);
            _weatherService = new WeatherService(_weatherRepo);
            _airQualityService = new AirQualityService(_airQualityRepo);
            _caffeineDrinkService = new CaffeineDrinkService(_caffeineDrinkRepo);

            _sleepService = new SleepService(_sleepRepo);
            _exerciseService = new ExerciseService(_exerciseRepo);
            _wellnessCheckInService = new WellnessCheckInService(_wellnessCheckInRepo);

            _dayCardService = new DayCardService(_dayCardRepo, _weatherService, _airQualityService, _openAiResponseClient);
        }
        /// <summary>
        /// Initializes the controllers by creating instances of each controller with their respective services.
        /// </summary>
        public void InitControllers()
        {
            _userController = new UserController(_userService);
            _weatherController = new WeatherController(_weatherService);
            _dayCardController = new DayCardController(_dayCardService);
            _caffeineDrinkController = new CaffeineDrinkController(_caffeineDrinkService);

            _sleepController = new SleepController(_sleepService);
            _exerciseController = new ExerciseController(_exerciseService);
            _wellnessCheckInController = new WellnessCheckInController(_wellnessCheckInService);

        }

        /// <summary>
        /// Initializes the presentation layer by setting up the menu handlers and session context.
        /// </summary>
        public void InitPresentation()
        {
            _mainMenuHandler = new MainMenuHandler(_userController, _weatherController);
            _userMenuHandler = new UserMenuHandler(_dayCardController, _userController, _weatherController);
            _dayCardMenuHandler = new DayCardMenuHandler(_dayCardController);
            _intakeMenuHandler = new IntakeMenuHandler(_caffeineDrinkController);
            _activityMenuHandler = new ExerciseMenuHandler(_exerciseController);
            _sleepMenuHandler = new SleepMenuHandler(_sleepController);
            _wellnessMenuHandler = new WellnessMenuHandler(_wellnessCheckInController);

            _menuRouterService = new MenuRouterService(_mainMenuHandler, _userMenuHandler, _dayCardMenuHandler, _intakeMenuHandler, _activityMenuHandler, _sleepMenuHandler, _wellnessMenuHandler);


            _sessionContext = new SessionContext();
            _sessionContext.MainMenuState = MainMenuState.Main;
            _sessionContext.UserMenuState = UserMenuState.None;
            _sessionContext.DayCardMenuState = DayCardMenuState.None;
            _sessionContext.IntakeMenuState = IntakeMenuState.None;
            _sessionContext.ActivityMenuState = ActivityMenuState.None;
            _sessionContext.SleepMenuState = SleepMenuState.None;

        }
        #endregion

        // Main run loop method.
        public async Task Run()
        {
            // Main loop for the application, it will keep running until the user chooses to exit.
            do
            {
                _sessionContext = await _menuRouterService.MenuRouter(_sessionContext);

            } while (_sessionContext.MainMenuState != MainMenuState.Exit);


        }
    }
}
