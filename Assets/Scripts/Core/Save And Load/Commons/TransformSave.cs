using Core.Save_And_Load.Expansions;
using Core.Save_And_Load.Interfaces;
using Newtonsoft.Json.Linq;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;
using Vector4 = System.Numerics.Vector4;

namespace Core.Save_And_Load.Commons
{
    public class TransformSave : MonoBehaviour, ISave
    {
        public object Save()
        {
            return new TransformData(transform);
        }

        public void Load(object save)
        {
            ((JObject)save).ToObject<TransformData>().Deconstruct(transform);
        }

        public struct TransformData
        {
            public Vector3 position;
            public Vector4 rotation;
            public Vector3 scale;
            public string name;

            public TransformData(Transform transform)
            {
                (Vector3 vector3, Vector4 vector4, Vector3 s) = transform.Save();
                position = vector3;
                rotation = vector4;
                scale = s;
                name = transform.name;
            }

            public void Deconstruct(Transform transform)
            {
                transform.Set(position, rotation, scale);
                transform.name = name;
            }
        }
    }
}