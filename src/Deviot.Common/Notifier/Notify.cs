using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Deviot.Common
{
    [ExcludeFromCodeCoverage]
    public class Notify
    {
        public string Id { get; private set; }

        public HttpStatusCode Type { get; private set; }

        public string Message { get; private set; }

        public Notify(HttpStatusCode type, string message)
        {
            Id = Guid.NewGuid().ToString();
            Type = type;
            Message = message;
        }

        public Notify(string id, HttpStatusCode type, string message)
        {
            Id = id;
            Type = type;
            Message = message;
        }
    }
}
