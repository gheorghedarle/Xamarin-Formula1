using Xamarin.Forms;

namespace Formula1.ViewModels.TabViews
{
    public class HomeViewModel: BaseViewModel
    {
        #region Commands

        public Command ProfileCommand { get; set; }

        #endregion

        #region Constructors

        public HomeViewModel()
        {
            Title = "Home";

            ProfileCommand = new Command(ProfileCommandHandler);
        }

        #endregion


        #region Command Handlers

        private async void ProfileCommandHandler()
        {
            await Shell.Current.GoToAsync($"profile");
        }

        #endregion
    }
}
