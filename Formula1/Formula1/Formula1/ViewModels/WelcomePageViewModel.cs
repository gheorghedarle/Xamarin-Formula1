using Formula1.Views;
using Prism.Navigation;
using Xamarin.Forms;

namespace Formula1.ViewModels
{
    public class WelcomePageViewModel : BaseViewModel
    {
        #region Commands

        public Command LetsStartCommand { get; set; }

        #endregion

        #region Constructors

        public WelcomePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            LetsStartCommand = new Command(LetsStartCommandHandler);
        }

        #endregion

        #region Command Handlers

        private async void LetsStartCommandHandler()
        {
            await _navigationService.NavigateAsync(nameof(TabPage));
        }

        #endregion
    }
}
