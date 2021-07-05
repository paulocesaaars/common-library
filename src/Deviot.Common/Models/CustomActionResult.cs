using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Deviot.Common.Models
{
    [ExcludeFromCodeCoverage]

    public class CustomActionResult
    {
        public IEnumerable<string> Messages { get; private set; }

        public object Data { get; private set; }

        public CustomActionResult()
        {

        }

        public CustomActionResult(string message, object data)
        {
            Messages = new List<string>(1) { message };
            Data = data;
        }

        public CustomActionResult(IEnumerable<string> message, object data)
        {
            Messages = message;
            Data = data;
        }
    }
}
