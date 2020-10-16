using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericNotifier
{
    public static class NotificationRegistry
    {
        public static int Count => _notificationService.Count;

        private static readonly Dictionary<string, NotificationService> _notificationService = new Dictionary<string, NotificationService>();

        public static void RegisterService(string id, NotificationService service)
        {
            if (ContainsService(id))
                return;

            _notificationService.Add(id, service);
        }

        public static void UnregisterService(string id)
        {
            if (ContainsService(id))
                _notificationService.Remove(id);
        }

        public static NotificationService GetService(string id)
        {
            if (ContainsService(id))
                return _notificationService[id];

            return null;
        }

        public static bool ContainsService(string id)
        {
            return _notificationService.ContainsKey(id);
        }

        private static DateTime lastTime;

        public static void UpdateServices()
        {
            DateTime startTime = DateTime.Now;
            TimeSpan timePassed = startTime - lastTime;

            foreach (var servicePair in _notificationService)
            {
                var service = servicePair.Value;
                if (service.Running)
                    continue;

                service.TotalTimePassed += (long)timePassed.TotalMilliseconds;

                if (service.TotalTimePassed >= service.Interval)
                {
                    service.Running = true;
                    service.Update();
                    service.TotalTimePassed = 0;
                    service.Running = false;
                }
            }

            lastTime = startTime;
        }

        public static void InitServices()
        {
            RegisterService("internet", new InternetNotifier());
        }
    }
}
