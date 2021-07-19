using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using AlarmClock.Core.Mvvm;
using Prism.Regions;
using AlarmClock.Core.Interfaces;

namespace AlarmClock.Modules.ViewModules.ViewModels
{
    public class AlarmViewViewModel : RegionViewModelBase
    {
        private string _nameAlert;
        public string NameAlert { get => _nameAlert; set { SetProperty(ref _nameAlert, value); } }
        public AlarmViewViewModel(IRegionManager regionManager, INotifyIcon notify):
            base(regionManager, notify)
        {
            NameAlert = "test";
        }


    }
}
