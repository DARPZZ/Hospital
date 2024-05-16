namespace Hospital.ViewModels;

public partial class LoginnViewModel : BaseViewModel
{
    [ObservableProperty]
    private string _username;
    [ObservableProperty]
    private string _password;
    [RelayCommand]
    private void OnSignUpClicked()
    {
        Debug.WriteLine("SignUp button clicked");
        Shell.Current.GoToAsync("///" + nameof(SignupPage));

    }
    [RelayCommand]
    private void OnSingInClicked()
    {
        Debug.WriteLine("click");
        Shell.Current.GoToAsync("///" + nameof(OpeningPage));
    }
}

