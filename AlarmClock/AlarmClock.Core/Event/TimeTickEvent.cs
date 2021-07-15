using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;

namespace AlarmClock.Core.Event
{
    public class TimeTickEvent : PubSubEvent<string> { }
}
