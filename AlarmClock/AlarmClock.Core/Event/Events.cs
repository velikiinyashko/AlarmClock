using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;
using AlarmClock.Core.Models;

namespace AlarmClock.Core.Event
{
    /// <summary>
    /// отображение модально
    /// </summary>
    public class VisibleAddAlarmModalEvent: PubSubEvent<bool> { }

    /// <summary>
    /// собитие добавления будильника
    /// </summary>
    public class AddAlarmEvent : PubSubEvent<AlarmModel> { }

    /// <summary>
    /// собитие для удаление будильника
    /// </summary>
    public class DeleteAlarmEvent : PubSubEvent<Guid> { }

    /// <summary>
    /// событие тика тайтера 
    /// </summary>
    public class TimeTickEvent : PubSubEvent<string> { }

    /// <summary>
    /// Изменение настроек будильника
    /// </summary>
    public class OpenDialogAlarmEvent : PubSubEvent<AlarmModel> { }

    /// <summary>
    /// выключение будильника
    /// </summary>
    public class DisableAlarmEvent : PubSubEvent<bool> { }

    public class EnableAlarmEvent : PubSubEvent<AlarmModel> { }

}
