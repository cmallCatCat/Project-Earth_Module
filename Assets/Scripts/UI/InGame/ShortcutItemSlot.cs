using Core.Inventory_And_Item.Data;
using InventoryAndItem.Core.Inventory_And_Item.Data;
using MeadowGames.MakeItFlow;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.InGame
{
    public class ShortcutItemSlot : MonoBehaviour, IDropHandler
    {
        private int index;
        private Inventory inventory;
        private ItemSlot itemSlot;
        public GameObject itemStackPrefab;

        public void OnDrop(PointerEventData eventData)
        {
            ShortcutItemStack stack = eventData.pointerDrag.GetComponent<ShortcutItemStack>();
            stack.parentAfterDrag = transform;
            stack.GetComponent<TranslateToTargetBehavior>().TargetPosition = transform;
            inventory.MergeOrSwitch(stack.index, index, false);
            // Destroy(eventData.pointerDrag);
        }

        public void Init(int index, Inventory inventory)
        {
            this.index = index;
            this.inventory = inventory;
            itemSlot = inventory.GetSlot(index);
        }

        public void Refresh()
        {
            if (itemSlot.ItemStack != null && transform.childCount == 0)
            {
                GameObject instantiate = Instantiate(itemStackPrefab, transform);
                instantiate.GetComponent<ShortcutItemStack>()
                    .Init(itemSlot.ItemStack, index, GetComponent<RectTransform>().sizeDelta);
            }

            if (itemSlot.ItemStack == null && transform.childCount > 0)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    Destroy(transform.GetChild(i).gameObject);
                }
            }
        }
    }
}