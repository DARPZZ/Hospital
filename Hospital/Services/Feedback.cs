﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public class Feedback
    {
        public void StartHaptiskFeedback()
        {
            Vibration.Default.Vibrate();
        }
    }
}
