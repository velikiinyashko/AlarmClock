using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using AlarmClock.Core.Mvvm;
using AlarmClock.Core.CommandModule;
using AlarmClock.Core.Models;
using AlarmClock.Services.Interfaces;
using Prism.Regions;
using System.Collections.ObjectModel;

namespace AlarmClock.Modules.ModuleAlarm.ViewModels
{
    public class ViewAlarmViewModel : RegionViewModelBase
    {
        private string _nameAlert;
        public string NameAlert { get => _nameAlert; set { SetProperty(ref _nameAlert, value); } }

        private ObservableCollection<AlarmModel> _alarmList;
        public ObservableCollection<AlarmModel> AlarmList { get => _alarmList; set { SetProperty(ref _alarmList, value); } }


        public ViewAlarmViewModel(IRegionManager regionManager, INotifyIcon notify):
            base(regionManager, notify)
        {
            AlarmList = new()
            {
                new AlarmModel() { Id = Guid.NewGuid(), Name = "Будильник", Time = DateTime.Now, IsEnable = true },
                new AlarmModel() { Id = Guid.NewGuid(), Name = "Будильник2", Time = DateTime.Now, IsEnable = true },
                new AlarmModel() { Id = Guid.NewGuid(), Name = "Будильник3", Time = DateTime.Now, IsEnable = true },
                new AlarmModel() { Id = Guid.NewGuid(), Name = "Будильник4", Time = DateTime.Now, IsEnable = true },
                new AlarmModel() { Id = Guid.NewGuid(), Name = "Будильник5", Time = DateTime.Now, IsEnable = true },
                new AlarmModel() { Id = Guid.NewGuid(), Name = "Будильник5", Time = DateTime.Now, IsEnable = true },
                new AlarmModel() { Id = Guid.NewGuid(), Name = "Будильник5", Time = DateTime.Now, IsEnable = true },
                new AlarmModel() { Id = Guid.NewGuid(), Name = "Будильник5", Time = DateTime.Now, IsEnable = true },
                new AlarmModel() { Id = Guid.NewGuid(), Name = "Будильник5", Time = DateTime.Now, IsEnable = true },
                new AlarmModel() { Id = Guid.NewGuid(), Name = "Будильник5", Time = DateTime.Now, IsEnable = true },
                new AlarmModel() { Id = Guid.NewGuid(), Name = "Будильник5", Time = DateTime.Now, IsEnable = true },
                new AlarmModel() { Id = Guid.NewGuid(), Name = "Будильник5", Time = DateTime.Now, IsEnable = true },

            };
        }
    }
}
