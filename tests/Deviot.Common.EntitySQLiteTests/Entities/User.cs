using Deviot.Common.Entities;
using System;

namespace Deviot.Common.EntitySQLiteTests.Entities
{
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
