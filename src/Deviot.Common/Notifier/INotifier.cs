using System.Collections.Generic;

namespace Deviot.Common
{
    public interface INotifier
    {
        bool HasNotifications(NotificationTypeEnum? filter = null);

        bool HasErrorsNotifications();

        IEnumerable<Notify> GetNotifications(NotificationTypeEnum? filter = null);

        void NotifySuccess(string message);

        void NotifyValidationError(string message);

        void NotifyInternalError(string message);

        void NotifyExternalError(string message);
    }
}
