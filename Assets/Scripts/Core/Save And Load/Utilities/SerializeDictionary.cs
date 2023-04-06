using System.Collections.Generic;
using UnityEngine;

namespace Core.Save_And_Load.Utilities
{
    public static class SerializeDictionary
    {
        public static string DicToJson<TKey, TValue>(Dictionary<TKey, TValue> dic)
        {
            return JsonUtility.ToJson(new SerializationDictionary<TKey, TValue>(dic));
        }

        public static Dictionary<TKey, TValue> DicFromJson<TKey, TValue>(string str)
        {
            return JsonUtility.FromJson<SerializationDictionary<TKey, TValue>>(str).ToDictionary();
        }   
    }
}