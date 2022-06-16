using Formula1.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web;
using Xamarin.Forms;

namespace Formula1.ViewModels
{
    public class DriverDetailsPageViewModel: BaseViewModel, IQueryAttributable
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
            await Shell.Current.Navigation.PopAsync();
        }

        #endregion

        #region IQueryAttributable

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            string driverString = HttpUtility.UrlDecode(query["driver"]);
            var driver = JsonConvert.DeserializeObject<DriverStadingsModel>(driverString);
            DriverStading = driver;
        }

        #endregion
    }
}
