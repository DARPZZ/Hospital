namespace Hospital.Views;

public partial class QrPage : ContentPage
{
	public QrPage(QrViewmodel viewmodel)
	{
		InitializeComponent();
        BindingContext = viewmodel;
	}
    private void cameraView_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
    {
        var barcode = e.Results.FirstOrDefault();

        if (barcode != null)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                text.Text = barcode.Value;
                
            });
        }

    }
}