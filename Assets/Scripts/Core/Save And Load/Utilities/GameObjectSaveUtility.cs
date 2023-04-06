using System;
using System.Collections.Generic;
using System.Linq;
using Core.Save_And_Load.Event;
using Framework;
using UnityEngine;

namespace Core.Save_And_Load.Utilities
{
    public class GameObjectSaveUtility : AbstractSystem
    {
        private SaveUtility saveUtility;
        private List<GameObject> gameObjects;

        protected override void OnInit()
        {
            saveUtility = this.GetUtility<SaveUtility>();
            this.RegisterEvent<LoadEvent>(OnLoad);
            this.RegisterEvent<SaveEvent>(OnSave);
        }

        private void OnSave(SaveEvent obj)
        {
            gameObjects = Resources.FindObjectsOfTypeAll(typeof(GameObject)).Cast<GameObject>().ToList();
            List<string> keys = new List<string>();
            for (int index = 0; index < gameObjects.Count; index++)
            {
                GameObject gameObject = gameObjects[index];
                string key = gameObject.GetInstanceID().ToString();
                ISave[] components = gameObject.GetComponents<ISave>();
                if (components.Length == 0)
                {
                    continue;
                }

                List<string> types = components.Select(save => save.GetType().FullName).ToList();
                List<object> data = components.Select(save => save.Save()).ToList();
                keys.Add(key);
                saveUtility.SaveList(types, key + "_types");
                saveUtility.SaveList(data, key + "_data", unity: false);
            }

            saveUtility.SaveList(keys.ToList(), "gameObjectKeys");
        }

        private void OnLoad(LoadEvent obj)
        {
            List<string> keys = saveUtility.LoadList<string>("gameObjectKeys");
            foreach (string key in keys)
            {
                GameObject gameObject = new GameObject();
                List<string> componentsB =
                    saveUtility.LoadList<string>(key + "_types");
                List<Type> components = new List<Type>(componentsB.Count);
                for (int i = 0; i < componentsB.Count; i++)
                {
                    Type type = Type.GetType(componentsB[i]+", Assembly-CSharp");
                    components.Add(type);
                }
                List<object> data = saveUtility.LoadList<object>(key + "_data", unity: false);
                for (int i = 0; i < components.Count; i++)
                {
                    ISave save = (ISave)gameObject.AddComponent(components[i]);
                    save.Load(data[i]);
                }
            }
        }
    }
}