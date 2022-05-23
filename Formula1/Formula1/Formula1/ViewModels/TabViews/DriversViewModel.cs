using Formula1.Models;
using Formula1.Views;
using Prism.Navigation;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Formula1.ViewModels.TabViews
{
    public class DriversViewModel : BaseViewModel
    {
        #region Properties

        public ObservableCollection<DriverModel> DriversList { get; set; }

        #endregion

        #region Commands

        public Command DriverDetailsCommand { get; set; }

        #endregion

        #region Constructors

        public DriversViewModel(INavigationService navigationService) : base(navigationService)
        {
            DriverDetailsCommand = new Command(DriverDetailsCommandHandler);

            Title = "Drivers";

            DriversList = new ObservableCollection<DriverModel>()
            {
                new DriverModel()
                {
                    Position = 1,
                    Name = "Max VERSTAPPEN",
                    Team = "Red Bull Racing",
                    Points = 110,
                    Image = "https://www.formula1.com/content/dam/fom-website/drivers/M/MAXVER01_Max_Verstappen/maxver01.png"
                },
                new DriverModel()
                {
                    Position = 2,
                    Name = "Charles LECLERC",
                    Team = "Ferrari",
                    Points = 104,
                    Image = "https://www.formula1.com/content/dam/fom-website/drivers/C/CHALEC01_Charles_Leclerc/chalec01.png"
                },
                new DriverModel()
                {
                    Position = 3,
                    Name = "George RUSSEL",
                    Team = "Mercedes",
                    Points = 80,
                    Image = "https://www.formula1.com/content/dam/fom-website/drivers/G/GEORUS01_George_Russell/georus01.png"
                }
            };
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
