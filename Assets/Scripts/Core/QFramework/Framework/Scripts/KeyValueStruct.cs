using System;
using System.Collections.Generic;
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

    [Serializable]
    public struct StringListStruct<T>
    {
        [SerializeField] public string Key;
        [SerializeReference] public List<T> Value;
        
        public StringListStruct(string key, List<T> value)
        {
            Key = key;
            Value = value;
        }
        
        public void Deconstruct(out string key, out List<T> value)
        {
            key = Key;
            value = Value;
        }
    }
}