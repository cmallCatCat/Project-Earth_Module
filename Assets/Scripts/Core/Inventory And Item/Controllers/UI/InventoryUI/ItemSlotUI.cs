using InventoryAndItem.Core.Inventory_And_Item.Command;
using InventoryAndItem.Core.Inventory_And_Item.Controllers.UI.InventoryUI.Singles;
using InventoryAndItem.Core.Inventory_And_Item.Data;
using QFramework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InventoryAndItem.Core.Inventory_And_Item.Controllers.UI.InventoryUI
{
    public abstract class ItemSlotUI : MonoBehaviour, IController, IPointerClickHandler
    {
        public InventoryUIPanel inventoryUIPanel;

        public int displayIndex;

        public Inventory Inventory => inventoryUIPanel.inventory;

        public ItemSlot ItemSlot => Inventory.GetSlot(InventoryIndex);

        public int InventoryIndex => displayIndex + inventoryUIPanel.inventoryUISetting.startIndex;

        public RectTransform RectTransform;

        public ItemStackUI itemStackUI;

        private Image backgroundImage;

        public ItemSlotUI Init(InventoryUIPanel inventoryUIPanel, int displayIndex)
        {
            this.inventoryUIPanel = inventoryUIPanel;
            this.displayIndex     = displayIndex;
            SetLayout();
            return this;
        }

        private void SetLayout()
        {
            RectTransform.sizeDelta = inventoryUIPanel.inventoryUISetting.slotSize;
            backgroundImage.sprite  = inventoryUIPanel.inventoryUISetting.slotDefaultBackground;
            backgroundImage.type    = Image.Type.Sliced;
        }

        public void Refresh()
        {
            if (transform.childCount == 0 && ItemSlot.ItemStack != null) itemStackUI = ItemStackCreator.Instance.Create(this);

            InventoryUISetting setting = inventoryUIPanel.inventoryUISetting;
            backgroundImage.sprite = inventoryUIPanel.IsSelected(displayIndex)
                ? setting.slotSelectedBackground
                : setting.slotBackground.TryGetValue(displayIndex, out Sprite value)
                    ? value
                    : setting.slotDefaultBackground;

            if (transform.childCount != 0 && ItemSlot.ItemStack == null)
            {
                for (int i = 0; i < transform.childCount; i++) DestroyImmediate(transform.GetChild(i).gameObject);

                itemStackUI = null;
            }

            if (itemStackUI != null) itemStackUI.Refresh();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (inventoryUIPanel.SelectableNow)
                {
                    this.SendCommand(new SelectItemCommand(this));
                }
                else
                {
                    if (PointerStack.Instance.itemOnHold)
                    {
                        if (itemStackUI == null)
                            this.SendCommand(new TryDropStackOnEmptySlotCommand(this));
                        else
                            this.SendCommand(new TryDropStackOnNonemptyCommand(this));
                    }
                    else
                    {
                        if (itemStackUI != null) this.SendCommand(new TryHoldStackCommand(this));
                    }
                }
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (!inventoryUIPanel.SelectableNow && !PointerStack.Instance.itemOnHold && itemStackUI != null)
                    this.SendCommand(new TrySplitStackCommand(this));
            }
        }

        private void Awake()
        {
            backgroundImage = GetComponent<Image>();
            RectTransform   = GetComponent<RectTransform>();
        }

        public abstract IArchitecture GetArchitecture();
    }

    public class TrySplitStackCommand : AbstractCommand
    {
        private readonly ItemSlotUI itemSlotUI;

        public TrySplitStackCommand(ItemSlotUI itemSlotUI)
        {
            this.itemSlotUI = itemSlotUI;
        }

        protected override void OnExecute()
        {
            ItemStack stack = itemSlotUI.ItemSlot.ItemStack;
            if (stack != null)
            {
                ItemStack removed = new ItemStack(stack.ItemInfo, stack.ItemDecorator,
                    Mathf.CeilToInt(stack.Number / 2.0f), stack.Transform);
                itemSlotUI.Inventory.Remove(removed, itemSlotUI.InventoryIndex);
                PointerStack.Instance.Create(removed);
            }
        }
    }
}