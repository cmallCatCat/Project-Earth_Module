using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Inventory_And_Item.Data.ItemIdentifications.ItemEffects
{
    public interface IEnvironment
    {
        GameObject Player { get; }
        void Instantiate(GameObject toCreate, bool useObjectPool, Vector2 getPosition, Quaternion getRotation);
        void Register<T>(MonoBehaviour monoBehaviour);
        void Unregister<T>(MonoBehaviour monoBehaviour);

        void Delay<T>(Action<T> action, T args, float delay);
    }
}