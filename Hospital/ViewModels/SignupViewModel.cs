using CommunityToolkit.Maui.Behaviors;
using Hospital.Models;
using Hospital.Services;

namespace Hospital.ViewModels;

public partial class SignupViewModel : BaseViewModel
{

    private readonly UserService _userService;
    [ObservableProperty]
    private bool _isPasswordTooltipVisible;
    [ObservableProperty]
    private string password;
    [ObservableProperty]
    private string email;
    [ObservableProperty]
    private string firstName;
    [ObservableProperty]
    private string lastName;
    [ObservableProperty]
    private bool isLoading;
    public SignupViewModel()
    {
        _userService = new UserService();
        IsLoading = false;
    }
    [RelayCommand]
    private async Task OnBackClicked()
    {
        await Shell.Current.GoToAsync("///" + nameof(LoginnPage));
    }
    [RelayCommand]
    private async Task OnOkClicked()
    {
        IsLoading = true;
        var user = new User
        {
            email = Email,
            password = Password,
            firstName = FirstName,
            lastName = LastName
        };
        
        bool isCreated = await _userService.CreateUserAsync(user);
        if (isCreated)
        {
            Debug.WriteLine("User registered successfully.");
            IsLoading = false;
            await Shell.Current.GoToAsync("///" + nameof(LoginnPage));
        }
        else
        {
            IsLoading = false;
            Debug.WriteLine("User registration failed.");
        }
    }

}
