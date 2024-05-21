using Hospital.Models;
using Hospital.Services;
using Microsoft.VisualBasic;

namespace Hospital.ViewModels
{
    public partial class OpeningViewModel : BaseViewModel,IQueryAttributable
    {
        private string email = "";
        private readonly UserService _userService;
        public OpeningViewModel()
        {
            _userService = new UserService();
            string id = Preferences.Default.Get("drawerID", "Youre id");
            ScanText = id;
        }
        [ObservableProperty]
        public string scanText;

        [ObservableProperty]
        private string welcome;
        public  void ApplyQueryAttributes(IDictionary<string, object> query)
        { 
            if(query.ContainsKey("scanText"))
            {
                ScanText = query["scanText"] as string;
                
            }
            if(query.ContainsKey("email"))
            {
                
                email = query["email"] as string;
                getNameOfUser();
            }
        }


        [RelayCommand]
        private void OnLogoutClicked()
        {
            Debug.WriteLine("logout clicked");
            ScanText = "Youre id";
            SecureStorage.Default.Remove("email");
            SecureStorage.Default.Remove("password");
            Preferences.Default.Remove("drawerID");
            Shell.Current.GoToAsync("///" + nameof(LoginnPage));

        }

        private async Task getNameOfUser()
        {
            Debug.WriteLine(email);
            var user = await _userService.Test(email);
            Welcome = "Good after noon" + "\n" + user.firstName+ " " + user.lastName;
           
        }
    }
}