using Formula1.Models;
using Formula1.Services.Ergast;
using Formula1.Views;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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

        public ObservableCollection<DriverStadingsModel> DriversList { get; set; }
        public ObservableCollection<ConstructorStadingsModel> TeamsList { get; set; }

        #endregion

        #region Commands

        public Command DriverDetailsCommand { get; set; }

        #endregion

        #region Constructors

        public HistoryViewModel(IErgastService ergastService)
        {
            Title = "History";

            _ergastService = ergastService;

            DriverDetailsCommand = new Command<DriverStadingsModel>(DriverDetailsCommandHandler);

            Init = Initialize();
        }

        #endregion

        #region Command Handlers

        private async void DriverDetailsCommandHandler(DriverStadingsModel driver)
        {
            //var param = new NavigationParameters()
            //{
            //    { "driver", driver }
            //};
            //await _navigationService.NavigateAsync(nameof(DriverDetailsPage), param);
        }

        #endregion

        #region Private Functionality

        private async Task Initialize()
        {
            var resDrivers = await _ergastService.GetDriverStadings("1990");
            DriversList = new ObservableCollection<DriverStadingsModel>(resDrivers);
            var resTeams = await _ergastService.GetTeamStadings("1990");
            TeamsList = new ObservableCollection<ConstructorStadingsModel>(resTeams);
        }

        #endregion
    }
}
