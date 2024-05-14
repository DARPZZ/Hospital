namespace Hospital.Views;

public partial class OpeningPage : ContentPage
{
	public OpeningPage(OpeningViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
