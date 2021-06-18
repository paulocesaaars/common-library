using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Deviot.Common
{
    public sealed class Notifier : INotifier
    {
        private List<Notify> _messages;

        private ILogger _logger;

        private const string ERROR_MESSAGE = "Ultrapassou o limite de notificações";

        public Notifier(ILogger<Notifier> logger)
        {
            _messages = new List<Notify>(10);
            _logger = logger;
        }

        private void AddNotification(Notify notify)
        {
            if (_logger is not null && _messages.Count == 10)
                _logger.LogError(ERROR_MESSAGE);
            else
                _messages.Add(notify);
        }

        private void Logger(Notify notify)
        {
            if(_logger is not null)
            {
                if (notify.Type == NotificationTypeEnum.ValidationError)
                    _logger.LogWarning(notify.Message);
                else if (notify.Type == NotificationTypeEnum.InternalError)
                    _logger.LogError(notify.Message);
                else if (notify.Type == NotificationTypeEnum.ExternalError)
                    _logger.LogError(notify.Message);
                else
                    _logger.LogInformation(notify.Message);
            }
        }

        public void NotifySuccess(string message)
        {
            var notify = new Notify(NotificationTypeEnum.Success, message);
            AddNotification(notify);
            Logger(notify);
        }

        public void NotifyValidationError(string message)
        {
            var notify = new Notify(NotificationTypeEnum.ValidationError, message);
            AddNotification(notify);
            Logger(notify);
        }

        public void NotifyInternalError(string message)
        {
            var notify = new Notify(NotificationTypeEnum.InternalError, message);
            AddNotification(notify);
            Logger(notify);
        }

        public void NotifyExternalError(string message)
        {
            var notify = new Notify(NotificationTypeEnum.ExternalError, message);
            AddNotification(notify);
            Logger(notify);
        }

        public bool HasNotifications(NotificationTypeEnum? filter = null)
        {
            if (filter is null)
                return _messages.Any();
            else
                return _messages.Any(x => x.Type == filter);
        }

        public bool HasErrorsNotifications()
        {
            return _messages.Any(x => x.Type != NotificationTypeEnum.Success);
        }

        public IEnumerable<Notify> GetNotifications(NotificationTypeEnum? filter = null)
        {
            if (filter is null)
                return _messages;
            else
                return _messages.Where(x => x.Type == filter);
        }
    }
}
