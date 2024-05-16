using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Models;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;

namespace Hospital.Services
{
    public class UserService
    {
            private readonly HttpClient _httpClient;
            private readonly string baseString = "http://10.176.163.241:4000/";
            public UserService()
            {
                _httpClient = new HttpClient();
            }


            public async Task<bool> CreateUserAsync(User user)
            {
                var url = baseString + "users";
                var userJson = JsonConvert.SerializeObject(user);
                var content = new StringContent(userJson, Encoding.UTF8, "application/json");
                Debug.WriteLine(userJson);
                try
                {
                    var response = await _httpClient.PostAsync(url, content);
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
