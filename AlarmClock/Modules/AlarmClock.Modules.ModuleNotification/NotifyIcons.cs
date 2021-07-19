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

        public NotifyIcons()
        {
            _notify.Icon = new($"{Environment.CurrentDirectory}\\Icons\\Icon.ico");
            _notify.Visible = true;
            _notify.Text = "Alarm Motherfucker";
            _notify.ContextMenuStrip = new();
            _notify.ContextMenuStrip.Items.Add("Exit",null, null);
        }

        public void Message(string Message)
        {
            _notify.ShowBalloonTip(2000, "Будильник", Message, ToolTipIcon.Info);           
        }

        public void Exit()
        {
            Dispose();
        }

        public void Dispose()
        {
            _notify.Visible = false;
            _notify.Dispose();
        }
    }
}
