using System.Diagnostics.CodeAnalysis;

namespace Deviot.Common
{
    public class Notify
    {
        [ExcludeFromCodeCoverage]
        public NotificationTypeEnum Type { get; private set; }

        public string Message { get; private set; }

        public Notify(NotificationTypeEnum type, string message)
        {
            Type = type;
            Message = message;
        }
    }
}
