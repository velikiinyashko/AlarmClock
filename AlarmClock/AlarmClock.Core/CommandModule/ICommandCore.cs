using Prism.Commands;

namespace AlarmClock.Core.CommandModule
{
    public interface ICommandCore
    {
        CompositeCommand AlarmsViewCommand { get; }
        CompositeCommand AlaramAddCommand { get; }
    }
}
