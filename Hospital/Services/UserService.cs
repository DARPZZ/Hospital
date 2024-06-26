﻿using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Hospital.Models;
using Newtonsoft.Json;

namespace Hospital.Services
{
    public class UserService : IUserService
    {
        private readonly string baseString = "http://srv534529.hstgr.cloud:4000/";
        //private readonly string baseString = "http://10.176.69.180:4000/";

        /// <summary>
        /// Opretter en ny bruger i systemet
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Bruges til at logge en bruger ind normalt
        /// Bruges også til Auto login
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> LogUserInAsync(User user)
        {
           
                var url = baseString + "signin";
                var userJson = JsonConvert.SerializeObject(user);
                var content = new StringContent(userJson, Encoding.UTF8, "application/json");

                try
                {
                    var response = await HttpClientSingleton.Client.PostAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        var cookies = HttpClientSingleton.Handler.CookieContainer.GetCookies(new Uri(baseString));
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
        /// <summary>
        /// Henter en bruger baseret på email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>

        public async Task<User> GetUserByEmail(string email)
        {
            try
            {
                var endpoint = baseString + "users/" + email;
                var result = HttpClientSingleton.Client.GetAsync(endpoint).Result;
                if (result.IsSuccessStatusCode)
                {
                    var cookies = HttpClientSingleton.Handler.CookieContainer.GetCookies(new Uri(baseString));
                    var json = await result.Content.ReadAsStringAsync();
                    var user = JsonConvert.DeserializeObject<User>(json);
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Giver brugeren mulighed for a ændre adgangskode ord via email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<User> SetNewPassword(string email, string password)
        {
            var endpoint = baseString + "users/" + email + "/" + "password";

            var jsonContent = new StringContent(JsonConvert.SerializeObject(new { password }), Encoding.UTF8, "application/json");
            var response = await HttpClientSingleton.Client.PatchAsync(endpoint, jsonContent);
            Debug.WriteLine(response.ToString());
            if (response.IsSuccessStatusCode)
            {
                var cookies = HttpClientSingleton.Handler.CookieContainer.GetCookies(new Uri(baseString));
                var userJson = await response.Content.ReadAsStringAsync();

                return new User { email = email };
            }
            else
            {
                throw new Exception("Failed to update password");
            }
        }
    }
}
