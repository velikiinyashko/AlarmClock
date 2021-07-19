using Prism.Mvvm;
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
using System.Windows;
using AlarmClock.Core.CommandModule;
using AlarmClock.Core.Models;
using AlarmClock.Core.Interfaces;
using MaterialDesignThemes.Wpf;
using System.Threading.Tasks;

namespace AlarmClock.ViewModels
{
    public class MainWindowViewModel : RegionViewModelBase
    {
        private string _title = "Время, оно не вечно, цени его";
        public string Title { get { return _title; } set { SetProperty(ref _title, value); } }

        private bool _isVisibility;
        public bool IsVisibility { get => _isVisibility; set { SetProperty(ref _isVisibility, value); } }

        private AlarmModel _alarm;
        public AlarmModel Alarm { get => _alarm; set { SetProperty(ref _alarm, value); } }

        private bool _dialogVisible;
        public bool DialogVisible { get => _dialogVisible; set { SetProperty(ref _dialogVisible, value); } }

        private ISnackbarMessageQueue _messageQueue;
        public ISnackbarMessageQueue MessageQueue { get { return _messageQueue; } set { SetProperty(ref _messageQueue, value); } }

        /// <summary>
        /// показ сообщения будильника?
        /// </summary>
        private bool _showAlarm;
        public bool ShowAlarm;

        private IModuleManager _module;

        public MainWindowViewModel(IRegionManager regionManager, ICommandCore commandCore, IEventAggregator eventAggregator, INotifyIcon notify, IModuleManager module) :
            base(regionManager, commandCore, eventAggregator, notify)
        {
            _module = module;
            EventAggregator.GetEvent<VisibleAddAlarmModalEvent>().Subscribe(VisibleAddAlarmModal);
            eventAggregator.GetEvent<OpenDialogAlarmEvent>().Subscribe(OpenDialogAlarm);
            eventAggregator.GetEvent<EnableAlarmEvent>().Subscribe(EnableAlarm);
            MessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(5));
        }

        private void EnableAlarm(AlarmModel obj)
        {
            MessageQueue.Enqueue(obj.Name, "Ok", async () => await Ok());
        }


        private async Task Ok()
        {
            EventAggregator.GetEvent<DisableAlarmEvent>().Publish(true);
        }

        private void OpenDialogAlarm(AlarmModel obj)
        {
            if (obj != null)
            {
                Alarm = obj;
                DialogVisible = true;
            }
        }

        private void VisibleAddAlarmModal(bool obj)
        {
            if (obj == true)
            {
                Alarm = new();
                DialogVisible = true;
            }
        }



        protected override void AlarmsView()
        {
            IsVisibility = IsVisibility == true ? false : true;
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            CommandCore.AlarmsViewCommand.RegisterCommand(AlarmViewCommand);
            //CommandCore.AlaramAddCommand.RegisterCommand();
            base.OnNavigatedTo(navigationContext);
        }
        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            Notify.Exit();
        }


    }
}
