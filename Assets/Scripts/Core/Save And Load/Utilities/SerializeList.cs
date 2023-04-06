using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Core.Save_And_Load.Utilities
{
    public static class SerializeList
    {
        public static string ListToJson<T>(List<T> l, bool unity)
        {
            return unity ? JsonUtility.ToJson(new SerializationList<T>(l)) : JsonConvert.SerializeObject(l);
        }

        public static List<T> ListFromJson<T>(string str, bool unity)
        {
            return unity ? JsonUtility.FromJson<SerializationList<T>>(str).ToList() : JsonConvert.DeserializeObject<List<T>>(str);
        }
    }
}