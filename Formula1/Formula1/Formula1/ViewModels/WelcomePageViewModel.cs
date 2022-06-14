using Formula1.Views;
using Xamarin.Forms;

namespace Formula1.ViewModels
{
    public class WelcomePageViewModel : BaseViewModel
    {
        #region Commands

        public Command LetsStartCommand { get; set; }

        #endregion

        #region Constructors

        public WelcomePageViewModel()
        {
            LetsStartCommand = new Command(LetsStartCommandHandler);
        }

        #endregion

        #region Command Handlers

        private async void LetsStartCommandHandler()
        {
            //await _navigationService.NavigateAsync(nameof(TabPage));
        }

        #endregion
    }
}
