using System;
using UnityEngine;

namespace InventoryAndItem.Core.Inventory_And_Item.Data.ItemIdentifications.ItemEffects
{
    public abstract class IEnvironment : MonoBehaviour
    {

        private Camera uiCamera;
        
        private GameObject player;

        protected abstract Camera GetUICamera { get; }
        
        protected abstract GameObject GetPlayer { get; }

        public Camera UiCamera
        {
            get
            {
                if (uiCamera == null)
                {
                    uiCamera = GetUICamera;
                }
                return uiCamera;
            }
        }
        
        public GameObject Player
        {
            get
            {
                if (player == null)
                {
                    player = GetPlayer;
                }

                return player;
            }
        }

        public abstract void Instantiate(GameObject toCreate, bool useObjectPool, Vector2 getPosition, Quaternion getRotation); 
        
        public abstract void Register<T>(MonoBehaviour monoBehaviour);
        
        public abstract void Unregister<T>(MonoBehaviour monoBehaviour);

        public abstract void Delay<T>(Action<T> action, T args, float delay);
    }
}
