using Formula1.Models;
using Formula1.Services.Ergast;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace Formula1.ViewModels.TabViews
{
    public class DriversViewModel : BaseViewModel
    {
        #region Fields

        private readonly IErgastService _ergastService;

        #endregion

        #region Properties

        public Task Init { get; }

        public ObservableCollection<DriverStadingsModel> DriversList { get; set; }

        #endregion

        #region Commands

        public Command DriverDetailsCommand { get; set; }

        #endregion

        #region Constructors

        public DriversViewModel(
            IErgastService ergastService)
        {
            Title = "Driver";
            _ergastService = ergastService;

            DriverDetailsCommand = new Command<DriverStadingsModel>(DriverDetailsCommandHandler);

            Init = Initialize();
        }

        #endregion

        #region Command Handlers

        private async void DriverDetailsCommandHandler(DriverStadingsModel driver)
        {
            await Shell.Current.GoToAsync($"/details?driver={JsonConvert.SerializeObject(driver)}");
        }

        #endregion

        #region Private Functionality

        private async Task Initialize()
        {
            MainState = LayoutState.Loading;
            var res = await _ergastService.GetDriverStadings("current");
            DriversList = new ObservableCollection<DriverStadingsModel>(res);
            MainState = LayoutState.None;
        }

        #endregion
    }
}
