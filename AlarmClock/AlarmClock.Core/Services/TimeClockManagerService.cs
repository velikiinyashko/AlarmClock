using AlarmClock.Core.Event;
using AlarmClock.Core.Interfaces;
using AlarmClock.Core.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private IEventAggregator _eventAggregator;
        private JsonSettings<List<AlarmModel>> _settings;
        private List<AlarmModel> _alarmsDef;
        private ObservableCollection<AlarmModel> _alarms;
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
                new AlarmModel() { Id = Guid.NewGuid(), Name = "Будильник", Time = (DateTime.Now + TimeSpan.FromMinutes(1)).ToString("HH:mm"), IsEnable = true, AlarmSounds = "Alarm", Status = 0 },
                new AlarmModel() { Id = Guid.NewGuid(), Name = "Будильник3", Time = (DateTime.Now + TimeSpan.FromMinutes(2)).ToString("HH:mm"), IsEnable = false, AlarmSounds = "Alarm", Status = 0 },
                new AlarmModel() { Id = Guid.NewGuid(), Name = "Будильник4", Time = (DateTime.Now + TimeSpan.FromMinutes(3)).ToString("HH:mm"), IsEnable = true, AlarmSounds = "Alarm", Status = 0 },
                new AlarmModel() { Id = Guid.NewGuid(), Name = "Будильник2", Time = (DateTime.Now + TimeSpan.FromMinutes(4)).ToString("HH:mm"), IsEnable = false, AlarmSounds = "Alarm", Status = 0 },
                new AlarmModel() { Id = Guid.NewGuid(), Name = "Будильник5", Time = (DateTime.Now + TimeSpan.FromMinutes(5)).ToString("HH:mm"), IsEnable = true, AlarmSounds = "Alarm", Status = 0 },

            };
            LoadAlarms();
        }

        /// <summary>
        /// загружаем настройки будильников
        /// </summary>
        /// <returns></returns>
        private async Task LoadAlarms()
        {
            Task task = Task.Run(async () =>
            {
                _alarms = new(await _settings.LoadConfig("alarms.json", _alarmsDef));

            });
            //_alarms = await _settings.LoadConfig("alarms.json", _alarmsDef);
            task.Wait();

        }
        /// <summary>
        /// останавливаем звуковой сигнал
        /// </summary>
        /// <param name="obj"></param>
        private void DisableAlarm(bool obj)
        {
            if (_player != null)
            {
                _player.Stop();
            }
        }
        /// <summary>
        /// получем текущее время
        /// </summary>
        private void GetTime()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    string TimeNow = DateTime.Now.ToString("HH:mm:ss");
                    string TimeAlarm = DateTime.Now.ToString("HH:mm");
                    _eventAggregator.GetEvent<TimeTickEvent>().Publish(TimeNow);
                    //ищем будильник назначенный на текущее время
                    if (_alarms != null)
                    {
                        try
                        {
                            var alarm = _alarms.FirstOrDefault(q => q.Time == TimeAlarm & q.IsEnable == true);
                            if (alarm != null)
                            {
                                if (alarm.Status == 0 & alarm.IsEnable == true)
                                {
                                    Task.Run(() =>
                                    {
                                        _player = new($"{Environment.CurrentDirectory}\\Sounds\\{alarm.AlarmSounds}.wav");
                                        _player.PlayLooping();
                                    });
                                    alarm.Status = 1;
                                    _alarms.Remove(alarm);
                                    _alarms.Add(alarm);
                                    _eventAggregator.GetEvent<EnableAlarmEvent>().Publish(alarm);
                                }
                            }
                            else
                            {
                                if (_player != null)
                                    _player.Stop();
                                foreach (var items in _alarms)
                                {
                                    items.Status = 0;
                                }
                            }
                        }
                        catch (Exception) { }
                    }
                }
            });
        }

        public List<AlarmModel> GetAlarmList()
        {
            return _alarms.ToList();
        }
        /// <summary>
        /// добавляем будильник
        /// </summary>
        /// <param name="alarm">сущность будильника</param>
        /// <returns></returns>
        public async Task AddAlarm(AlarmModel alarm)
        {
            var item = _alarms.FirstOrDefault(q => q.Id == alarm.Id);
            if (item == null)
            {
                _alarms.Add(alarm);
                Task task = Task.Run(async () =>
                {
                    await _settings.SaveConfig("alarms.json", _alarms.ToList());
                });
            }
            else
            {
                _alarms.Remove(item);
                _alarms.Add(alarm);
                Task task = Task.Run(async () =>
                {
                    await _settings.SaveConfig("alarms.json", _alarms.ToList());
                });
            }
        }

        public void RemoveAlarm(AlarmModel alarm)
        {
            if (alarm != null)
            {
                _alarms.Remove(alarm);
                Task task = Task.Run(async () =>
                {
                    await _settings.SaveConfig("alarms.json", _alarms.ToList());
                });
            }
        }
    }
}
