using System.Collections.Generic;
using System.Net;

namespace Deviot.Common
{
    public interface INotifier
    {
        bool HasNotifications { get; }

        IEnumerable<Notify> GetNotifications();

        void Notify(HttpStatusCode httpStatusCode, string message);

        void Clear();
    }
}
