using System;
using System.Collections;
using System.Collections.Generic;
using Core.Inventory_And_Item.Data.ItemIdentifications.ItemEffects;
using UnityEngine;

namespace Core
{
    public class SceneEnvironment : MonoBehaviour, IEnvironment
    {
        public static SceneEnvironment Instance { get; private set; }

        private GameObject player;

        private Dictionary<Type, List<MonoBehaviour>> data=new Dictionary<Type, List<MonoBehaviour>>();

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

        
        public void Delay<T>(Action<T> action, T args, float delay)
        {
            // TODO: Implement
            StartCoroutine(Do(action, args,delay));
            
        }

        public IEnumerator Do<T>(Action<T> action, T args, float delay)
        {
            yield return new WaitForSeconds(delay);
            action.Invoke(args);
        }

        private void Awake()
        {
            Instance = this;
        }
    }
}