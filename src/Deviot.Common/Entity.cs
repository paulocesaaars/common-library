using System;

namespace Deviot.Common
{
    public class Entity<T>
    {
        public T Id { get; protected set; }

        public Entity()
        {
            var type = this.GetType();
            var property = type.GetProperty($"Id");

            if(property.PropertyType == typeof(Guid))
                property.SetValue(this, Guid.NewGuid());
        }

        public Entity(T id)
        {
            Id = id;
        }
    }
}
