using System.Collections.Generic;
using UnityEngine;

namespace Core.Root
{
    public static class ListExpand
    {
        public static void Log<T>(this List<T> list)
        {
            Debug.Log("--List: " + list + "length:" + list.Count);
            foreach (T item in list)
            {
                Debug.Log(item);
            }
        }
    }
}