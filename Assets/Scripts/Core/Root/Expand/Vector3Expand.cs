using UnityEngine;

namespace Core.Root.Expand
{
    public static class VectorExpand
    {
        public static bool InDistance(this Vector3 v1, Vector3 v2,float distance)
        {
            return  (v1-v2).sqrMagnitude < distance*distance;
        }
    }
}