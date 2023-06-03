using System;
using QFramework;
using UnityEngine;

namespace Core.Root.Base
{
    public abstract class IEnvironment : MonoSingleton<IEnvironment>
    {
        public readonly Vector3 FarAway = new Vector3(9999f, 9999f, 9999f);

        private static Camera     mainCamera;
        private static Camera     uiCamera;
        private static Canvas     canvas;
        private static GameObject player;


        public abstract GameObject Instantiate(GameObject toCreate, bool useObjectPool, 
            Vector2 getPosition, Quaternion getRotation);

        public abstract void Delay<T>(Action<T> action, T args, float delay);


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
                    uiCamera = UIRoot.Instance.UICamera;
                }

                return uiCamera;
            }
        }

        public static Canvas Canvas
        {
            get
            {
                if (canvas == null)
                {
                    canvas = UIRoot.Instance.Canvas;
                }
                return canvas;
            }
        }


        public static GameObject Player
        {
            get
            {
                if (player == null)
                {
                    player = GameObject.FindGameObjectWithTag("Player");
                }

                return player;
            }
        }
    }
}