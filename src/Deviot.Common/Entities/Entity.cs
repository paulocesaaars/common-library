using System;
using System.Diagnostics.CodeAnalysis;

namespace Deviot.Common.Entities
{
    [ExcludeFromCodeCoverage]
    public class Entity
    {
        public Guid Id { get; protected set; }

        public Entity()
        {
            Id = Guid.NewGuid();
        }

        public Entity(Guid id)
        {
            Id = id;
        }
    }
}
