using System;
using UnityEngine;

namespace Core.Save_And_Load.Utilities
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

        public override string ToString()
        {
            return $"{nameof(Key)}: {Key}, {nameof(Value)}: {Value}";
        }
    }
}