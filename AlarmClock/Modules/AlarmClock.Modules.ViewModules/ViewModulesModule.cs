using AlarmClock.Modules.ViewModules.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using AlarmClock.Core;

namespace AlarmClock.Modules.ViewModules
{
    public class ViewModulesModule : IModule
    {
        private IRegionManager _regionManager;
        public ViewModulesModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;

        }
        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, "Main");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainViewWindow>("Main");
        }
    }
}