using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Deviot.Common
{
    public sealed class Notifier : INotifier
    {
        private List<Notify> _notifies;

        private ILogger _logger;

        private const string ERROR_MESSAGE = "Ultrapassou o limite de notificações";

        public bool HasNotifications => _notifies.Any();

        public Notifier(ILogger<Notifier> logger)
        {
            _notifies = new List<Notify>(10);
            _logger = logger;
        }

        private void AddNotification(Notify notify)
        {
            if (_logger is not null && _notifies.Count == 10)
                _logger.LogError(ERROR_MESSAGE);
            else
                _notifies.Add(notify);
        }

        private void Logger(Notify notify)
        {
            if(_logger is not null)
                _logger.LogInformation(notify.Message);
        }

        public IEnumerable<Notify> GetNotifications() => _notifies;

        public void Notify(HttpStatusCode httpStatusCode, string message)
        {
            var notify = new Notify(httpStatusCode, message);
            AddNotification(notify);
            Logger(notify);
        }
    }
}
