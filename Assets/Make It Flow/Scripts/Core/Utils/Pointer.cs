using Make_It_Flow.Scripts.Core.Event_System.Input_Manager;
using UnityEngine;

namespace Make_It_Flow.Scripts.Core.Utils
{
    public class Pointer : MonoBehaviour, IUpdateEvent
    {
        CanvasManager _canvasManager;
        
        void Awake()
        {
            _canvasManager = transform.GetComponentInParent<CanvasManager>();
        }

        void OnEnable()
        {
            MFSystemManager.AddToUpdate(this);
        }
        void OnDisable()
        {
            MFSystemManager.RemoveFromUpdate(this);
        }

        void Start()
        {
            Cursor.visible = false;
        }

        public void OnUpdate()
        {
            transform.position = InputManager.Instance.GetCanvasPointerPosition(_canvasManager);
        }
    }
}