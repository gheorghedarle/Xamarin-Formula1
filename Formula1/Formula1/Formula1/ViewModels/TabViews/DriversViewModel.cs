using Formula1.Models;
using Formula1.Services.Ergast;
using Formula1.Views;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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

        public ObservableCollection<DriverModel> DriversList { get; set; }

        #endregion

        #region Commands

        public Command DriverDetailsCommand { get; set; }

        #endregion

        #region Constructors

        public DriversViewModel(
            INavigationService navigationService,
            IErgastService ergastService) : base(navigationService)
        {
            Title = "Drivers";

            _ergastService = ergastService;

            DriverDetailsCommand = new Command(DriverDetailsCommandHandler);

            Init = Initialize();
        }

        #endregion

        #region Command Handlers

        private async void DriverDetailsCommandHandler()
        {
            await _navigationService.NavigateAsync(nameof(DriverDetailsPage));
        }

        #endregion

        #region Private Functionality

        private async Task Initialize()
        {
            var res = await _ergastService.GetDriverStadings();
        }

        #endregion
    }
}
