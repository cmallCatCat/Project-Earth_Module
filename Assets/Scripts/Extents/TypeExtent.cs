using System;
using System.Collections.Generic;
using System.Linq;

namespace Extents
{
    public static class TypeExtent
    {
        public static IEnumerable<Type> GetImplements(this Type type, Type[] types)
        {
            return types.Where(t => t.IsAssignableFrom(type));
        }

        public static IEnumerable<Type> GetSubClasses(this Type type, Type[] types)
        {
            return types.Where(t => t.IsSubclassOf(type));
        }
    }
}