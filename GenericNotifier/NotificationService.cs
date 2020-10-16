using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericNotifier
{
    public abstract class NotificationService
    {
        public int Interval
        {
            get;
            protected set;
        }

        public long TotalTimePassed
        {
            get;
            set;
        }

        public bool Running
        {
            get;
            set;
        }

        public NotificationService()
        {
            Interval = 10000;
        }

        public abstract void Update();

        public void SendNotification(string message)
        {
            /*ToastContent content = new ToastContentBuilder()
                .AddText(message)
                .GetToastContent();

            var toast = new ToastNotification(content.GetXml());

            DesktopNotificationManagerCompat.CreateToastNotifier().Show(toast);*/

            Program.TrayIcon.BalloonTipText = message;
            Program.TrayIcon.ShowBalloonTip(3000);
        }
    }
}
