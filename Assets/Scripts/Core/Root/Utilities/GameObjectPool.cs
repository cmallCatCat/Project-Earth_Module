using System.Collections.Generic;
using UnityEngine;

namespace Core.Root.Utilities
{
    public class GameObjectPool
    {
        private GameObject        prefab;           // 预制体，用于实例化新对象
        private Stack<GameObject> availableObjects; // 存储可用的对象

        public GameObjectPool(GameObject prefab)
        {
            this.prefab      = prefab;
            availableObjects = new Stack<GameObject>();
        }

        // 从对象池中获取一个对象，如果没有可用的对象则实例化一个新对象
        public GameObject GetObject()
        {
            if (availableObjects.Count == 0)
            {
                CreateNewObject();
            }
        
            GameObject obj = availableObjects.Pop();
            obj.SetActive(true);
            return obj;
        }

        // 将对象归还到对象池
        public void ReturnObject(GameObject obj)
        {
            obj.SetActive(false);
            availableObjects.Push(obj);
        }

        // 实例化一个新对象并将其添加到对象池
        private void CreateNewObject()
        {
            GameObject newObj = Object.Instantiate(prefab);
            newObj.SetActive(false);
            availableObjects.Push(newObj);
        }
    }
}