using AlarmClock.Services.Interfaces;
using System.Windows.Threading;
using AlarmClock.Core;
using Prism.Events;
using System.Threading.Tasks;
using AlarmClock.Core.Event;
using System;
using AlarmClock.Core.Models;
using System.Collections.Generic;
using AlarmClock.Core.Interfaces;
using System.IO;
using System.Text.Json;

namespace AlarmClock.Services
{
    public class TimeClockManagerService : ITimeClockManagerService
    {
        private DispatcherTimer _dispatcherTimer;
        private IEventAggregator _eventAggregator;
        private 
        private List<AlarmModel> _alarms;

        public TimeClockManagerService(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<DisableAlarmEvent>().Subscribe(DisableAlarm);
            GetTime();
            _alarms = new()
            {
                new AlarmModel() { Id = Guid.NewGuid(), Name = "Будильник", Time = DateTime.Now, IsEnable = true },
                new AlarmModel() { Id = Guid.NewGuid(), Name = "Будильник2", Time = DateTime.Now, IsEnable = true },
                new AlarmModel() { Id = Guid.NewGuid(), Name = "Будильник3", Time = DateTime.Now, IsEnable = true },
                new AlarmModel() { Id = Guid.NewGuid(), Name = "Будильник4", Time = DateTime.Now, IsEnable = true },
                new AlarmModel() { Id = Guid.NewGuid(), Name = "Будильник5", Time = DateTime.Now, IsEnable = true },
                new AlarmModel() { Id = Guid.NewGuid(), Name = "Будильник5", Time = DateTime.Now, IsEnable = false },
                new AlarmModel() { Id = Guid.NewGuid(), Name = "Будильник5", Time = DateTime.Now, IsEnable = true },
                new AlarmModel() { Id = Guid.NewGuid(), Name = "Будильник5", Time = DateTime.Now, IsEnable = true },
                new AlarmModel() { Id = Guid.NewGuid(), Name = "Будильник5", Time = DateTime.Now, IsEnable = true },
                new AlarmModel() { Id = Guid.NewGuid(), Name = "Будильник5", Time = DateTime.Now, IsEnable = true },
                new AlarmModel() { Id = Guid.NewGuid(), Name = "Будильник5", Time = DateTime.Now, IsEnable = true },
                new AlarmModel() { Id = Guid.NewGuid(), Name = "Будильник5", Time = DateTime.Now, IsEnable = true },

            };
        }

        private async Task LoadAlarms()
        {
            try
            {
                if()
                using (FileStream stream = new FileStream($"{Directory.GetCurrentDirectory()}\\config.json", FileMode.Open))
                {
                    List<AlarmModel> config = await JsonSerializer.DeserializeAsync<List<AlarmModel>>(stream);
                    return config;
                }
            }
            catch (Exception ex)
            {
                //_log.WriteLog(LogLevel.Error, ex.Message);
                //return null;
            }
        }

        private void DisableAlarm(bool obj)
        {
            throw new NotImplementedException();
        }

        private void GetTime()
        {
            Task.Run(() =>
            {
                while (true)
                    _eventAggregator.GetEvent<TimeTickEvent>().Publish(DateTime.Now.ToString("HH:mm:ss"));
            });
        }

        public List<AlarmModel> GetAlarmList()
        {
            return _alarms;
        }

        public void AddAlarm(AlarmModel alarm)
        {
            _alarms.Add(alarm);
        }

        public void RemoveAlarm(AlarmModel alarm)
        {
            _alarms.Remove(alarm);
        }
    }
}
