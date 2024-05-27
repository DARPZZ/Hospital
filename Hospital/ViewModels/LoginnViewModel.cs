using System.Windows.Input;
using Hospital.Models;
using Hospital.Services;
using DotNetEnv;
namespace Hospital.ViewModels;

public partial class LoginnViewModel : BaseViewModel
{
    [ObservableProperty]
    private double isLoading;
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
        IsLoading = 0;
        
    }
    [RelayCommand]
    private void OnPasswordClicked()
    {

        Shell.Current.GoToAsync("///" + nameof(ForgotPasswordPage));

    }
    [RelayCommand]
    private void OnSignUpClicked()
    {
        Shell.Current.GoToAsync("///" + nameof(SignupPage));

    }
    [RelayCommand]
    private async Task OnSingInClicked()
    {
        IsLoading = 1;
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
          
            var navigationParameters = new Dictionary<string, object>
            {
                { "email", Username },
            };

            clearFeilds();
            await Shell.Current.GoToAsync("///" + nameof(OpeningPage),false,navigationParameters);
            IsLoading = 0;
            
        }
        else
        {
            IsLoading = 0;
            Debug.WriteLine("Not correct information");
        }

    }
    private async Task  LogInAutomatic()
    {
        IsLoading = 1;
       string? email = await SecureStorage.GetAsync("email");
       string? password = await SecureStorage.GetAsync("password");
        if (email != null && password != null)
        {

            var user = new User
            {
                email = email,
                password = password
            };

            bool isCorrectCredentials = await _userService.LogUserInAsync(user);
            if (isCorrectCredentials)
            {
                var navigationParameters = new Dictionary<string, object>
{
                    { "email", email },
                };

                await Shell.Current.GoToAsync("///" + nameof(OpeningPage), false, navigationParameters);
                IsLoading = 0;
            }
        }
    }
    public void clearFeilds()
    {
        Username = "";
        Password = "";
    }

}

