using Formula1.Models;
using Formula1.Services.Ergast;
using Formula1.Views.Popups;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace Formula1.ViewModels.TabViews
{
    public class HistoryViewModel: BaseViewModel
    {
        #region Fields

        private readonly IErgastService _ergastService;

        #endregion

        #region Properties

        public Task Init { get; }

        public int SelectedSeason { get; set; }

        public ObservableCollection<DriverStadingsModel> DriversList { get; set; }
        public ObservableCollection<ConstructorStadingsModel> TeamsList { get; set; }

        public LayoutState DriversState { get; set; }
        public LayoutState TeamsState { get; set; }

        #endregion

        #region Commands

        public Command DriverDetailsCommand { get; set; }
        public Command SelectSeasonCommand { get; set; }

        #endregion

        #region Constructors

        public HistoryViewModel(IErgastService ergastService)
        {
            Title = "History";

            _ergastService = ergastService;

            DriverDetailsCommand = new Command<DriverStadingsModel>(DriverDetailsCommandHandler);
            SelectSeasonCommand = new Command(SelectSeasonCommandHandler);

            Init = Initialize();
        }

        #endregion

        #region Command Handlers

        private async void DriverDetailsCommandHandler(DriverStadingsModel driver)
        {
            await Shell.Current.GoToAsync($"driverdetails?driver={JsonConvert.SerializeObject(driver)}");
        }

        private async void SelectSeasonCommandHandler()
        {
            var res = await Shell.Current.Navigation.ShowPopupAsync(new SeasonPopupPage());
        }

        #endregion

        #region Private Functionality

        private async Task Initialize()
        {
            SelectedSeason = 2021;
            await GetDrivers();
            await GetTeams();
        }

        private async Task GetDrivers()
        {
            DriversState = LayoutState.Loading;
            var resDrivers = await _ergastService.GetDriverStadings(SelectedSeason.ToString());
            DriversList = new ObservableCollection<DriverStadingsModel>(resDrivers);
            DriversState = LayoutState.None;
        }

        private async Task GetTeams()
        {
            TeamsState = LayoutState.Loading;
            var resTeams = await _ergastService.GetTeamStadings(SelectedSeason.ToString());
            TeamsList = new ObservableCollection<ConstructorStadingsModel>(resTeams);
            TeamsState = LayoutState.None;
        }

        #endregion
    }
}
