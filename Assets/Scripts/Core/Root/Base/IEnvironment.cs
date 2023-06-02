using System;
using QFramework;
using UnityEngine;

namespace Core.Root
{
    public abstract class IEnvironment : MonoSingleton<IEnvironment>
    {
        public readonly Vector3 FarAway = new Vector3(9999f, 9999f, 9999f);

        private Camera mainCamera;
        
        private Camera uiCamera;

        private GameObject player;

        protected abstract Camera GetUICamera { get; }

        protected abstract GameObject GetPlayer { get; }


        public Camera MainCamera
        {
            get
            {
                if (mainCamera == null)
                {
                    mainCamera = Camera.main;
                }

                return mainCamera;
            }
        }

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

        public abstract Canvas UICanvas { get; }

        public abstract void Instantiate(GameObject toCreate, bool useObjectPool, Vector2 getPosition, Quaternion getRotation);

        public abstract void Register<T>(MonoBehaviour monoBehaviour);

        public abstract void Unregister<T>(MonoBehaviour monoBehaviour);

        public abstract void Delay<T>(Action<T> action, T args, float delay);
    }
}