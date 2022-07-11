using Formula1.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Formula1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
        }

        void RegisterRoutes()
        {
            Routing.RegisterRoute("profile", typeof(ProfilePage));
            Routing.RegisterRoute("schedule/details", typeof(CircuitDetailsPage));
            Routing.RegisterRoute("drivers/details", typeof(DriverDetailsPage));
        }
    }
}