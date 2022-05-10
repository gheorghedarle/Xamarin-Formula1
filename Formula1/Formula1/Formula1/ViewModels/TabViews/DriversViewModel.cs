using Formula1.Views;
using Prism.Navigation;
using Xamarin.Forms;

namespace Formula1.ViewModels.TabViews
{
    public class DriversViewModel : BaseViewModel
    {
        #region Commands

        public Command DriverDetailsCommand { get; set; }

        #endregion

        #region Constructors

        public DriversViewModel(INavigationService navigationService) : base(navigationService)
        {
            DriverDetailsCommand = new Command(DriverDetailsCommandHandler);
        }

        #endregion

        #region Command Handlers

        private async void DriverDetailsCommandHandler()
        {
            await _navigationService.NavigateAsync(nameof(DriverDetailsPage));
        }

        #endregion
    }
}
