using Hospital.Models;
using Hospital.Services;

namespace Hospital.ViewModels;

public partial class SignupViewModel : BaseViewModel
{

    private readonly UserService _userService;

    [ObservableProperty]
    private string password;
    [ObservableProperty]
    private string email;
    [ObservableProperty]
    private string firstName;
    [ObservableProperty]
    private string lastName;
    public SignupViewModel()
    {
        _userService = new UserService();
    }
    [RelayCommand]
    private async Task OnOkClicked()
    {
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

        }
        else
        {
            Debug.WriteLine("User registration failed.");
        }



    }
}
