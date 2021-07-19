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
            // запуск окна
            ThemeManager.Current.ChangeTheme(this, "Dark.Blue");
            return Container.Resolve<MainWindow>();
        }

        /// <summary>
        /// события прои закрытии приложения
        /// </summary>
        /// <param name="e"></param>
        protected override void OnExit(ExitEventArgs e)
        {            
            base.OnExit(e);
        }
        /// <summary>
        /// регистрируем зависимости в проекте
        /// </summary>
        /// <param name="containerRegistry"></param>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
            containerRegistry.RegisterSingleton<INotifyIcon, NotifyIcons>();
            containerRegistry.RegisterScoped<ITimeClockManagerService, TimeClockManagerService>();
            containerRegistry.RegisterSingleton<ICommandCore, CommandCore>();
        }
        /// <summary>
        /// загрузка модулей
        /// </summary>
        /// <param name="moduleCatalog">каталог модулей</param>
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {

            moduleCatalog.AddModule(GetModuleInfoOnStart(typeof(ModuleAlarmModule)));
            moduleCatalog.AddModule(GetModuleInfoOnStart(typeof(ViewModulesModule)));
            moduleCatalog.AddModule(GetModuleInfo(typeof(ModuleSettingsModule)));
        }

        /// <summary>
        /// метод отложенной загрузки модулей
        /// </summary>
        /// <param name="Module"></param>
        /// <returns></returns>
        private ModuleInfo GetModuleInfo(Type Module) => new()
        {
            ModuleName = Module.Name,
            ModuleType = Module.AssemblyQualifiedName,
            InitializationMode = InitializationMode.OnDemand
        };
        /// <summary>
        /// метод загрузки модулей при запуске
        /// </summary>
        /// <param name="Module"></param>
        /// <returns></returns>
        private ModuleInfo GetModuleInfoOnStart(Type Module) => new()
        {
            ModuleName = Module.Name,
            ModuleType = Module.AssemblyQualifiedName,
            InitializationMode = InitializationMode.WhenAvailable
        };
    }
}
