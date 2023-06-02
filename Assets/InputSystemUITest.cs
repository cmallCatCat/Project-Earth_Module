using UnityEngine;
using UnityEngine.EventSystems;

public class InputSystemUITest : MonoBehaviour , IPointerClickHandler, IScrollHandler
{

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(eventData.button);
    }

    public void OnScroll(PointerEventData eventData)
    {
        Debug.Log(eventData.scrollDelta);
    }
}
