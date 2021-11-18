using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Deviot.Common
{
    public sealed class Notifier : INotifier
    {
        private readonly List<Notify> _notifies;

        public bool HasNotifications => _notifies.Any();

        public Notifier()
        {
            _notifies = new List<Notify>();
        }

        private void AddNotification(Notify notify)
        {
            _notifies.Add(notify);
        }

        public IEnumerable<Notify> GetNotifications() => _notifies;

        public void Notify(HttpStatusCode httpStatusCode, string message)
        {
            var notify = new Notify(httpStatusCode, message);
            AddNotification(notify);
        }

        public void NotifyCreated(string id, string message)
        {
            var notify = new Notify(id, HttpStatusCode.Created, message);
            AddNotification(notify);
        }

        public void Clear() => _notifies.Clear();
    }
}
