using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GenericNotifier
{
    public class InternetNotifier : NotificationService
    {
        private readonly WebClient _web;
        private bool _isConnected;


        public InternetNotifier()
        {
            Interval = 1000;
            _isConnected = false;

            _web = new WebClient();
        }

        public override void Update()
        {
            string text = _web.DownloadString("http://www.msftconnecttest.com/connecttest.txt");

            if (!_isConnected && text == "Microsoft Connect Test")
            {
                SendNotification("Internet Connected!");
                _isConnected = true;
            } else if (_isConnected && text != "Microsoft Connect Test")
            {
                SendNotification("Internet Disconnected!");
                _isConnected = false;
            }
        }
    }
}
