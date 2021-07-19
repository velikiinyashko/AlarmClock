using AlarmClock.Services;
using AlarmClock.Modules.ModuleNotification;
using AlarmClock.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;
using AlarmClock.Modules.ModuleSettings;
using System;
using ControlzEx.Theming;
using AlarmClock.Modules.ViewModules;
using AlarmClock.Modules.ModuleAlarm;
using AlarmClock.Core.CommandModule;
using AlarmClock.Core.Interfaces;
using AlarmClock.Core.Services;

namespace AlarmClock
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            ThemeManager.Current.ChangeTheme(this, "Dark.Orange");
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<INotifyIcon, NotifyIcons>();
            containerRegistry.RegisterScoped<ITimeClockManagerService, TimeClockManagerService>();
            containerRegistry.RegisterSingleton<ICommandCore, CommandCore>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule(GetModuleInfoOnStart(typeof(ModuleAlarmModule)));
            moduleCatalog.AddModule(GetModuleInfoOnStart(typeof(ViewModulesModule)));
            moduleCatalog.AddModule(GetModuleInfo(typeof(ModuleSettingsModule)));
        }

        private ModuleInfo GetModuleInfo(Type Module) => new()
        {
            ModuleName = Module.Name,
            ModuleType = Module.AssemblyQualifiedName,
            InitializationMode = InitializationMode.OnDemand
        };

        private ModuleInfo GetModuleInfoOnStart(Type Module) => new()
        {
            ModuleName = Module.Name,
            ModuleType = Module.AssemblyQualifiedName,
            InitializationMode = InitializationMode.WhenAvailable
        };
    }
}
