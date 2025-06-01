using AppLogic;
using AppLogic.Controllers;
using AppLogic.Repositories;
using AppLogic.Repositories.Interfaces;
using AppLogic.Services;
using AppLogic.Services.Interfaces;
using Presentation.MenuState_Enums;

namespace Presentation
{
    public class App
    {
        // Presentation, Context and Menu Handlers
        private LoggAppContext _dbContext;
        private SessionContext _sessionContext;
        private MenuRouterService _menuRouterService;
        private MainMenuHandler _mainMenuHandler;
        private UserMenuHandler _userMenuHandler;
        private DayCardMenuHandler _dayCardMenuHandler;
        private IntakeMenuHandler _intakeMenuHandler;
        private ActivityMenuHandler _activityMenuHandler;
        private SleepMenuHandler _sleepMenuHandler;
        private OpenAiResponseClient _openAiResponseClient;

        // Repos
        private IUserRepo _userRepo;
        private IDayCardRepo _dayCardRepo;
        private ICaffeineDrinkRepo _caffeineDrinkRepo;
        private IWeatherRepo _weatherRepo;
        private IAirQualityRepo _airQualityRepo;
        private ISleepRepo _sleepRepo;
        private IActivityRepo _activityRepo;
        private IExerciseRepo _exerciseRepo;
        private ISupplementRepo _supplementRepo;
        private IWellnessCheckInRepo _wellnessCheckInRepo;


        // Services
        private IUserService _userService;
        private IWeatherService _weatherService;
        private IAirQualityService _airQualityService;
        private IDayCardService _dayCardService;
        private ICaffeineDrinkService _caffeineDrinkService;
        private ISleepService _sleepService;
        private IActivityService _activityService;
        private IExerciseService _exerciseService;
        private ISupplementService _supplementService;
        private IWellnessCheckInService _wellnessCheckInService;

        // Controllers
        private UserController _userController;
        private WeatherController _weatherController;
        private AirQualityController _airQualityController;
        private DayCardController _dayCardController;
        private CaffeineDrinkController _caffeineDrinkController;
        private SleepController _sleepController;
        private ActivityController _activityController;
        private ExerciseController _exerciseController;
        private SupplementController _supplementController;
        private WellnessCheckInController _wellnessCheckInController;


        public App(LoggAppContext dbContext)
        {
            _dbContext = dbContext;
            
        }
        
        // Initialization Methods Below, I have them separate(instead of constructor) for clarity and intention.
        public void InitRepos()
        {
            _userRepo = new UserRepo(_dbContext);
            _dayCardRepo = new DayCardRepo(_dbContext);
            _caffeineDrinkRepo = new CaffeineDrinkRepo(_dbContext);
            _weatherRepo = new WeatherRepo(_dbContext);
            _airQualityRepo = new AirQualityRepo(_dbContext);
            _openAiResponseClient = new OpenAiResponseClient();

            _sleepRepo = new SleepRepo(_dbContext);
            _activityRepo = new ActivityRepo(_dbContext);
            _exerciseRepo = new ExerciseRepo(_dbContext);
            _supplementRepo = new SupplementRepo(_dbContext);
            _wellnessCheckInRepo = new WellnessCheckInRepo(_dbContext);
        }

        public void InitServices()
        {
            _userService = new UserService(_userRepo);
            _weatherService = new WeatherService(_weatherRepo);
            _airQualityService = new AirQualityService(_airQualityRepo);
            _caffeineDrinkService = new CaffeineDrinkService(_caffeineDrinkRepo);

            _sleepService = new SleepService(_sleepRepo);
            _activityService = new ActivityService(_activityRepo);
            _exerciseService = new ExerciseService(_exerciseRepo);
            _supplementService = new SupplementService(_supplementRepo);
            _wellnessCheckInService = new WellnessCheckInService(_wellnessCheckInRepo);

            _dayCardService = new DayCardService(_dayCardRepo, _weatherService, _airQualityService, _caffeineDrinkService, _exerciseService, _openAiResponseClient, _sleepService);
        }

        public void InitControllers()
        {
            _userController = new UserController(_userService);
            _weatherController = new WeatherController(_weatherService);
            _airQualityController = new AirQualityController(_airQualityService);
            _dayCardController = new DayCardController(_dayCardService);
            _caffeineDrinkController = new CaffeineDrinkController(_caffeineDrinkService);

            _sleepController = new SleepController(_sleepService);
            _activityController = new ActivityController(_activityService);
            _exerciseController = new ExerciseController(_exerciseService);
            _supplementController = new SupplementController(_supplementService);
            _wellnessCheckInController = new WellnessCheckInController(_wellnessCheckInService);

        }

        public void InitPresentation()
        {
            _mainMenuHandler = new MainMenuHandler(_userController, _weatherController);
            _userMenuHandler = new UserMenuHandler(_dayCardController, _userController, _weatherController);
            _dayCardMenuHandler = new DayCardMenuHandler(_dayCardController);
            _intakeMenuHandler = new IntakeMenuHandler(_caffeineDrinkController);
            _activityMenuHandler = new ActivityMenuHandler(_exerciseController);
            _sleepMenuHandler = new SleepMenuHandler(_sleepController);

            _menuRouterService = new MenuRouterService(_mainMenuHandler, _userMenuHandler, _dayCardMenuHandler, _intakeMenuHandler, _activityMenuHandler, _sleepMenuHandler);


            _sessionContext = new SessionContext();
            _sessionContext.MainMenuState = MainMenuState.Main;
            _sessionContext.UserMenuState = UserMenuState.None;
            _sessionContext.DayCardMenuState = DayCardMenuState.None;
            _sessionContext.IntakeMenuState = IntakeMenuState.None;
            _sessionContext.ActivityMenuState = ActivityMenuState.None;
            _sessionContext.SleepMenuState = SleepMenuState.None;

        }

        // Main run loop method.
        public async Task Run()
        {
            
            do
            {
                _sessionContext = await _menuRouterService.MenuRouter(_sessionContext);



            } while (_sessionContext.MainMenuState != MainMenuState.Exit);


        }
    }
}
