using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Core.Save_And_Load.Utilities
{
    public class JsonSaveUtility : SaveUtility
    {
        public void CreateDirectoryIfNotExist(string path)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        }

        #region Save

        public override void SaveDictionary<S, T>(Dictionary<S, T> dictionary, string path,bool unity = true)
        {
            List<KeyValueStruct<S, T>> dictionaryToList = DictionaryTransList.DictionaryToList(dictionary);
            string json = SerializeList.ListToJson(dictionaryToList, unity);
            SaveString(json, path);
        }

        public override void SaveList<S>(List<S> list, string path, bool unity = true)
        {
            string json = SerializeList.ListToJson(list, unity);
            SaveString(json, path);
        }
        

        public override void SaveString(string toSave, string path)
        {
            CreateDirectoryIfNotExist(Application.persistentDataPath + "/strings");
            File.WriteAllText($"{Application.persistentDataPath}/strings/{path}.json", toSave);
        }

        public override void SaveInt(int value, string path)
        {
            CreateDirectoryIfNotExist(Application.persistentDataPath + "/ints");
            File.WriteAllText($"{Application.persistentDataPath}/ints/{path}.json", value.ToString());
        }

        public override void SaveObject<T>(T obj, string path)
        {
            CreateDirectoryIfNotExist(Application.persistentDataPath + "/objects");
            File.WriteAllText($"{Application.persistentDataPath}/objects/{path}.json", JsonUtility.ToJson(obj));
        }

        #endregion


        #region Load

        public override Dictionary<S, T> LoadDictionary<S, T>(string path, Dictionary<S, T> defaultValue = null, bool unity = true)
        {
            if (!File.Exists($"{Application.persistentDataPath}/strings/{path}.json")) return defaultValue;

            string json = File.ReadAllText($"{Application.persistentDataPath}/strings/{path}.json");
            List<KeyValueStruct<S, T>> list = SerializeList.ListFromJson<KeyValueStruct<S, T>>(json, unity);
            Dictionary<S, T> dictionary = DictionaryTransList.ListToDictionary(list);
            return dictionary;
        }

        public override List<S> LoadList<S>(string path, bool unity = true)
        {
            if (!File.Exists($"{Application.persistentDataPath}/strings/{path}.json")) return new List<S>();

            string json = File.ReadAllText($"{Application.persistentDataPath}/strings/{path}.json");
            List<S> listFromJson = SerializeList.ListFromJson<S>(json, unity);
            return listFromJson;
        }

        public override string LoadString(string path, string defaultValue = "")
        {
            if (!File.Exists($"{Application.persistentDataPath}/strings/{path}.json")) return defaultValue;

            return File.ReadAllText($"{Application.persistentDataPath}/strings/{path}.json");
        }

        public override int LoadInt(string path, int defaultValue = 0)
        {
            if (!File.Exists($"{Application.persistentDataPath}/ints/{path}.json")) return defaultValue;

            return int.Parse(File.ReadAllText($"{Application.persistentDataPath}/ints/{path}.json"));
        }

        public override T LoadObject<T>(string path, T defaultValue = default)
        {
            if (!File.Exists($"{Application.persistentDataPath}/objects/{path}.json"))
            {
                return defaultValue;
            }

            return JsonUtility.FromJson<T>(File.ReadAllText($"{Application.persistentDataPath}/objects/{path}.json"));
        }

        #endregion
    }
}