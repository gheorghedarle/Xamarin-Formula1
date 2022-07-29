using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Formula1.ViewModels
{
    public class WelcomePageViewModel : BaseViewModel
    {
        #region Commands

        public Command LetsStartCommand { get; set; }

        #endregion

        #region Properties

        public Task Init { get; }

        #endregion

        #region Constructors

        public WelcomePageViewModel()
        {
            LetsStartCommand = new Command(LetsStartCommandHandler);

            Init = Initialize();
        }

        #endregion

        #region Command Handlers

        private async void LetsStartCommandHandler()
        {
            await Shell.Current.GoToAsync("///main");
        }

        #endregion

        #region Private Functionality

        private async Task Initialize()
        {
            VersionTracking.Track();
            if (VersionTracking.IsFirstLaunchEver)
            {
                await Shell.Current.GoToAsync("///welcome");
            }
            else
            {
                await Shell.Current.GoToAsync("///main");
            }
        }

        #endregion
    }
}
