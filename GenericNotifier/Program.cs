using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenericNotifier
{
    public static class Program
    {
        public static NotifyIcon TrayIcon;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            // Register AUMID and COM server (for MSIX/sparse package apps, this no-ops)
            //DesktopNotificationManagerCompat.RegisterAumidAndComServer<MyNotificationActivator>("HumanGamer.GenericNotifier");

            // Register COM server and activator type
            //DesktopNotificationManagerCompat.RegisterActivator<MyNotificationActivator>();

            NotificationRegistry.InitServices();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            TrayIcon = new NotifyIcon();
            TrayIcon.Text = "Generic Notifier";
            TrayIcon.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            TrayIcon.Visible = true;

            System.Timers.Timer timer = new System.Timers.Timer(100);
            timer.AutoReset = true;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            while (true)
                Thread.Sleep(1);
        }

        private static async void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            NotificationRegistry.UpdateServices();
        }
    }
}
