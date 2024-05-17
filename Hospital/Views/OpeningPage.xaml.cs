namespace Hospital.Views;

public partial class OpeningPage : ContentPage
{
	public OpeningPage(OpeningViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    private void cameraView_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
    {
        var barcode = e.Results.FirstOrDefault();
        
        if (barcode != null)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                text.Text = barcode.Value;
                Debug.WriteLine(barcode.Value + "VALUE");
            });
        }

    }
}
