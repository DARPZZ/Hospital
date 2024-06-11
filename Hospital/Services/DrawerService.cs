using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Models;
using Hospital.Services.Interfaces;
using Newtonsoft.Json;

namespace Hospital.Services
{
    public class DrawerService : IDrawerService
    {
        private readonly string baseString = "http://srv534529.hstgr.cloud:4000/";

        //private readonly string baseString = "http://10.176.69.180:4000/";
        
        //Giver brugeren en skuffe
        public async Task<bool> SetDrawerToUser(Drawer drawer)
        {
            var url = baseString + "locks/user";
            var userJson = JsonConvert.SerializeObject(drawer);
            var content = new StringContent(userJson, Encoding.UTF8, "application/json");

            try
            {
                    var response = await HttpClientSingleton.Client.PostAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        Debug.WriteLine($"Error: {errorContent}");
                        return false;
                    }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
                return false;
            }
        }
        //Retunere en drawer, hvor man kan udlede id 
        public async Task<Drawer> GetIdOfUsersDrawer(string email)
        {
                var endpoint = baseString + "locks/email/" + email;
                var result = HttpClientSingleton.Client.GetAsync(endpoint).Result;
                if (result.IsSuccessStatusCode)
                {
                    var cookies = HttpClientSingleton.Handler.CookieContainer.GetCookies(new Uri(baseString));
                    var json = await result.Content.ReadAsStringAsync();
                    
                    var drawer = JsonConvert.DeserializeObject<Drawer>(json);
                    
                    return drawer;
                }
                else
                {
                    Debug.WriteLine("Mistake");
                    return null;
                }

            

        }
        /// <summary>
        /// Henter status på den specifikke skuffe 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Drawer> GetStatus(string id)
        {
            var endpoint = baseString + "locks/status/" + id;
            var result = HttpClientSingleton.Client.GetAsync(endpoint).Result;
            if (result.IsSuccessStatusCode)
            {
                var cookies = HttpClientSingleton.Handler.CookieContainer.GetCookies(new Uri(baseString));
                var json = await result.Content.ReadAsStringAsync();
                Debug.WriteLine(json.ToString() + "hest");
                var drawer = JsonConvert.DeserializeObject<Drawer>(json);

                return drawer;
            }
            else
            {
                Debug.WriteLine("Status fail");
                return null;
            }



        }
        /// <summary>
        /// Skifter mellem åben og lukning af skuffen baseret på endpoint
        /// </summary>
        /// <param name="drawer"></param>
        /// <param name="endpoint"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<bool> OpenLockDrawer(Drawer drawer, string endpoint, string email)
        {
            var url = baseString + "locks/" + endpoint + "/" + email;
            var userJson = JsonConvert.SerializeObject(drawer);
            var content = new StringContent(userJson, Encoding.UTF8, "application/json");

            try
            {
              
                    var response = await HttpClientSingleton.Client.PostAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        Debug.WriteLine($"Error: {errorContent}");
                        return false;
                    }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
                return false;
            }
        }

    }
}
