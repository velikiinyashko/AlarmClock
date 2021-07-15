using AlarmClock.Modules.ModuleAlarm.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using AlarmClock.Core;

namespace AlarmClock.Modules.ModuleAlarm
{
    public class ModuleAlarmModule : IModule
    {
        private IRegionManager _regionManager;
        public ModuleAlarmModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RequestNavigate(RegionNames.AlarmsRegion, "ViewAlarm");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ViewAlarm>("ViewAlarm");
        }
    }
}