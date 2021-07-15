using Prism.Regions;
using System;
using AlarmClock.Services.Interfaces;
using Prism.Events;

namespace AlarmClock.Core.Mvvm
{
    public class RegionViewModelBase : ViewModelBase, INavigationAware, IConfirmNavigationRequest
    {
        protected IRegionManager RegionManager { get; private set; }
        protected INotifyIcon Notify { get; private set; }
        protected IEventAggregator EventAggregator { get; private set; }

        public RegionViewModelBase(IRegionManager regionManager, IEventAggregator eventAggregator, INotifyIcon notify)
        {
            RegionManager = regionManager;
            Notify = notify;
            EventAggregator = eventAggregator;
        }

        public RegionViewModelBase(IRegionManager regionManager, INotifyIcon notify)
        {
            RegionManager = regionManager;
            Notify = notify;
        }
        

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
    }
}
