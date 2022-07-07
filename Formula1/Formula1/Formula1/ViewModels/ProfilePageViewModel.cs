using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Formula1.ViewModels
{
    public class ProfilePageViewModel
    {
        #region Commands

        public ICommand BackCommand { get; set; }
        public ICommand DarkModeToggleCommand { get; set; }

        #endregion

        #region Properties

        public Task Init { get; }
        public bool IsDarkMode { get; set; }

        #endregion

        #region Constructors

        public ProfilePageViewModel()
        {
            BackCommand = new Command(BackCommandHandler);
            DarkModeToggleCommand = new Command(DarkModeToggleCommandHandler);

            Init = Initialize();
        }

        public Task Initialize()
        {
            IsDarkMode = Application.Current.UserAppTheme.Equals(OSAppTheme.Dark);
            return Task.CompletedTask;
        }

        #endregion

        #region Command Handlers

        private async void BackCommandHandler()
        {
            await Shell.Current.Navigation.PopAsync();
        }

        private void DarkModeToggleCommandHandler()
        {
            if (IsDarkMode)
            {
                Application.Current.UserAppTheme = OSAppTheme.Dark;
                Preferences.Set("theme", "dark");
            }
            else
            {
                Application.Current.UserAppTheme = OSAppTheme.Light;
                Preferences.Set("theme", "light");
            }
        }

        #endregion
    }
}
