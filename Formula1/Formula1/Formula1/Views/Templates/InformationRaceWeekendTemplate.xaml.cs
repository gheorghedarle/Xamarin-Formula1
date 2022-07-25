using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Formula1.Views.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InformationRaceWeekendTemplate : StackLayout
    {
        public InformationRaceWeekendTemplate()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty EventNameProperty = BindableProperty.Create(nameof(EventName), typeof(string), typeof(InformationTemplate), string.Empty);
        public static readonly BindableProperty DateProperty = BindableProperty.Create(nameof(Date), typeof(DateTime), typeof(InformationTemplate), DateTime.Now);
        public static readonly BindableProperty TimeProperty = BindableProperty.Create(nameof(Time), typeof(DateTime), typeof(InformationTemplate), DateTime.Now);

        public string EventName
        {
            get => (string)GetValue(EventNameProperty);
            set => SetValue(EventNameProperty, value);
        }

        public DateTime Date
        {
            get => (DateTime)GetValue(DateProperty);
            set => SetValue(DateProperty, value);
        }

        public DateTime Time
        {
            get => (DateTime)GetValue(TimeProperty);
            set => SetValue(TimeProperty, value);
        }
    }
}