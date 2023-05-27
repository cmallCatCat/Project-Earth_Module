using InventoryAndItem.Core.Inventory_And_Item.Data;
using QFramework;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.InGame
{
    public class ItemSlotUI : MonoBehaviour, IDropHandler, IController
    {
        private int index;
        private Inventory inventory;
        private ItemSlot itemSlot;
        public GameObject itemStackPrefab;

        public void OnDrop(PointerEventData eventData)
        {
            ItemStackUI stackUI = eventData.pointerDrag.GetComponent<ItemStackUI>();
            stackUI.parentAfterDrag = transform;
            stackUI.GetComponent<TranslateToTargetBehavior>().TargetPosition = transform;
            inventory.MergeOrSwitch(stackUI.index, index, false);
            // this.SendCommand<MergeOrSwitchCommand>(eventData.pointerDrag,stackUI.index,index);
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
                instantiate.GetComponent<ItemStackUI>()
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

        public IArchitecture GetArchitecture()
        {
            return Core.Architectures.InGame.Interface;
        }
    }
}