using System;
using InventoryAndItem.Core.Inventory_And_Item.Command;
using InventoryAndItem.Core.Inventory_And_Item.Controllers.UI.InventoryUI.Singles;
using InventoryAndItem.Core.Inventory_And_Item.Data;
using JetBrains.Annotations;
using QFramework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
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
            this.displayIndex = displayIndex;
            SetLayout();
            return this;
        }

        private void SetLayout()
        {
            RectTransform.sizeDelta = inventoryUIPanel.inventoryUISetting.slotSize;
            backgroundImage.sprite = inventoryUIPanel.inventoryUISetting.slotBackground;
            backgroundImage.type = Image.Type.Sliced;
        }

        public void Refresh()
        {
            if (transform.childCount == 0 && ItemSlot.ItemStack != null)
            {
                itemStackUI = ItemStackCreator.Instance.Create(this);
            }

            backgroundImage.sprite = inventoryUIPanel.IsSelected(displayIndex)
                ? inventoryUIPanel.inventoryUISetting.slotSelectedBackground
                : inventoryUIPanel.inventoryUISetting.slotBackground;

            if (transform.childCount != 0 && ItemSlot.ItemStack == null)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    Destroy(transform.GetChild(i).gameObject);
                }

                itemStackUI = null;
            }

            if (itemStackUI != null)
            {
                itemStackUI.Refresh();
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (inventoryUIPanel.SelectableNow)
            {
                this.SendCommand(new SelectItemCommand(this));
            }
            else if (PointerStack.Instance.itemOnHold)
            {
                if (itemStackUI == null)
                {
                    this.SendCommand(new TryDropStackOnEmptySlotCommand(this));
                }
                else
                {
                    this.SendCommand(new TryDropStackOnNonemptyCommand(this));
                }
            }
            else
            {
                this.SendCommand(new TryHoldStackCommand(this));
            }
        }

        private void Awake()
        {
            backgroundImage = GetComponent<Image>();
            RectTransform = GetComponent<RectTransform>();
        }

        public abstract IArchitecture GetArchitecture();
    }
}