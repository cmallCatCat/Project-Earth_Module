using System;
using System.Collections;
using System.Collections.Generic;
using InventoryAndItem.Core.Inventory_And_Item.Data;
using InventoryAndItem.Core.Inventory_And_Item.Data.ItemInfos.ItemEffects;
using UnityEngine;

namespace Core
{
    public class SceneEnvironment : IEnvironment
    {
        public static SceneEnvironment Instance { get; private set; }
        
        private Dictionary<Type, List<MonoBehaviour>> data = new Dictionary<Type, List<MonoBehaviour>>();

        protected override Camera GetUICamera => GameObject.Find("UICamera").GetComponent<Camera>();

        protected override GameObject GetPlayer => GameObject.FindGameObjectWithTag("Player");

        public override void Instantiate(GameObject toCreate, bool useObjectPool, Vector2 getPosition, Quaternion getRotation)
        {
            GameObject.Instantiate(toCreate, getPosition, getRotation).name = toCreate.name;
        }

        public override void Register<T>(MonoBehaviour monoBehaviour)
        {
            if (data.TryGetValue(typeof(T), out List<MonoBehaviour> list))
            {
                list.Add(monoBehaviour);
            }
            else
            {
                data.Add(typeof(T), new List<MonoBehaviour> { monoBehaviour });
            }
        }

        public override void Unregister<T>(MonoBehaviour monoBehaviour)
        {
            if (data.TryGetValue(typeof(T), out List<MonoBehaviour> list))
            {
                bool remove = list.Remove(monoBehaviour);
                if (!remove)
                {
                    Debug.LogError($"{monoBehaviour} is not registered, can't unregister it.");
                }
            }
            else
            {
                Debug.LogError($"{monoBehaviour} is not registered, can't unregister it.");
            }
        }


        public override void Delay<T>(Action<T> action, T args, float delay)
        {
            // TODO: Implement
            StartCoroutine(Do(action, args, delay));
        }

        public IEnumerator Do<T>(Action<T> action, T args, float delay)
        {
            yield return new WaitForSeconds(delay);
            action.Invoke(args);
        }

        private void Awake()
        {
            Instance = this;
            ItemDatabaseHandler.Init(QAssetBundle.New_item_database_asset.NEW_ITEM_DATABASE);
        }
    }
}