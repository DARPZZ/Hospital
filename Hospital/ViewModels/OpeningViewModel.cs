using Hospital.Models;
using Hospital.Services;
using Microsoft.VisualBasic;

namespace Hospital.ViewModels
{
    public partial class OpeningViewModel : BaseViewModel,IQueryAttributable
    {
        public string YourObject { get; private set; }
        private readonly UserService _userService;
        public OpeningViewModel()
        {
            _userService = new UserService();
            string id = Preferences.Default.Get("drawerID", "Youre id");
            ScanText = id;
        }
        [ObservableProperty]
        public string scanText;
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        { 
            ScanText = query["scanText"] as string;
            //setPref();
        }


        [RelayCommand]
        private void OnLogoutClicked()
        {
            Debug.WriteLine("logout clicked");
            SecureStorage.Default.Remove("email");
            SecureStorage.Default.Remove("password");
            Preferences.Default.Remove("drawerID");
            Shell.Current.GoToAsync("///" + nameof(LoginnPage));

        }
        [RelayCommand]
        private async Task OnTestClicked()
        {

            var user = await _userService.Test("w");
            Debug.WriteLine(user.email);


        }
        public void setPref()
        {
            Preferences.Default.Set("drawerID", ScanText);
        }

    }
}