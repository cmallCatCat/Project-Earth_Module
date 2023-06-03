using System;
using System.Collections;
using System.Collections.Generic;
using Core.Inventory_And_Item.Data;
using Core.Root.Base;
using QFramework;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core
{
    public class SceneEnvironment : IEnvironment
    {
        private Dictionary<Type, List<MonoBehaviour>> data = new Dictionary<Type, List<MonoBehaviour>>();
        
        public override GameObject Instantiate(GameObject toCreate, bool useObjectPool, Vector2 getPosition,
            Quaternion                                    getRotation)
        {
            GameObject instantiate = Object.Instantiate(toCreate, getPosition, getRotation);
            instantiate.name = toCreate.name;
            return instantiate;
        }

        public override void Delay<T>(Action<T> action, T args, float delay)
        {
            StartCoroutine(Do(action, args, delay));
        }

        public IEnumerator Do<T>(Action<T> action, T args, float delay)
        {
            yield return new WaitForSeconds(delay);
            action.Invoke(args);
        }

        private void Awake()
        {
            ResKit.Init();
            ItemDatabaseHandler.Instance.Init(QAssetBundle.New_item_database_asset.NEW_ITEM_DATABASE);
        }
    }
}