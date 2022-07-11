using Formula1.Models;
using Formula1.Services.Ergast;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace Formula1.ViewModels.TabViews
{
    public class HomeViewModel: BaseViewModel
    {
        #region Fields

        private readonly IErgastService _ergastService;

        private RaceEventModel _latestRace;

        #endregion

        #region Properties

        public Task Init { get; }

        public string LatestRace { get; set; }
        public ObservableCollection<RaceResultModel> LatestResults { get; set; }
        public ObservableCollection<RaceEventModel> UpcomingRaceEventList { get; set; }

        public LayoutState ResultsState { get; set; }
        public LayoutState ScheduleState { get; set; }

        #endregion

        #region Commands

        public Command ProfileCommand { get; set; }
        public Command SeeDriverCommand { get; set; }
        public Command SeeMoreResultsCommand { get; set; }
        public Command SeeEventCommand { get; set; }
        public Command SeeMoreScheduleCommand { get; set; }

        #endregion

        #region Constructors

        public HomeViewModel(
            IErgastService ergastService)
        {
            Title = "Home";
            _ergastService = ergastService;

            ProfileCommand = new Command(ProfileCommandHandler);
            SeeDriverCommand = new Command<RaceResultModel>(SeeDriverCommandHandler);
            SeeMoreResultsCommand = new Command(SeeMoreResultsCommandHandler);
            SeeEventCommand = new Command<RaceEventModel>(SeeEventCommandHandler);
            SeeMoreScheduleCommand = new Command(SeeMoreScheduleCommandHandler);

            Init = Initialize();
        }

        #endregion


        #region Command Handlers

        private async void ProfileCommandHandler()
        {
            await Shell.Current.GoToAsync($"profile");
        }

        private async void SeeDriverCommandHandler(RaceResultModel raceResult)
        {
            var driver = new DriverStadingsModel()
            {
                Driver = raceResult.Driver,
                Constructors = new List<ConstructorModel>() { raceResult.Constructor }
            };
            await Shell.Current.GoToAsync($"//main/drivers/details?driver={JsonConvert.SerializeObject(driver)}");
        }

        private async void SeeMoreResultsCommandHandler()
        {
            await Shell.Current.GoToAsync($"//main/schedule/details?circuit={JsonConvert.SerializeObject(_latestRace)}&selectedTab=1");
        }

        private async void SeeEventCommandHandler(RaceEventModel raceEvent)
        {
            await Shell.Current.GoToAsync($"//main/schedule/details?circuit={JsonConvert.SerializeObject(raceEvent)}");
        }

        private async void SeeMoreScheduleCommandHandler()
        {
            await Shell.Current.GoToAsync($"//main/schedule");
        }

        #endregion

        #region Private Functionality

        private async Task Initialize()
        {
            ResultsState = LayoutState.Loading;
            ScheduleState = LayoutState.Loading;
            await GetResults();
            await GetSchedule();
        }

        private async Task GetResults()
        {
            var res = await _ergastService.GetResults("current", "last", "results", "limit=10");
            _latestRace = res.First();
            LatestRace = $"Round {res.First().Round} - {res.First().Circuit.Location.Country} ({res.First().Circuit.CircuitName})";
            LatestResults = new ObservableCollection<RaceResultModel>(res.First().Results);
            LatestResults.Add(new RaceResultModel());
            ResultsState = LayoutState.None;
        }

        private async Task GetSchedule()
        {
            var res = await _ergastService.GetSchedule("current");
            _latestRace = res.PastRaceEvents.Last();
            UpcomingRaceEventList = new ObservableCollection<RaceEventModel>(res.UpcomingRaceEvents.Take(3));
            UpcomingRaceEventList.Add(new RaceEventModel());
            ScheduleState = LayoutState.None;
        }

        #endregion
    }
}
