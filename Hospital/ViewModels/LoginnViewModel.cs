using Hospital.Models;
using Hospital.Services;

namespace Hospital.ViewModels;

public partial class LoginnViewModel : BaseViewModel
{
    [ObservableProperty]
    private string isChecked;
    private readonly UserService _userService;
    [ObservableProperty]
    private string username;
    [ObservableProperty]
    private string password;
    public LoginnViewModel()
    {
        LogInAutomatic();
        _userService = new UserService();
        
    }

    [RelayCommand]
    private void OnSignUpClicked()
    {
        Shell.Current.GoToAsync("///" + nameof(SignupPage));

    }
    [RelayCommand]
    private async Task OnSingInClicked()
    {
        var user = new User
        {
            email = Username,
            password = Password
        };
        bool isCorrectCredentials = await _userService.LogUserInAsync(user);
        if (isCorrectCredentials)
        {
            if (IsChecked != null)
            {
              await SecureStorage.SetAsync("email", Username);
              await SecureStorage.SetAsync("password", Password);
            }

            await Shell.Current.GoToAsync("///" + nameof(OpeningPage));
        }
        else
        {
            Debug.WriteLine("Not correct information");
        }

    }
    private async Task  LogInAutomatic()
    {
       string? email = await SecureStorage.GetAsync("email");
       string? password = await SecureStorage.GetAsync("password");
        if (email != null && password !=null)
        {
            var user = new User
            {
                email = email,
                password = password
            };
            bool isCorrectCredentials = await _userService.LogUserInAsync(user);
            if (isCorrectCredentials)
            {
               await Shell.Current.GoToAsync("///" + nameof(OpeningPage));
            }
        }

    }
}

