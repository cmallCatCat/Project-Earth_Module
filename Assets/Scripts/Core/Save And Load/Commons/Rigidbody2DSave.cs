using System;
using Core.Save_And_Load.Interfaces;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Core.Save_And_Load.Commons
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Rigidbody2DSave : MonoBehaviour, ISave
    {
        public object Save()
        {
            return new Rigidbody2DData(GetComponent<Rigidbody2D>());
        }

        public void Load(object save)
        {
            ((JObject)save).ToObject<Rigidbody2DData>().Deconstruct(GetComponent<Rigidbody2D>());
        }

        public struct Rigidbody2DData
        {
            public float mass;
            public RigidbodyConstraints2D rigidbody2DConstraints;
            public float rigidbody2DDrag;
            public float rigidbody2DInertia;
            public RigidbodyInterpolation2D rigidbody2DInterpolation;
            public bool rigidbody2DSimulated;
            public float rigidbody2DAngularDrag;
            public RigidbodyType2D rigidbody2DBodyType;
            public bool rigidbody2DFreezeRotation;
            public float rigidbody2DGravityScale;
            public bool rigidbody2DIsKinematic;

            public Rigidbody2DData(Rigidbody2D rigidbody2D)
            {
                mass = rigidbody2D.mass;
                rigidbody2DConstraints = rigidbody2D.constraints;
                rigidbody2DDrag = rigidbody2D.drag;
                rigidbody2DInertia = rigidbody2D.inertia;
                rigidbody2DInterpolation = rigidbody2D.interpolation;
                rigidbody2DSimulated = rigidbody2D.simulated;
                rigidbody2DAngularDrag = rigidbody2D.angularDrag;
                rigidbody2DBodyType = rigidbody2D.bodyType;
                rigidbody2DFreezeRotation = rigidbody2D.freezeRotation;
                rigidbody2DGravityScale = rigidbody2D.gravityScale;
                rigidbody2DIsKinematic = rigidbody2D.isKinematic;
            }

            public void Deconstruct(Rigidbody2D rigidbody2D)
            {
                rigidbody2D.mass = mass;
                rigidbody2D.constraints = rigidbody2DConstraints;
                rigidbody2D.drag = rigidbody2DDrag;
                rigidbody2D.inertia = rigidbody2DInertia;
                rigidbody2D.interpolation = rigidbody2DInterpolation;
                rigidbody2D.simulated = rigidbody2DSimulated;
                rigidbody2D.angularDrag = rigidbody2DAngularDrag;
                rigidbody2D.bodyType = rigidbody2DBodyType;
                rigidbody2D.freezeRotation = rigidbody2DFreezeRotation;
                rigidbody2D.gravityScale = rigidbody2DGravityScale;
                rigidbody2D.isKinematic = rigidbody2DIsKinematic;
            }
        }
    }
}