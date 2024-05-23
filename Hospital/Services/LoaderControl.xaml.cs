namespace Hospital.Services
{
    public partial class LoaderControl : StackLayout
    {
        public LoaderControl()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty IndicatorColorProperty = BindableProperty.Create(
            propertyName: nameof(IndicatorColor),
            returnType: typeof(Color),
            defaultValue: Colors.LightBlue,
            declaringType: typeof(LoaderControl),
            defaultBindingMode: BindingMode.OneWay);

        public Color IndicatorColor
        {
            get => (Color)GetValue(IndicatorColorProperty);
            set => SetValue(IndicatorColorProperty, value);
        }

        public static readonly BindableProperty LoadingTextProperty = BindableProperty.Create(
            propertyName: nameof(LoadingText),
            returnType: typeof(string),
            defaultValue: "Please wait",
            declaringType: typeof(LoaderControl),
            defaultBindingMode: BindingMode.OneWay);

        public string LoadingText
        {
            get => (string)GetValue(LoadingTextProperty);
            set => SetValue(LoadingTextProperty, value);
        }
    }
}
