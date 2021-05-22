using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Deviot.Common.Enumerators
{
    public abstract class Enumeration : IComparable
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        protected Enumeration(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString() => Name;

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            var fields = typeof(T).GetFields(BindingFlags.Public |
                                             BindingFlags.Static |
                                             BindingFlags.DeclaredOnly);


            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        public override bool Equals(object obj)
        {
            try
            {
                var otherValue = obj as Enumeration;

                if (otherValue == null)
                    return false;

                var typeMatches = GetType().Equals(obj.GetType());
                var valueMatches = Name.Equals(otherValue.Name);

                return typeMatches && valueMatches;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public int CompareTo(object other) => Name.CompareTo(((Enumeration)other).Name);
    }
}