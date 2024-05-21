using ZXing.Net.Maui;
using ZXing;

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



    protected override void OnAppearing()
    {
        base.OnAppearing();
        InitializeCamera();
    }

    private void InitializeCamera()
    {
        cameraView.IsVisible = true;

        cameraView.CameraLocation = CameraLocation.Front;
        cameraView.CameraLocation = CameraLocation.Rear;
        cameraView.IsEnabled = true;
        cameraView.IsDetecting = true;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        DeinitializeCamera();
    }

    private void DeinitializeCamera()
    {
        cameraView.IsEnabled = false;
        cameraView.IsDetecting = false;
        cameraView.IsVisible = false;
    }

}