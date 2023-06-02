using UnityEngine;

namespace Core.Root.Expand
{
    public static class GameObjectExpand
    {
        public static T GetComponentInCounterparts<T>(this GameObject gameObject)
        {
            return gameObject.transform.parent.GetComponentInChildren<T>();
        }
    }
}