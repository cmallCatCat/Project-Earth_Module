﻿using System;
using System.Collections.Generic;

namespace Core.Root.Utilities
{
    public static class TypeUtility
    {
        public static Type[] GetAllBaseTypes(this Type type)
        {
            List<Type> types = new List<Type>();
            Type derived = type;
            do
            {
                derived = derived.BaseType;
                if (derived != null)
                    types.Add(derived);
            } while (derived != null);
            return types.ToArray();
        }
    }
}