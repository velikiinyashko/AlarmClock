using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using AlarmClock.Core.Mvvm;
using AlarmClock.Core.CommandModule;
using AlarmClock.Core.Models;
using AlarmClock.Core.Event;
using Prism.Regions;
using System.Collections.ObjectModel;
using Prism.Events;
using AlarmClock.Core.Interfaces;

namespace AlarmClock.Modules.ModuleAlarm.ViewModels
{
    public class ViewAlarmViewModel : RegionViewModelBase
    {
        private string _nameAlert;
        public string NameAlert { get => _nameAlert; set { SetProperty(ref _nameAlert, value); } }

        private ObservableCollection<AlarmModel> _alarmList;
        public ObservableCollection<AlarmModel> AlarmList { get => _alarmList; set { SetProperty(ref _alarmList, value); } }

        private ITimeClockManagerService _timeClockManagerService;

        public ViewAlarmViewModel(IRegionManager regionManager, ICommandCore commandCore, IEventAggregator eventAggregator, INotifyIcon notify, ITimeClockManagerService timeClockManagerService) :
            base(regionManager, commandCore, eventAggregator, notify)
        {
            _timeClockManagerService = timeClockManagerService;
            AlarmList = new(_timeClockManagerService.GetAlarmList());
            EventAggregator.GetEvent<DeleteAlarmEvent>().Subscribe(DeleteAlarm);
       
        }

        private void DeleteAlarm(Guid obj)
        {
            AlarmList.Remove(AlarmList.FirstOrDefault(q => q.Id == obj));
        }

        protected override void AlarmAdd()
        {
            EventAggregator.GetEvent<VisibleAddAlarmModalEvent>().Publish(true);
        }

        

        protected override void DeleteAlarm()
        {

        }
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            CommandCore.DeleteAlarmCommand.RegisterCommand(DeleteAlarmCommand);
        }
        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            CommandCore.DeleteAlarmCommand.UnregisterCommand(DeleteAlarmCommand);
        }
    }
}
