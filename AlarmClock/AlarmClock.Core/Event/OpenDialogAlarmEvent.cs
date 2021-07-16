using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;
using AlarmClock.Core.Models;

namespace AlarmClock.Core.Event
{
    public class OpenDialogAlarmEvent : PubSubEvent<AlarmModel> { }
}
