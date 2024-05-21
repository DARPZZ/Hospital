using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Hospital.Models;
using Newtonsoft.Json;

namespace Hospital.Services
{
    public class UserService
    {
        private readonly string baseString = "http://10.176.163.239:4000/";

        public async Task<bool> CreateUserAsync(User user)
        {
            var url = baseString + "users";
            var userJson = JsonConvert.SerializeObject(user);
            var content = new StringContent(userJson, Encoding.UTF8, "application/json");

            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(url, content);
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
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> LogUserInAsync(User user)
        {
            var url = baseString + "signin";
            var userJson = JsonConvert.SerializeObject(user);
            var content = new StringContent(userJson, Encoding.UTF8, "application/json");

            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(url, content);
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
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
                return false;
            }
        }
        public async Task<User> Test(string emaik)
        {
            using(var httpClient = new HttpClient())
            {
                var endpoint = baseString + "users/" + emaik;
                var result = httpClient.GetAsync(endpoint).Result;
                if(result.IsSuccessStatusCode)
                {
                    var json = await result.Content.ReadAsStringAsync();
                    var user = JsonConvert.DeserializeObject<User>(json);
                    return user;
                }
                else
                {
                    return null;
                }
                
                
                
            }
        }
       
    }
}
