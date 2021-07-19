using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using AlarmClock.Core.Mvvm;
using Prism.Regions;
using AlarmClock.Core.Event;
using Prism.Events;
using AlarmClock.Core.CommandModule;
using AlarmClock.Core.Interfaces;

namespace AlarmClock.Modules.ViewModules.ViewModels
{
    public class MainViewWindowViewModel : RegionViewModelBase
    {
        private string _clockLabel;
        public string ClockLabel { get => _clockLabel; set { SetProperty(ref _clockLabel, value); } }

        private string _dateLabel;
        public string DateLabel { get => _dateLabel; set { SetProperty(ref _dateLabel, value); } }

        public MainViewWindowViewModel(IRegionManager regionManager,ICommandCore commandCore, IEventAggregator eventAggregator, INotifyIcon notify):
            base(regionManager,commandCore, eventAggregator, notify)
        {
            EventAggregator.GetEvent<TimeTickEvent>().Subscribe(VisualTime);
        }
        private void VisualTime(string obj)
        {
            ClockLabel = obj;
            DateLabel = DateTime.Now.ToString("dddd: dd MMMM yyyy");
        }
    }
}
