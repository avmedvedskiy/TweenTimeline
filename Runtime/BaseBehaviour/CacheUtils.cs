using System;
using System.Collections.Generic;
using System.Reflection;

namespace Timeline.Move
{
    internal static class CacheUtils
    {
        private static Dictionary<object, List<Tuple<PropertyInfo, object>>> _cachedItems = new();

        private static bool Has(object item)
        {
            return _cachedItems.ContainsKey(item);
        }

        public static void Remove(object item)
        {
            _cachedItems.Remove(item);
        }

        public static void Save(object item)
        {
            if(Has(item))
                return;
            
            var props = new List<Tuple<PropertyInfo, object>>();
            foreach (var prop in item.GetType().GetProperties())
            {
                if (prop.CanRead && prop.CanWrite)
                {
                    props.Add(new Tuple<PropertyInfo, object>(prop,prop.GetValue(item)));
                }
            }
            _cachedItems.Add(item,props);
        }

        public static void Restore(object item)
        {
            if (Has(item))
            {
                var list = _cachedItems[item];
                foreach (var cachedProp in _cachedItems[item])
                {
                    cachedProp.Item1.SetValue(item,cachedProp.Item2);
                }
            }
        }
    }
}