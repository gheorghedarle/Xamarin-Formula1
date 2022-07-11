using Formula1.Models;
using Formula1.Services.Ergast;
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

        #endregion

        #region Constructors

        public HomeViewModel(
            IErgastService ergastService)
        {
            Title = "Home";
            _ergastService = ergastService;

            ProfileCommand = new Command(ProfileCommandHandler);

            Init = Initialize();
        }

        #endregion


        #region Command Handlers

        private async void ProfileCommandHandler()
        {
            await Shell.Current.GoToAsync($"profile");
        }

        #endregion

        #region Private Functionality

        private async Task Initialize()
        {
            await GetResults();
            await GetSchedule();
        }

        private async Task GetResults()
        {
            ResultsState = LayoutState.Loading;
            var res = await _ergastService.GetResults("current", "last", "results");
            LatestRace = $"Round {res.First().Round} - {res.First().Circuit.Location.Country} ({res.First().Circuit.CircuitName})";
            LatestResults = new ObservableCollection<RaceResultModel>(res.First().Results);
            ResultsState = LayoutState.None;
        }

        private async Task GetSchedule()
        {
            ScheduleState = LayoutState.Loading;
            var res = await _ergastService.GetSchedule("current");
            UpcomingRaceEventList = new ObservableCollection<RaceEventModel>(res.UpcomingRaceEvents);
            ScheduleState = LayoutState.None;
        }

        #endregion
    }
}
