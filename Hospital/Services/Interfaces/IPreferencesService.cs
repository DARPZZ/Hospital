﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services.Interfaces
{
    public interface IPreferencesService
    {
        string Get(string key, string defaultValue);
        void Set(string key, string value);
        void Remove(string key);
    }
}