using Hospital.Models;
using Hospital.Services;
using Microsoft.VisualBasic;

namespace Hospital.ViewModels
{
    public partial class OpeningViewModel : BaseViewModel,IQueryAttributable
    {
        Feedback feedback = new Feedback();
        enum buttonOptions
        {
            open_drawer,
            close_drawer,
        }
        private string email = "";
        private readonly UserService _userService;
        private readonly DrawerService _drawerService;
        public OpeningViewModel()
        {
            LockUpButtonText = buttonOptions.open_drawer.ToString();
            _userService = new UserService();
            _drawerService = new DrawerService();
            string id = Preferences.Default.Get("drawerID", "Youre id");
            ScanText = id;
        }
        [ObservableProperty]
        public string scanText;
        [ObservableProperty]
        private string lockUpButtonText;
        [ObservableProperty]
        private string welcome;
        public  async void ApplyQueryAttributes(IDictionary<string, object> query)
        { 
            if(query.ContainsKey("scanText"))
            {
                ScanText = query["scanText"] as string;
                var hest = int.Parse(ScanText);
                await AssignDrawerIdToUser(hest, email);

            }
            if(query.ContainsKey("email"))
            {
                
                
                email = query["email"] as string;
                var toScanText = await GetTheDrawerId(email);
                Debug.WriteLine(toScanText);
                ScanText = "Youre id is:    " + toScanText.ToString();

                await getNameOfUser();
                
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

        [RelayCommand]
        private async void OnOpenDrawerClicked()
        {
            feedback.StartHaptiskFeedback();
            GetTimeOfDay();
            var drawer = new Drawer
            {
                email = email,
            };
            if (LockUpButtonText == buttonOptions.open_drawer.ToString())
            {
                await _drawerService.OpenLockDrawer(drawer, "unlock", email);
                LockUpButtonText = buttonOptions.close_drawer.ToString();
            }
            else if (LockUpButtonText == buttonOptions.close_drawer.ToString())
            {
                await _drawerService.OpenLockDrawer(drawer, "lock", email);
                LockUpButtonText = buttonOptions.open_drawer.ToString();
            }



        }
        private async Task getNameOfUser()
        {
           
            var user = await _userService.Test(email);
            Welcome = GetTimeOfDay()  + "\n" + user.firstName+ " " + user.lastName;
            
            

        }
        private async Task AssignDrawerIdToUser(int id, string email)
        {
            var drawer  = new Drawer
            {
                email = email,
                id = id,
            };
 

            bool isCreated = await _drawerService.SetDrawerToUser(drawer);
            if (isCreated)
            {Debug.WriteLine("Drawer was placed sucessfully ");}
            else {Debug.WriteLine("drawer failed.");}
        }
        private async Task<int> GetTheDrawerId(string email)
        {
            
            var drawer = await _drawerService.GetIdOfUsersDrawer(email);
           
            if (drawer == null)
            {
                return 0;
            }else
            {
                int drawerId = drawer.id;
                return drawerId;
            }
            
        }
        private string GetTimeOfDay()
        {
            var current_time = DateTime.Now.ToString("HH");
            var intTme = int.Parse(current_time);
            if ( intTme<5 && intTme<12)
            {
                return "Good morning";
            }else if(intTme<12 && intTme < 18)
            {
                return "Good afternoon";
            }
            else
            {
                return "Good evening";
            }
            
        }
    }
}