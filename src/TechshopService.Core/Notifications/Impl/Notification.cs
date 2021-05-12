using System;
using System.Collections.Generic;

namespace TechshopService.Core.Notifications
{
    public class Notification : INotification
    {
        private readonly List<(string, int)> _notifications = new();

        public void Add(string message, int statusCode = 400) =>
            _notifications.Add((message, statusCode));

        public void Add(Exception exception, int statusCode = 500) =>
            _notifications.Add((exception.Message, statusCode));

        public bool Any() => _notifications.Count > 0;
    }
}
