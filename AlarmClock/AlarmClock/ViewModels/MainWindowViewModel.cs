using Prism.Mvvm;
using AlarmClock.Services.Interfaces;
using AlarmClock.Core.Mvvm;
using AlarmClock.Core.Event;
using Prism.Regions;
using System.Threading;
using System.Windows.Threading;
using Prism.Events;
using System;
using ControlzEx.Theming;
using Prism.Modularity;
using AlarmClock.Core;
using AlarmClock.Modules.ViewModules;

namespace AlarmClock.ViewModels
{
    public class MainWindowViewModel : RegionViewModelBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private ITimeClockManagerService _timeManager;
        private IModuleManager _module;

        public MainWindowViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, INotifyIcon notify, ITimeClockManagerService timeManager, IModuleManager module):
            base(regionManager, eventAggregator, notify)
        {
            _module = module;
            _timeManager = timeManager;
            //Notify.Message("Start App");
        }
        
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
        }
    }
}
