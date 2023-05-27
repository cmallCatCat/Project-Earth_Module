using InventoryAndItem.Core.Inventory_And_Item.Data;
using QFramework;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace UI.InGame
{
    public class ItemStackUI : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [Header("UI")]
        public Image stackImage;

        public Image icon;
        public TMP_Text count;

        [HideInInspector]
        public Transform parentAfterDrag;

        public int index;

        private void Awake()
        {
            parentAfterDrag = transform.parent;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            stackImage.raycastTarget = false;
            parentAfterDrag = transform.parent;
            GetComponent<TranslateToTargetBehavior>().TargetPosition = transform.parent;
            transform.SetParent(transform.parent.parent.parent.parent);
        }

        public void OnDrag(PointerEventData eventData)
        {
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            stackImage.raycastTarget = true;
            transform.SetParent(parentAfterDrag);
        }

        public void Init(ItemStack itemSlotItemStack, int i, Vector2 sizeDelta)
        {
            icon.sprite = itemSlotItemStack.ItemInfo.SpriteIcon;
            count.text = itemSlotItemStack.Number.ToString();
            index = i;
            GetComponent<RectTransform>().sizeDelta = sizeDelta;
        }
        
    }
}