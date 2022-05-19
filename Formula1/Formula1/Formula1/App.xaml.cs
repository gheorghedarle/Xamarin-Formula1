using Formula1.ViewModels;
using Formula1.ViewModels.TabViews;
using Formula1.Views;
using Formula1.Views.TabViews;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Mvvm;
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
    public partial class App : PrismApplication
    {
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer)
        { }

        public new static App Current => Application.Current as App;

        protected override async void OnInitialized()
        {
            InitializeComponent();
            SetAppTheme();

            VersionTracking.Track();
            if (VersionTracking.IsFirstLaunchEver)
            {
                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(WelcomePage)}");
            }
            else
            {
                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(TabPage)}");
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>("NavigationPage");
            containerRegistry.RegisterForNavigation<WelcomePage, WelcomePageViewModel>("WelcomePage");
            containerRegistry.RegisterForNavigation<TabPage, TabPageViewModel>("TabPage");
            containerRegistry.RegisterForNavigation<DriverDetailsPage, DriverDetailsPageViewModel>("DriverDetailsPage");

            ViewModelLocationProvider.Register<HomeView, HomeViewModel>();
            ViewModelLocationProvider.Register<ScheduleView, ScheduleViewModel>();
            ViewModelLocationProvider.Register<DriversView, DriversViewModel>();
            ViewModelLocationProvider.Register<TeamsView, TeamsViewModel>();
            ViewModelLocationProvider.Register<HistoryView, HistoryViewModel>();
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
