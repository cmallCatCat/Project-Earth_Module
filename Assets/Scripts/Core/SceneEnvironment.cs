using System;
using System.Collections.Generic;
using Core.Inventory_And_Item.Data.ItemIdentifications.ItemEffects;
using UnityEngine;

namespace Core
{
    public class SceneEnvironment : MonoBehaviour, IEnvironment
    {
        public static SceneEnvironment Instance { get; private set; }

        private GameObject player;

        private Dictionary<Type, List<MonoBehaviour>> data;

        public GameObject Player
        {
            get
            {
                if (player == null)
                {
                    player = GameObject.Find("Player");
                }

                return player;
            }
        }

        public void Instantiate(GameObject toCreate, bool useObjectPool, Vector2 getPosition, Quaternion getRotation)
        {
            GameObject.Instantiate(toCreate, getPosition, getRotation).name = toCreate.name;
        }

        public void Register<T>(MonoBehaviour monoBehaviour)
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

        public void Unregister<T>(MonoBehaviour monoBehaviour)
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

        private void Awake()
        {
            Instance = this;
        }
    }
}