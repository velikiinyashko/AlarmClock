using System;
using System.Windows;
using System.Windows.Controls;
using AlarmClock.Core.Event;
using Prism.Events;
using AlarmClock.Core.Models;

namespace AlarmClock.Modules.ModuleAlarm.Views
{
    /// <summary>
    /// Interaction logic for ViewAlarm
    /// </summary>
    public partial class ViewAlarm : UserControl
    {
        private IEventAggregator _eventAggregator;
        public ViewAlarm(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            InitializeComponent();
        }

        private void Chip_DeleteClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var i = (MaterialDesignThemes.Wpf.Chip)sender;
            _eventAggregator.GetEvent<DeleteAlarmEvent>().Publish(((AlarmModel)i.DataContext).Id);
        }

        private void Chip_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var i = (MaterialDesignThemes.Wpf.Chip)sender;
            _eventAggregator.GetEvent<OpenDialogAlarmEvent>().Publish((AlarmModel)i.DataContext);

        }

        private void Chip_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MessageBox.Show("double click");

        }
    }
}
