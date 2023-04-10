using System;
using System.Collections.Generic;
using System.Linq;
using Core.QFramework;
using Core.Save_And_Load.Event;
using Core.Save_And_Load.Interfaces;
using Core.Save_And_Load.Utilities;
using QFramework;
using UnityEngine;

namespace Core.Save_And_Load.Systems
{
    public class GameObjectSaveSystem : AbstractSystem
    {
        private const string POSTFIX = "_data";
        private SaveUtility saveUtility;

        protected override void OnInit()
        {
            saveUtility = this.GetUtility<SaveUtility>();
            this.RegisterEvent<LoadEvent>(OnLoad);
            this.RegisterEvent<SaveEvent>(OnSave);
        }

        private void OnSave(SaveEvent obj)
        {
            List<GameObject> gameObjects = Resources.FindObjectsOfTypeAll(typeof(GameObject)).Cast<GameObject>().ToList();
            List<string> keys = new List<string>();
            foreach (GameObject gameObject in gameObjects)
            {
                //是否需要储存
                string key = gameObject.GetInstanceID().ToString();
                ISave[] components = gameObject.GetComponents<ISave>();
                if (components.Length == 0)
                {
                    continue;
                }

                //需要储存
                List<KeyValueStruct<string,object>> data = components.Select
                    (save => new KeyValueStruct<string,object>(save.GetType().AssemblyQualifiedName, save.Save())).ToList();
                saveUtility.SaveList(data, key + POSTFIX, unity: false);
                keys.Add(key);
            }

            saveUtility.SaveList(keys.ToList(), "gameObjectKeys");
        }

        private void OnLoad(LoadEvent obj)
        {
            List<string> keys = saveUtility.LoadList<string>("gameObjectKeys");
            foreach (string key in keys)
            {
                GameObject gameObject = new GameObject();
                // AddComponent
                List<KeyValueStruct<string,object>> myStructs = saveUtility.LoadList<KeyValueStruct<string,object>>(key + POSTFIX, unity: false);
                foreach (KeyValueStruct<string,object> t in myStructs)
                {
                    (string typeString, object data) = t;
                    Type type = Type.GetType(typeString);
                    ISave save = (ISave)gameObject.AddComponent(type);
                    save.Load(data);
                }
            }
        }
    }
}