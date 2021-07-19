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
using System.Globalization;

namespace AlarmClock.ViewModels
{
    public class MainWindowViewModel : RegionViewModelBase
    {
        #region 
        /// <summary>
        /// заголовок окна
        /// </summary>
        private string _title = "Время, оно не вечно, цени его";
        public string Title { get { return _title; } set { SetProperty(ref _title, value); } }


        private bool _isVisibility;
        public bool IsVisibility { get => _isVisibility; set { SetProperty(ref _isVisibility, value); } }

        /// <summary>
        /// модель визуализатора будильника
        /// </summary>
        private AlarmModel _alarm;
        public AlarmModel Alarm { get => _alarm; set { SetProperty(ref _alarm, value); } }

        /// <summary>
        /// отображение модального окна
        /// </summary>
        private bool _dialogVisible;
        public bool DialogVisible { get => _dialogVisible; set { SetProperty(ref _dialogVisible, value); } }
        
        
        /// <summary>
        /// очередь на отображение в снекбаре
        /// </summary>
        private ISnackbarMessageQueue _messageQueue;
        public ISnackbarMessageQueue MessageQueue { get { return _messageQueue; } set { SetProperty(ref _messageQueue, value); } }
        #endregion

        /// <summary>
        /// конструктор формы с передачей зависимостей
        /// </summary>
        /// <param name="regionManager">менеджеры регионов отображения</param>
        /// <param name="commandCore">комманды</param>
        /// <param name="eventAggregator">события</param>
        /// <param name="notify">оповещения в трее</param>
        public MainWindowViewModel(IRegionManager regionManager, ICommandCore commandCore, IEventAggregator eventAggregator, INotifyIcon notify) :
            base(regionManager, commandCore, eventAggregator, notify)
        {
            EventAggregator.GetEvent<VisibleAddAlarmModalEvent>().Subscribe(VisibleAddAlarmModal);
            eventAggregator.GetEvent<OpenDialogAlarmEvent>().Subscribe(OpenDialogAlarm);
            eventAggregator.GetEvent<EnableAlarmEvent>().Subscribe(EnableAlarm);
            MessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(60));
        }

        /// <summary>
        /// включение снекбара с информацией о сработавшем будильнике 
        /// </summary>
        /// <param name="obj"></param>
        private void EnableAlarm(AlarmModel obj)
        {
            MessageQueue.Enqueue(obj.Name, "Отключить", async () => await Ok());
            Notify.Message($"{obj.Name} : {obj.Time}");
        }

        /// <summary>
        /// отключем будильник
        /// </summary>
        /// <returns></returns>
        private async Task Ok()
        {
            EventAggregator.GetEvent<DisableAlarmEvent>().Publish(true);
        }

        protected override void SaveAlarm()
        {
            DateTime time;
            try
            {
                try
                {
                    time = DateTime.ParseExact(Alarm.Time, "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                }
                catch (Exception)
                {
                    time = DateTime.ParseExact(Alarm.Time, "HH:mm", CultureInfo.InvariantCulture);
                }
                // генерируем сущъность будильника
                AlarmModel _alarm = new()
                {
                    AlarmSounds = "Alarm",
                    Id = Alarm.Id == Guid.Empty ? Guid.NewGuid() : Alarm.Id,
                    Name = Alarm.Name,
                    IsEnable = Alarm.IsEnable,
                    Time = time.ToString("HH:mm"),
                    Status = 0
                };
                //анонсируем сущьность для подписчиков на событие
                EventAggregator.GetEvent<AddAlarmEvent>().Publish(_alarm);
                // закрываем модальное окно
                DialogVisible = false;
            }catch(ArgumentNullException ex)
            {
                Notify.Message($"{ex.ParamName} : {ex.Message}");
            }
        }

        /// <summary>
        /// открываем диалоговое окно с настройкой будильника
        /// </summary>
        /// <param name="obj"></param>
        private void OpenDialogAlarm(AlarmModel obj)
        {
            if (obj != null)
            {
                Alarm = obj;
                DialogVisible = true;
            }
        }
        /// <summary>
        /// отобразить окно настройки будильника?
        /// </summary>
        /// <param name="obj"></param>
        private void VisibleAddAlarmModal(bool obj)
        {
            if (obj == true)
            {
                Alarm = new();
                DialogVisible = true;
            }
        }
        /// <summary>
        /// команнда показа модального окна с настройками будильника
        /// </summary>
        protected override void AlarmsView()
        {
            IsVisibility = IsVisibility == true ? false : true;
        }
        /// <summary>
        /// дествия при запуске формы
        /// </summary>
        /// <param name="navigationContext"></param>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            CommandCore.AlarmsViewCommand.RegisterCommand(AlarmViewCommand);
            CommandCore.SaveAlarmCommand.RegisterCommand(SaveAlarmCommand);
            base.OnNavigatedTo(navigationContext);
        }

        /// <summary>
        /// действия при закрытии формы 
        /// </summary>
        /// <param name="navigationContext"></param>
        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            CommandCore.AlarmsViewCommand.UnregisterCommand(AlarmViewCommand);
            CommandCore.SaveAlarmCommand.UnregisterCommand(SaveAlarmCommand);
        }


    }
}
