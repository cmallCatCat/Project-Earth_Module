using UnityEngine;

namespace Core.Root.Expand
{
    public static class TransformExpand
    {
        public static T GetComponentInCounterparts<T>(this Transform transform)
        {
            return transform.parent.GetComponentInChildren<T>();
        }
    }
}