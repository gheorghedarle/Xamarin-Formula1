using Formula1.Models;
using Xamarin.Forms;

namespace Formula1.ViewModels
{
    public class DriverDetailsPageViewModel: BaseViewModel
    {
        #region Properties

        public DriverStadingsModel DriverStading { get; set; }

        #endregion

        #region Commands

        public Command BackCommand { get; set; }

        #endregion

        #region Constructors

        public DriverDetailsPageViewModel()
        {
            BackCommand = new Command(BackCommandHandler);
        }

        #endregion

        #region Command Handlers

        private async void BackCommandHandler()
        {
            //await _navigationService.GoBackAsync();
            await Shell.Current.Navigation.PopAsync();
        }

        #endregion

        #region Navigation Handlers

        //public override void OnNavigatedTo(INavigationParameters parameters)
        //{
        //    var driver = parameters.GetValue<DriverStadingsModel>("driver");
        //    if(driver != null)
        //    {
        //        DriverStading = driver;
        //    }
        //}

        #endregion
    }
}
