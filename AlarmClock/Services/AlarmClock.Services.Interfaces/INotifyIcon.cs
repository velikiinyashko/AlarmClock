﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AlarmClock.Services.Interfaces
{
    public interface INotifyIcon
    {
        void Message(string Message);
    }
}