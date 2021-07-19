using Prism.Commands;

namespace AlarmClock.Core.CommandModule
{
    public class CommandCore : ICommandCore
    {
        public CompositeCommand AlarmsViewCommand { get; } = new();
        public CompositeCommand AlaramAddCommand { get; } = new();
        public CompositeCommand DeleteAlarmCommand { get; } = new();
    }
}
