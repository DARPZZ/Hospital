using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Services.Interfaces;

namespace Hospital.Services
{
    public class PreferencesService : IPreferencesService
    {
        public string Get(string key, string defaultValue)
        {
            return Preferences.Default.Get(key, defaultValue);
        }

        public void Set(string key, string value)
        {
            Preferences.Default.Set(key, value);
        }

        public void Remove(string key)
        {
            Preferences.Default.Remove(key);
        }
    }

}