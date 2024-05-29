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
    private readonly RegistrationForm _registrationForm;

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

    public SignupViewModel(UserService userService, RegistrationForm registrationForm)
    {
        StatusColor = Color.FromRgb(144, 238, 144);
        _userService = userService;
        _registrationForm = registrationForm;
        IsLoading = false;
    }
 
    [RelayCommand]
    private async Task OnBackToLoginClicked()
    {
        await Shell.Current.GoToAsync("///" + nameof(LoginnPage));
    }
    [RelayCommand]
    private async Task OnSignupClicked()
    {
        StatusColor = Color.FromRgb(144, 238, 144);
        IsLoading = true;
        _registrationForm.Email = Email;
        _registrationForm.Password = Password;
        _registrationForm.FirstName = FirstName;
        _registrationForm.LastName = LastName;

        if (!_registrationForm.IsValid())
        {
            StatusColor = Color.FromRgb(255, 0, 0);
            IsLoading = false;
            return;
        }

       
        var user = new User
        {
            email = _registrationForm.Email,
            password = _registrationForm.Password,
            firstName = _registrationForm.FirstName,
            lastName = _registrationForm.LastName
        };

        bool isCreated = await _userService.CreateUserAsync(user);
        if (isCreated)
        {
            
            IsLoading = false;
            await Shell.Current.GoToAsync("///" + nameof(LoginnPage));
        }
        else
        {
            StatusColor = Color.FromRgb(255, 0, 0);
            IsLoading = false;
           
        }
    }

}
