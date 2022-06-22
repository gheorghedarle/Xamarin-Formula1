using Formula1.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
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

        private async void SelectSeasonCommandHandler(int season)
        {
            Dismiss
            await Shell.Current.Navigation.PopModalAsync();
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
