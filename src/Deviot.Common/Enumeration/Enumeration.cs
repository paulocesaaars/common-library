﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Deviot.Common
{
    public abstract class Enumeration : IComparable
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        private const string APPLICATION_EXCEPTION_MESSAGE = "'{0}' is not a valid {1} in {2}";
        private const string VALUE = "value";
        private const string DISPLAY_NAME = "display name";

        protected Enumeration()
        {

        }

        protected Enumeration(int id, string name)
        {
            Id = id;
            Name = name;
        }

        private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem is null)
            {
                var message = string.Format(APPLICATION_EXCEPTION_MESSAGE, value, description, typeof(T));
                throw new ApplicationException(message);
            }

            return matchingItem;
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
                    throw new InvalidOperationException();

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

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }

        public static T FromId<T>(int id) where T : Enumeration, new()
        {
            var matchingItem = Parse<T, int>(id, VALUE, item => item.Id.Equals(id));
            return matchingItem;
        }

        public static T FromName<T>(string name) where T : Enumeration, new()
        {
            var matchingItem = Parse<T, string>(name, DISPLAY_NAME, item => item.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            return matchingItem;
        }

        public static T FromIdOrDefault<T>(int id, T enumerationDefault) where T : Enumeration, new()
        {
            try
            {
                return FromId<T>(id);
            }
            catch(Exception)
            {
                return enumerationDefault;
            }
        }

        public static T FromNameOrDefault<T>(string name, T enumerationDefault) where T : Enumeration, new()
        {
            try
            {
                return FromName<T>(name);
            }
            catch (Exception)
            {
                return enumerationDefault;
            }
        }
    }
}