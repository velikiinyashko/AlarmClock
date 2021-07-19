using AlarmClock.Modules.ModuleSettings.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using AlarmClock.Core;

namespace AlarmClock.Modules.ModuleSettings
{

    public class ModuleSettingsModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public ModuleSettingsModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, "Settings");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Settings>();
        }
    }
}