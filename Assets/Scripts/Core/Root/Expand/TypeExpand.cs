using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Root.Expand
{
    public static class TypeExpand
    {
        public static IEnumerable<Type> GetImplements(this Type type, Type[] types)
        {
            return types.Where(t =>
                type.IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface && !t.IsGenericType);
        }

        public static IEnumerable<Type> GetSubClasses(this Type type, Type[] types)
        {
            return types.Where(t =>
                t.IsSubclassOf(type) && !t.IsAbstract && !t.IsGenericType);
        }
    }
}