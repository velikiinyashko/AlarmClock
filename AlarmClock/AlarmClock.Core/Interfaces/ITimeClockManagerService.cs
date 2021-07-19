using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlarmClock.Core.Models;

namespace AlarmClock.Core.Interfaces
{
    public interface ITimeClockManagerService
    {
        List<AlarmModel> GetAlarmList();
        Task AddAlarm(AlarmModel alarm);
        void RemoveAlarm(AlarmModel alarm);
    }
}
