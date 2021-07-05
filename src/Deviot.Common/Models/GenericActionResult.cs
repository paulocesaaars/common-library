using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Deviot.Common.Models
{
    [ExcludeFromCodeCoverage]

    public class GenericActionResult<T>
    {
        public IEnumerable<string> Messages { get; set; }

        public T Data { get; set; }

        public GenericActionResult()
        {

        }

        public GenericActionResult(IEnumerable<string> messages, T data)
        {
            Messages = messages;
            Data = data;
        }
    }
}
