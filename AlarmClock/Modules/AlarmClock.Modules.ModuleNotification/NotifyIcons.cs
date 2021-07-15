using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlarmClock.Services.Interfaces;
using System.Windows.Forms;
using System.Drawing;

namespace AlarmClock.Modules.ModuleNotification
{
    public class NotifyIcons : INotifyIcon
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
    }
}
