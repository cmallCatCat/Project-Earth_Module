#nullable enable
using InventoryAndItem.Core.Inventory_And_Item.Controllers.UI;
using InventoryAndItem.Core.Inventory_And_Item.Data;
using InventoryAndItem.Core.Inventory_And_Item.Data.ItemInfos.ItemEffects;
using InventoryAndItem.Core.Inventory_And_Item.Events;
using QFramework;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InventoryAndItem.Core.Inventory_And_Item.Command
{
    public class HoldStackEndCommand : AbstractCommand
    {
        public ItemStackUI stackUI;
        public PointerEventData pointerEventData;


        public HoldStackEndCommand(ItemStackUI stackUI, PointerEventData pointerEventData)
        {
            this.stackUI = stackUI;
            this.pointerEventData = pointerEventData;
        }

        protected override void OnExecute()
        {
            // InventoryUIPanel.itemOnHold = false;
            // ItemSlotUI.initializePosition = stackUI.transform.position;
            //
            // ItemSlotUI toSlot = pointerEventData.hovered.Find(o =>
            //     o.name == "ItemSlot(Clone)" && stackUI.slot.gameObject != o).GetComponent<ItemSlotUI>();
            // if (toSlot == null)
            // {
            //     this.SendEvent<ReplaceStackUIEvent>();
            // }
            // else
            // {
            //     ItemStack? onHand = toSlot.inventory.MergeOrSwitch(stackUI.itemStack, toSlot.index);
            //     if (onHand != null)
            //     {
            //         InventoryUIPanel.itemOnHold = true;
            //         ItemStackUI newStackUI = ItemStackUI.Create(onHand, InventoryUIPanel.OnHold, stackUI.transform.position,
            //             stackUI.GetComponent<RectTransform>().sizeDelta);
            //
            //         this.SendCommand(new HoldStackStartCommand(newStackUI));
            //     }
            //
            //     Object.Destroy(stackUI.gameObject);
            // }
            //
            // ItemSlotUI.initializePosition = null;
        }
    }

    public class TryDropStackOnStackCommand : AbstractCommand
    {
        public ItemStackUI itemStackUI;

        public TryDropStackOnStackCommand(ItemStackUI itemStackUI)
        {
            this.itemStackUI = itemStackUI;
        }

        protected override void OnExecute()
        {
        }
    }
}