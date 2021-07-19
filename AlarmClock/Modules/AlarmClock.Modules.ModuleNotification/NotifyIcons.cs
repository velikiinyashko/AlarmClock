using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using AlarmClock.Core.Interfaces;

namespace AlarmClock.Modules.ModuleNotification
{
    public class NotifyIcons : IDisposable, INotifyIcon
    {
        private NotifyIcon _notify = new();
        /// <summary>
        /// конструктор создания индикатора в трее
        /// </summary>
        public NotifyIcons()
        {
            // подписываемся на события закрытия прилжения
            System.Windows.Application.Current.Exit += ExitEvent;
            #region создание иконки в трее
            _notify.Icon = new($"{Environment.CurrentDirectory}\\Icons\\Icon.ico");
            _notify.Visible = true;
            _notify.Text = "Будильник";
            _notify.ContextMenuStrip = new();
            _notify.ContextMenuStrip.Items.Add("Exit",null, ExitEvent);
            #endregion
        }
        /// <summary>
        /// событие для закрытия приложения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitEvent(object sender, EventArgs e)
        {
            Dispose();
            Environment.Exit(0);
    
        }
        /// <summary>
        /// сообщение в трее
        /// </summary>
        /// <param name="Message">текст сообщения</param>
        public void Message(string Message)
        {
            _notify.ShowBalloonTip(10000, "Будильник", Message, ToolTipIcon.Info);           
        }

        /// <summary>
        /// метод очистки и высвобождения ресурсов при закрытии
        /// </summary>
        public void Dispose()
        {
            _notify.Visible = false;
            _notify.Dispose();
        }
    }
}
