using Formula1.ViewModels.Popups;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Formula1.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SeasonPopupPage
    {
        public SeasonPopupPage()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<SeasonPopupPageViewModel, string>(this.BindingContext, "Dismiss", (sender, args) =>
            {
                Dismiss(args);
            });
        }
    }
}