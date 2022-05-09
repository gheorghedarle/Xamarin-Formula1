using Formula1.ViewModels;
using Formula1.Views;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: ExportFont("FontAwesome-Regular.ttf", Alias = "FontAwesome_Regular")]
[assembly: ExportFont("FontAwesome-Solid.ttf", Alias = "FontAwesome_Solid")]

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

            await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(TabPage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>("NavigationPage");
            containerRegistry.RegisterForNavigation<WelcomePage, WelcomePageViewModel>("WelcomePage");
            containerRegistry.RegisterForNavigation<TabPage, TabPageViewModel>("TabPage");
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
