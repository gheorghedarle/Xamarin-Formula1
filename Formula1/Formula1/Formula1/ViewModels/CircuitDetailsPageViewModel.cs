using Formula1.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web;
using Xamarin.Forms;

namespace Formula1.ViewModels
{
    public class CircuitDetailsPageViewModel : BaseViewModel, IQueryAttributable
    {
        #region Properties

        public ScheduleModel Circuit { get; set; }

        #endregion

        #region Commands

        public Command BackCommand { get; set; }

        #endregion

        #region Constructors

        public CircuitDetailsPageViewModel()
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
            string circuitString = HttpUtility.UrlDecode(query["circuit"]);
            var circuit = JsonConvert.DeserializeObject<ScheduleModel>(circuitString);
            Circuit = circuit;
        }

        #endregion
    }
}
