using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Models;

namespace Hospital.Services
{
    public interface IUserService
    {
        Task<bool> CreateUserAsync(User user);
        Task<bool> LogUserInAsync(User user);
        Task<User> Test(string email);
        Task<User> SetNewPassword(string email, string password);
    }
}
