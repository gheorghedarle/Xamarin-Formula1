using Formula1.Core;
using Formula1.Services.Ergast;
using Formula1.ViewModels;
using Formula1.ViewModels.TabViews;
using Formula1.Views;
using Formula1.Views.TabViews;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: ExportFont("FontAwesome-Regular.ttf", Alias = "FontAwesome_Regular")]
[assembly: ExportFont("FontAwesome-Solid.ttf", Alias = "FontAwesome_Solid")]

[assembly: ExportFont("Exo-Black.ttf", Alias = "Exo_Black")]
[assembly: ExportFont("Exo-Bold.ttf", Alias = "Exo_Bold")]
[assembly: ExportFont("Exo-Medium.ttf", Alias = "Exo_Medium")]
[assembly: ExportFont("Exo-Regular.ttf", Alias = "Exo_Regular")]

namespace Formula1
{
    public partial class App : Application
    {
        public App() 
        {
            InitializeComponent();
            SetAppTheme();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void SetAppTheme()
        {
            var theme = Preferences.Get("theme", string.Empty);
            if (string.IsNullOrEmpty(theme) || theme == "light")
            {
                Application.Current.UserAppTheme = OSAppTheme.Light;
            }
            else
            {
                Application.Current.UserAppTheme = OSAppTheme.Dark;
            }
        }
    }
}
