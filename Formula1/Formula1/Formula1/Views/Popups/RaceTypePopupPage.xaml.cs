using Formula1.ViewModels.Popups;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Formula1.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RaceTypePopupPage
    {
        public RaceTypePopupPage()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<RaceTypePopupPageViewModel, string>(this.BindingContext, "Dismiss", (sender, args) =>
            {
                Dismiss(args);
            });
        }
    }
}