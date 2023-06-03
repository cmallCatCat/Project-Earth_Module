using Core.Root.Base;
using UnityEngine;

namespace Core.Inventory_And_Item.Controllers
{
    public class FollowPointer : MonoBehaviour
    {
        private Camera uiCamera;

        private void Awake()
        {
            uiCamera = IEnvironment.Instance.UiCamera;
        }

        private void Update()
        {
            Vector3 screenToWorldPoint = uiCamera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(screenToWorldPoint.x, screenToWorldPoint.y, 0);
        }
    }
}