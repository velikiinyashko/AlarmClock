using AlarmClock.Core.Event;
using AlarmClock.Core.Interfaces;
using AlarmClock.Core.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace AlarmClock.Core.Services
{
    public class TimeClockManagerService : ITimeClockManagerService
    {
        private DispatcherTimer _dispatcherTimer;
        private IEventAggregator _eventAggregator;
        private JsonSettings<List<AlarmModel>> _settings;
        private List<AlarmModel> _alarmsDef;
        private List<AlarmModel> _alarms;
        private Dictionary<Guid, Timer> alarmtimer;
        System.Media.SoundPlayer _player;

        public TimeClockManagerService(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<DisableAlarmEvent>().Subscribe(DisableAlarm);
            //_eventAggregator.GetEvent<>
            GetTime();
            _settings = new(eventAggregator);
            _alarmsDef = new()
            {
                new AlarmModel() { Id = Guid.NewGuid(), Name = "Будильник", Time = (DateTime.Now + TimeSpan.FromMinutes(1)).ToString("HH:mm"), IsEnable = true, AlarmSounds = "Alarm" },
                new AlarmModel() { Id = Guid.NewGuid(), Name = "Будильник3", Time = (DateTime.Now + TimeSpan.FromMinutes(2)).ToString("HH:mm"), IsEnable = true, AlarmSounds = "Alarm" },
                new AlarmModel() { Id = Guid.NewGuid(), Name = "Будильник4", Time = (DateTime.Now + TimeSpan.FromMinutes(3)).ToString("HH:mm"), IsEnable = true, AlarmSounds = "Alarm" },
                new AlarmModel() { Id = Guid.NewGuid(), Name = "Будильник2", Time = (DateTime.Now + TimeSpan.FromMinutes(4)).ToString("HH:mm"), IsEnable = true, AlarmSounds = "Alarm" },
                new AlarmModel() { Id = Guid.NewGuid(), Name = "Будильник5", Time = (DateTime.Now + TimeSpan.FromMinutes(5)).ToString("HH:mm"), IsEnable = true, AlarmSounds = "Alarm" },

            };
            _alarms = _settings.LoadConfig("alarms.json", _alarmsDef).Result;

            //LoadAlarms();

        }

        //private 

        private async Task LoadAlarms()
        {
        }

        private void DisableAlarm(bool obj)
        {
            if (_player != null)
            {
                _player.Stop();
                _player = null;
            }
        }

        private void GetTime()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    string TimeNow = DateTime.Now.ToString("HH:mm:ss");
                    string TimeAlarm = DateTime.Now.ToString("HH:mm");
                    _eventAggregator.GetEvent<TimeTickEvent>().Publish(TimeNow);
                    var alarm =  _alarmsDef.FirstOrDefault(q => q.Time == TimeAlarm);
                    if (alarm != null)
                    {
                        _player = new($"{Environment.CurrentDirectory}\\Sounds\\{alarm.AlarmSounds}.wav");
                        _player.PlayLooping();
                        _eventAggregator.GetEvent<EnableAlarmEvent>().Publish(true);
                    }
                }
            });
        }

        public List<AlarmModel> GetAlarmList()
        {
            return _alarms == null ? _alarmsDef : _alarms;
        }

        public async Task AddAlarm(AlarmModel alarm)
        {
            _alarms.Add(alarm);
            await _settings.SaveConfig("alarm.json", _alarms);
        }

        public void RemoveAlarm(AlarmModel alarm)
        {
            _alarms.Remove(alarm);
        }
    }
}
