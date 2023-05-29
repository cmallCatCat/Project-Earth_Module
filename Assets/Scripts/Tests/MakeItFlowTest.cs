using UnityEngine;
using UnityEngine.EventSystems;

namespace Tests
{
    public class MakeItFlowTest : MonoBehaviour, IDragHandler
    {
        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Camera.main.ScreenToWorldPoint(eventData.position);
        }
    }
}