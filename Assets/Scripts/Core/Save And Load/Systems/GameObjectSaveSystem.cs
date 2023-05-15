using System;
using System.Collections.Generic;
using System.Linq;
using Core.QFramework;
using Core.Save_And_Load.Event;
using Core.Save_And_Load.Interfaces;
using Core.Save_And_Load.Utilities;
using QFramework;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

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
            GameObject[] gameObjects = Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
            List<string> keys = new List<string>();
            foreach (GameObject gameObject in gameObjects)
            {
                //是否需要储存
                string key = gameObject.GetInstanceID().ToString();
                if (!gameObject.scene.isLoaded || !gameObject.CompareTag("Player"))
                {
                    continue;
                }

                Debug.Log(gameObject.name);
                saveUtility.SaveObject(gameObject, key + POSTFIX);
                keys.Add(key);
            }

            saveUtility.SaveList(keys.ToList(), "gameObjectKeys");
        }

        private void OnLoad(LoadEvent obj)
        {
            List<string> keys = saveUtility.LoadList<string>("gameObjectKeys");
            foreach (string key in keys)
            {
                saveUtility.LoadObject<GameObject>(key + POSTFIX, null);
            }
        }
    }
}