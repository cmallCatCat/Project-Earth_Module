using System;
using UnityEngine;

namespace Core.QFramework
{
    [Serializable]
    public struct KeyValueStruct<S, T>
    {
        [SerializeField] public S Key;
        [SerializeField] public T Value;

        public KeyValueStruct(S key, T value)
        {
            Key = key;
            Value = value;
        }

        public void Deconstruct(out S key, out T value)
        {
            key = Key;
            value = Value;
        }
    }
}