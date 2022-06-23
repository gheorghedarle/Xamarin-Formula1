using Formula1.Helpers;
using Formula1.Views.Popups;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Formula1.ViewModels.Popups
{
    public class SeasonPopupPageViewModel
    {
        #region Properties

        public Task Init { get; }

        public ObservableCollection<int> SeasonsList { get; set; }

        #endregion

        #region Commands
        public Command SelectSeasonCommand { get; set; }

        #endregion

        #region Constructors

        public SeasonPopupPageViewModel()
        {
            SelectSeasonCommand = new Command<int>(SelectSeasonCommandHandler);

            Init = Initialize();
        }

        #endregion

        #region Command Handlers

        private void SelectSeasonCommandHandler(int season)
        {
            MessagingCenter.Send<SeasonPopupPageViewModel, string>(this, "Dismiss", season.ToString());
        }

        #endregion

        #region Private Functionality

        private Task Initialize()
        {
            SeasonsList = new ObservableCollection<int>(Constants.GetSeasonsList());
            return Task.CompletedTask;
        }

        #endregion
    }
}
