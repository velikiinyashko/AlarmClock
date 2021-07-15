using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using AlarmClock.Core.Mvvm;
using AlarmClock.Services.Interfaces;
using Prism.Regions;
using AlarmClock.Core.Event;
using Prism.Events;

namespace AlarmClock.Modules.ViewModules.ViewModels
{
    public class MainViewWindowViewModel : RegionViewModelBase
    {
        private string _clockLabel;
        public string ClockLabel { get => _clockLabel; set { SetProperty(ref _clockLabel, value); } }
        public MainViewWindowViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, INotifyIcon notify):
            base(regionManager, eventAggregator, notify)
        {
            EventAggregator.GetEvent<TimeTickEvent>().Subscribe(VisualTime);
        }
        private void VisualTime(string obj)
        {
            ClockLabel = obj;
        }
    }
}
