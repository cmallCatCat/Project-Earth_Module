using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Save_And_Load.DictionaryAndList
{
    [Serializable]
    public class SerializationDictionary<TKey, TValue> : ISerializationCallbackReceiver
    {
        [SerializeField] private List<TKey> keys;

        [SerializeField] private List<TValue> values;

        private Dictionary<TKey, TValue> target;

        public SerializationDictionary(Dictionary<TKey, TValue> target)
        {
            this.target = target;
        }

        public void OnBeforeSerialize()
        {
            keys = new List<TKey>(target.Keys);
            values = new List<TValue>(target.Values);
        }

        public void OnAfterDeserialize()
        {
            int count = Math.Min(keys.Count, values.Count);
            target = new Dictionary<TKey, TValue>(count);
            for (int i = 0; i < count; ++i) target.Add(keys[i], values[i]);
        }

        public Dictionary<TKey, TValue> ToDictionary()
        {
            return target;
        }
    }
}