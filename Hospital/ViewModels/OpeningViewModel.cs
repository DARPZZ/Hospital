﻿using System.Net.Mail;
using System.Net;
using Hospital.Models;
using Hospital.Services;
using Microsoft.VisualBasic;
using Hospital.Services.Interfaces;
using Plugin.LocalNotification;

namespace Hospital.ViewModels
{
    public partial class OpeningViewModel : BaseViewModel,IQueryAttributable
    {
        [ObservableProperty]
        private string stateOfDrawer;
        [ObservableProperty]
        public string scanText;
        [ObservableProperty]
        private string lockUpButtonText;
        [ObservableProperty]
        private string welcome;
        Feedback feedback = new Feedback();



        private string email = "";
        readonly  UserService _userService;
        readonly  DrawerService _drawerService;
        readonly IPreferencesService _preferencesService;
        readonly IUserService us;
        readonly IDrawerService ds;
        enum buttonOptions
        {
            open_drawer,
            close_drawer,
        }
 
        public OpeningViewModel(UserService userService, DrawerService drawerService)
        {
            LockUpButtonText = buttonOptions.open_drawer.ToString();
            _userService = userService;
            _drawerService = drawerService;
            string id = Preferences.Default.Get("drawerID", "Youre id");
            ScanText = id;
            StateOfDrawer = "redball.png";
            
        }
        public OpeningViewModel(IUserService userService, IDrawerService drawerService, IPreferencesService preferencesService)
        {
            
            us = userService;
            ds = drawerService;
            _preferencesService = preferencesService;
            string id = _preferencesService.Get("drawerID", "Your id");
            ScanText = id;
            StateOfDrawer = "redball.png";
        }
        /// <summary>
        /// Henter de forskellige navigations parameter
        /// Om du kommer fra LoginPage eller QRPage
        /// </summary>
        /// <param name="query"></param>
        public  async void ApplyQueryAttributes(IDictionary<string, object> query)
        { 
            if(query.ContainsKey("scanText"))
            {
                ScanText =  query["scanText"] as string;
                var parsedText = int.Parse(ScanText);
                await AssignDrawerIdToUser(parsedText, email);

            }
            if(query.ContainsKey("email"))
            {
                email = query["email"] as string;
                var toScanText = await GetTheDrawerId(email);           
                GetStatusOfDrawer(toScanText.ToString());
                if (toScanText.ToString().Equals("0"))
                {
                    ScanText = "Go to QR to assing a drawer to you";
                }
                else
                {
                    ScanText = "Youre drawer number is:    " + toScanText.ToString();
                }
                await getNameOfUser();
            }
        }


        [RelayCommand]
        private void OnLogoutClicked()
        {
            LockUpButtonText = buttonOptions.open_drawer.ToString();
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
                PushNotification("start");
                LockUpButtonText = buttonOptions.close_drawer.ToString();
                StateOfDrawer = "greenball";
                await _drawerService.OpenLockDrawer(drawer, "unlock", email);

            }
            else if (LockUpButtonText == buttonOptions.close_drawer.ToString())
            {
                PushNotification("close");
                LockUpButtonText = buttonOptions.open_drawer.ToString();
                StateOfDrawer = "redball.png";
                await _drawerService.OpenLockDrawer(drawer, "lock", email);
            }
        }
        private async Task getNameOfUser()
        {
           
            var user = await _userService.GetUserByEmail(email);
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
            else { ScanText = "A user is allready assigned to that drawer";}
        }
        private async Task GetStatusOfDrawer(string id)
        {
            
            var drawer = await _drawerService.GetStatus(id);
            
            if(drawer.status == "true")
            {
                LockUpButtonText = buttonOptions.open_drawer.ToString();
            }
            else if (drawer.status == "false")
            {
                LockUpButtonText = buttonOptions.close_drawer.ToString();
            }
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
        private  string GetTimeOfDay()
        {
            var current_time = DateTime.Now.ToString("HH");
            var intTme = int.Parse(current_time);
            
            if ( intTme>5 && intTme<12)
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
        /// <summary>
        /// Sender en local notifikation ud til brugeren
        /// baseret på om skuffen er åben eller lukket 
        /// </summary>
        /// <param name="mode"></param>
        public void PushNotification(string mode)
        {
            if (mode == "start")
            {
                var request = new NotificationRequest
                {
                    Title = "Åben skuffe",
                    Subtitle = "Åben skuffe",
                    Description = "🔔 \"Pssst! Har du husket at lukke skabet?",
                    BadgeNumber = 42,
                    NotificationId  = 69,
                    Schedule = new NotificationRequestSchedule
                    {
                        NotifyTime = DateTime.Now.AddSeconds(10),

                    },
                };
                LocalNotificationCenter.Current.Show(request);
            }
            else
            {
                LocalNotificationCenter.Current.Cancel(69);
            }
        }
    }
}