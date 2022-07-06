using Formula1.Models;
using Formula1.Services.Ergast;
using Formula1.Views.Popups;
using System;
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
        public ObservableCollection<RaceEventModel> ScheduleList { get; set; }

        public LayoutState DriversState { get; set; }
        public LayoutState TeamsState { get; set; }
        public LayoutState ScheduleState { get; set; }

        #endregion

        #region Commands

        public Command SelectSeasonCommand { get; set; }

        #endregion

        #region Constructors

        public HistoryViewModel(IErgastService ergastService)
        {
            Title = "History";

            _ergastService = ergastService;

            SelectSeasonCommand = new Command(SelectSeasonCommandHandler);

            Init = Initialize();
        }

        #endregion

        #region Command Handlers

        private async void SelectSeasonCommandHandler()
        {
            var season = await Shell.Current.Navigation.ShowPopupAsync(new SeasonPopupPage());
            if(season != null)
            {
                SelectedSeason = Convert.ToInt32(season);
                await GetDrivers(SelectedSeason.ToString());
                await GetTeams(SelectedSeason.ToString());
                await GetSchedule(SelectedSeason.ToString());
            }
        }

        #endregion

        #region Private Functionality

        private async Task Initialize()
        {
            SelectedSeason = 2021;
            await GetDrivers(SelectedSeason.ToString());
            await GetTeams(SelectedSeason.ToString());
            await GetSchedule(SelectedSeason.ToString());
        }

        private async Task GetDrivers(string season)
        {
            DriversState = LayoutState.Loading;
            var resDrivers = await _ergastService.GetDriverStadings(season);
            DriversList = new ObservableCollection<DriverStadingsModel>(resDrivers);
            DriversState = LayoutState.None;
        }

        private async Task GetTeams(string season)
        {
            TeamsState = LayoutState.Loading;
            var resTeams = await _ergastService.GetTeamStadings(SelectedSeason.ToString());
            TeamsList = new ObservableCollection<ConstructorStadingsModel>(resTeams);
            TeamsState = LayoutState.None;
        }

        private async Task GetSchedule(string season)
        {
            ScheduleState = LayoutState.Loading;
            var resSchedule = await _ergastService.GetSchedule(SelectedSeason.ToString());
            ScheduleList = new ObservableCollection<RaceEventModel>(resSchedule.PastRaceEvents);
            ScheduleState = LayoutState.None;
        }

        #endregion
    }
}
