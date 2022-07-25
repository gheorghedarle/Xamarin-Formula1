using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Formula1.Views.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InformationDateOfBirthTemplate : StackLayout
    {
        public InformationDateOfBirthTemplate()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty LabelProperty = BindableProperty.Create(nameof(Label), typeof(string), typeof(InformationTemplate), string.Empty);
        public static readonly BindableProperty DateOfBirthProperty = BindableProperty.Create(nameof(DateOfBirth), typeof(string), typeof(InformationTemplate), string.Empty);
        public static readonly BindableProperty AgeProperty = BindableProperty.Create(nameof(Age), typeof(string), typeof(InformationTemplate), string.Empty);

        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        public string DateOfBirth
        {
            get => (string)GetValue(DateOfBirthProperty);
            set => SetValue(DateOfBirthProperty, value);
        }

        public string Age
        {
            get => (string)GetValue(AgeProperty);
            set => SetValue(AgeProperty, value);
        }
    }
}