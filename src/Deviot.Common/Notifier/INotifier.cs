using System.Collections.Generic;
using System.Net;

namespace Deviot.Common
{
    public interface INotifier
    {
        bool HasNotifications { get; }

        IEnumerable<Notify> GetNotifications();

        void Notify(HttpStatusCode httpStatusCode, string message);

        void Notify(string id, HttpStatusCode httpStatusCode, string message);

        void Clear();
    }
}
