using Prism.Regions;
using System;
using Prism.Events;
using AlarmClock.Core.CommandModule;
using Prism.Commands;
using AlarmClock.Core.Interfaces;

namespace AlarmClock.Core.Mvvm
{
    public class RegionViewModelBase : ViewModelBase, INavigationAware, IConfirmNavigationRequest
    {
        protected IRegionManager RegionManager { get; private set; }
        protected INotifyIcon Notify { get; private set; }
        protected IEventAggregator EventAggregator { get; private set; }
        protected ICommandCore CommandCore { get; private set; }
        public DelegateCommand AlarmViewCommand { get; private set; }
        public DelegateCommand AlaramAddCommand { get; private set; }
        public DelegateCommand DeleteAlarmCommand { get; private set; }

        public RegionViewModelBase(IRegionManager regionManager, ICommandCore commandCore, IEventAggregator eventAggregator, INotifyIcon notify)
        {
            CommandCore = commandCore;
            RegionManager = regionManager;
            Notify = notify;
            EventAggregator = eventAggregator;
            AlarmViewCommand = new(AlarmsView);
            AlaramAddCommand = new(AlarmAdd);
            DeleteAlarmCommand = new(DeleteAlarm);
        }

        public RegionViewModelBase(IRegionManager regionManager, INotifyIcon notify)
        {
            RegionManager = regionManager;
            Notify = notify;
        }

        #region комманды

        protected virtual void AlarmsView() { }

        /// <summary>
        /// добавление будильника
        /// </summary>
        protected virtual void AlarmAdd() { }

        /// <summary>
        /// удаление будильника
        /// </summary>
        protected virtual void DeleteAlarm() { }

        #endregion


        #region навигация
        public virtual void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            continuationCallback(true);
        }

        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {

        }
        #endregion
    }
}
