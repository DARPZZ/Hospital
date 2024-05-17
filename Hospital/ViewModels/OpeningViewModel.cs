namespace Hospital.ViewModels;

public partial class OpeningViewModel : BaseViewModel
{
    [ObservableProperty]
    public string scannedText;
    [RelayCommand]
    public void OnPrintTextClicked()
    {

        Debug.WriteLine(ScannedText);
    }
    
}
