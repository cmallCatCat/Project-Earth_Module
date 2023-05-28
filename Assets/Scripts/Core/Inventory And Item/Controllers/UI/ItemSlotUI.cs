using InventoryAndItem.Core.Inventory_And_Item.Command;
using InventoryAndItem.Core.Inventory_And_Item.Data;
using QFramework;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InventoryAndItem.Core.Inventory_And_Item.Controllers.UI
{
    public abstract class ItemSlotUI : MonoBehaviour, IController, IPointerClickHandler
    {
        public int index;
        public Inventory inventory;
        public ItemSlot itemSlot;


        public ItemSlotUI Init(int index, Inventory inventory)
        {
            this.index = index;
            this.inventory = inventory;
            itemSlot = inventory.GetSlot(index);
            return this;
        }

        public void OnChildRemoved()
        {
            inventory.Remove(index);
        }

        public void Refresh()
        {
            if (transform.childCount == 0 && itemSlot.ItemStack != null)
            {
                ItemStackUI itemStackUI = ItemStackCreator.Instance.Create(itemSlot.ItemStack, transform,
                    transform.GetComponent<RectTransform>().sizeDelta);
                itemStackUI.OnRemoved += OnChildRemoved;
            }

            if (transform.childCount != 0 && itemSlot.ItemStack == null)
            {
                Destroy(transform.GetChild(0).gameObject);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (PointerStack.Instance.itemOnHold)
            {
                this.SendCommand(new TryDropStackOnSlotCommand(this));
            }
        }

        public abstract IArchitecture GetArchitecture();
    }
}