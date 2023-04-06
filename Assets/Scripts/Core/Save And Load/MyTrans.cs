using UnityEngine;
using Vector3 = System.Numerics.Vector3;
using Vector4 = System.Numerics.Vector4;

namespace Core.Save_And_Load
{
    public static class MyTrans
    {
        public static Vector3 Save(this UnityEngine.Vector3 vector3)
            => new Vector3(vector3.x, vector3.y, vector3.z);

        public static Vector4 Save(this Quaternion quaternion)
            => new Vector4(quaternion.x, quaternion.y, quaternion.z, quaternion.w);

        public static (Vector3, Vector4, Vector3) Save(this Transform transform)
            => (transform.position.Save(), transform.rotation.Save(), transform.localScale.Save());

        public static void Set(this Transform transform, Vector3 position, Vector4 rotation, Vector3 scale)
        {
            transform.position = new UnityEngine.Vector3(position.X, position.Y, position.Z);
            transform.rotation = new Quaternion(rotation.X, rotation.Y, rotation.Z, rotation.W);
            transform.localScale = new UnityEngine.Vector3(scale.X, scale.Y, scale.Z);
        }
    }
}