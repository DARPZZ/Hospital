namespace Hospital.ViewModels;

public partial class LoginnViewModel : BaseViewModel
{
    [ObservableProperty]
    private string _username;
    [ObservableProperty]
    private string _password;
}
