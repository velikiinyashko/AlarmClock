using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using AlarmClock.Core.Mvvm;
using AlarmClock.Services.Interfaces;
using Prism.Regions;

namespace AlarmClock.Modules.ModuleAlarm.ViewModels
{
    public class ViewAlarmViewModel : RegionViewModelBase
    {
        private string _nameAlert;
        public string NameAlert { get => _nameAlert; set { SetProperty(ref _nameAlert, value); } }
        public ViewAlarmViewModel(IRegionManager regionManager, INotifyIcon notify):
            base(regionManager, notify)
        {

        }
    }
}
