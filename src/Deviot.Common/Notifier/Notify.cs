using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Deviot.Common
{
    public class Notify
    {
        [ExcludeFromCodeCoverage]
        public HttpStatusCode Type { get; private set; }

        public string Message { get; private set; }

        public Notify(HttpStatusCode type, string message)
        {
            Type = type;
            Message = message;
        }
    }
}
