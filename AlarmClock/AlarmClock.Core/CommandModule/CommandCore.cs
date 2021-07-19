using Prism.Commands;

namespace AlarmClock.Core.CommandModule
{
    public class CommandCore : ICommandCore
    {
        /// <summary>
        /// отображение настроек будильника
        /// </summary>
        public CompositeCommand AlarmsViewCommand { get; } = new();
        /// <summary>
        /// добавление будильника
        /// </summary>
        public CompositeCommand AlaramAddCommand { get; } = new();
        /// <summary>
        /// удаление будильника
        /// </summary>
        public CompositeCommand DeleteAlarmCommand { get; } = new();
        /// <summary>
        /// сохранение будильника
        /// </summary>
        public CompositeCommand SaveAlarmCommand { get; } = new();
    }
}
