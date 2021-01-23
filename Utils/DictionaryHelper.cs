using System;
using System.Collections.Generic;

namespace GameNode
{
    internal static class DictionaryHelper
    {
        internal static R Find<R, T>(Dictionary<Type, T> map) where R : T
        {
            return (R) Find(map, typeof(R));
        }

        internal static T Find<T>(Dictionary<Type, T> map, Type requiredType)
        {
            if (map.ContainsKey(requiredType))
            {
                return map[requiredType];
            }

            var keys = map.Keys;
            foreach (var key in keys)
            {
                if (requiredType.IsAssignableFrom(key))
                {
                    return map[key];
                }
            }

            throw new Exception("Value is not found!");
        }

        internal static bool TryFind<T>(Dictionary<Type, T> map, Type requiredType, out T item)
        {
            if (map.ContainsKey(requiredType))
            {
                item =  map[requiredType];
                return true;
            }

            var keys = map.Keys;
            foreach (var key in keys)
            {
                if (requiredType.IsAssignableFrom(key))
                {
                    item = map[key];
                    return true;
                }
            }

            item = default;
            return false;
        }
    }
}