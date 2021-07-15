using AlarmClock.Services.Interfaces;
using System.Windows.Threading;
using AlarmClock.Core;
using Prism.Events;
using System.Threading.Tasks;
using AlarmClock.Core.Event;
using System;

namespace AlarmClock.Services
{
    public class TimeClockManagerService : ITimeClockManagerService
    {
        private DispatcherTimer _dispatcherTimer;
        private IEventAggregator _eventAggregator;

        public TimeClockManagerService(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            GetTime();
        }

        private void GetTime()
        {
            Task.Run(() =>
            {
                while (true)
                    _eventAggregator.GetEvent<TimeTickEvent>().Publish(DateTime.Now.ToString("HH:mm:ss"));
            });
        }
    }
}
