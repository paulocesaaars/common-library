using System;
using System.Diagnostics.CodeAnalysis;

namespace Deviot.Common.EntitySQLiteTests.Entities
{
    [ExcludeFromCodeCoverage]
    public class User : Entity
    {
        public string Name { get; private set; }

        public User(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
