using System;
using System.Collections.Generic;
using System.Linq;
using Core.Save_And_Load.Event;
using Core.Save_And_Load.Utilities;
using Framework;
using UnityEngine;

namespace Core.Save_And_Load.Systems
{
    public class GameObjectSaveSystem : AbstractSystem
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
            foreach (GameObject gameObject in gameObjects)
            {
                string key = gameObject.GetInstanceID().ToString();
                if (SaveGameObject(gameObject, key))
                    keys.Add(key);
            }

            saveUtility.SaveList(keys.ToList(), "gameObjectKeys");
        }

        private bool SaveGameObject(GameObject gameObject, string key)
        {
            ISave[] components = gameObject.GetComponents<ISave>();
            if (components.Length == 0)
            {
                return false;
            }

            List<string> types = components.Select(save => save.GetType().AssemblyQualifiedName).ToList();
            List<object> data = components.Select(save => save.Save()).ToList();
            saveUtility.SaveList(types, key + "_types");
            saveUtility.SaveList(data, key + "_data", unity: false);
            return true;
        }

        private void OnLoad(LoadEvent obj)
        {
            List<string> keys = saveUtility.LoadList<string>("gameObjectKeys");
            foreach (string key in keys)
            {
                List<string> componentsB = saveUtility.LoadList<string>(key + "_types");
                List<object> data = saveUtility.LoadList<object>(key + "_data", unity: false);
                GameObject gameObject = new GameObject();
                List<Type> components = new List<Type>(componentsB.Count);
                for (int i = 0; i < componentsB.Count; i++)
                {
                    Type type = Type.GetType(componentsB[i]);
                    components.Add(type);
                }

                for (int i = 0; i < components.Count; i++)
                {
                    ISave save = (ISave)gameObject.AddComponent(components[i]);
                    save.Load(data[i]);
                }
            }
        }
    }
}