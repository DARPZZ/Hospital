using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Models;

namespace Hospital.Services.Interfaces
{
    public interface IDrawerService
    {
        Task<bool> SetDrawerToUser(Drawer drawer);
        Task<Drawer> GetIdOfUsersDrawer(string email);
        Task<bool> OpenLockDrawer(Drawer drawer, string endpoint, string email);
    }
}
