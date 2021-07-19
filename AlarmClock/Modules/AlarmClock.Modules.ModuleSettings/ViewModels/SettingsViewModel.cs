using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using AlarmClock.Core.Mvvm;
using Prism.Regions;
using AlarmClock.Core.Interfaces;

namespace AlarmClock.Modules.ModuleSettings.ViewModels
{
    public class SettingsViewModel : RegionViewModelBase
    {

        public SettingsViewModel(IRegionManager regionManager, INotifyIcon notify):
            base(regionManager, notify)
        {
            Notify.Message("Настройки!");
        }
    }
}
