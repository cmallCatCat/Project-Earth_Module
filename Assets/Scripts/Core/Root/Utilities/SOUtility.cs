#nullable enable
using System;
using UnityEngine;

namespace Core.Root.Utilities
{
    public static class SOHelper
    {
        public static T? CloneScriptableObject<T>(T original) where T : ScriptableObject
        {
            if (ReferenceEquals(original, null)) throw new Exception("original ScriptableObject is null");

            string json = JsonUtility.ToJson(original);
            return JsonToScriptableObject<T>(json);
        }

        public static T? JsonToScriptableObject<T>(string json) where T : ScriptableObject
        {
            if (string.IsNullOrEmpty(json)) return null;

            T scriptableObject = ScriptableObject.CreateInstance<T>();
            JsonUtility.FromJsonOverwrite(json, scriptableObject);
            return scriptableObject;
        }

        public static ScriptableObject? JsonToScriptableObject(string json, Type type)
        {
            if (string.IsNullOrEmpty(json)) return null;

            ScriptableObject scriptableObject = ScriptableObject.CreateInstance(type);
            JsonUtility.FromJsonOverwrite(json, scriptableObject);
            return scriptableObject;
        }
    }
}