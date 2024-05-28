using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Maui.Behaviors;
using Hospital.Models;
using Hospital.Services;

namespace Hospital.ViewModels;

public partial class SignupViewModel : BaseViewModel
{
    
    [ObservableProperty]
    private Color statusColor;

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
        StatusColor = Color.FromRgb(144, 238, 144);
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
        StatusColor = Color.FromRgb(144, 238, 144);
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
            StatusColor = Color.FromRgb(255, 0, 0);
            IsLoading = false;
            Debug.WriteLine("User registration failed.");
        }
    }

}
