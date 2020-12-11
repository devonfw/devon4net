using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AOP.ErrorHandler.Core.Common.Enum
{
    public abstract class EnumBaseType<T> where T : EnumBaseType<T>
    {
        protected static List<T> EnumValues = new List<T>();

        public readonly int Key;
        public readonly string Value;

        protected EnumBaseType(int key, string value)
        {
            Key = key;
            Value = value;
            EnumValues.Add((T) this);
        }

        protected static ReadOnlyCollection<T> GetBaseValues()
        {
            return EnumValues.AsReadOnly();
        }

        protected static T GetBaseByKey(int key)
        {
            return EnumValues.FirstOrDefault(t => t.Key == key);
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
