using Core.Inventory_And_Item.Command;
using Core.Inventory_And_Item.Controllers.UI.InventoryUI.Singles;
using Core.Root.Utilities;
using QFramework;
using UnityEngine.EventSystems;

namespace Core.Inventory_And_Item.Controllers.UI.InventoryUI
{
    public abstract partial class ItemSlotUI
    {
        public  float hoverDelay = 0.3f; // 悬停多长时间后触发事件（以秒为单位）

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (inventoryUIPanel.SelectableNow)
            {
                return;
            }
            // 鼠标进入 UI 组件时开始计时
            TimerManager.Instance.TryToStopTimer("HoverTimer");
            TimerManager.Instance.StartTimer("HoverTimer", hoverDelay, () => Hover(this));
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (inventoryUIPanel.SelectableNow)
            {
                return;
            }
            // 鼠标离开 UI 组件时重置计时器
            if (TimerManager.Instance.HasTimer("HoverTimer"))
            {
                TimerManager.Instance.StopTimer("HoverTimer");
            }
            else
            {
                UnHover();
            }
        }

        protected abstract void Hover(ItemSlotUI itemSlotUI);
        protected abstract void UnHover();

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                OnLeftClick();
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                OnRightClick();
            }
        }

        public void OnScroll(PointerEventData eventData)
        {
            if (inventoryUIPanel.SelectableNow)
            {
                return;
            }
            if (itemStackUI != null && PointerStack.Instance.CanAdd(itemStackUI.itemStack) && eventData.scrollDelta.y > 0)
            {
                this.SendCommand(new TryHoldStackCommand(this, 1));
            }
            else if (PointerStack.Instance.ItemOnHold && eventData.scrollDelta.y < 0)
            {
                this.SendCommand(new TryDropStackOnlyCommand(this, 1));
            }
        }

        private void OnRightClick()
        {
            if (inventoryUIPanel.SelectableNow)
            {
                this.SendCommand(new SelectItemCommand(this));
                return;
            }
            if (itemStackUI != null)
            {
                if (PointerStack.Instance.CanAdd(itemStackUI.itemStack))
                {
                    this.SendCommand(new TryHoldStackCommand(this, itemStackUI.itemStack.Number / 2));
                }
            }
            else
            {
                this.SendCommand(new TryDropStackOnEmptySlotCommand(this));
            }
        }

        private void OnLeftClick()
        {
            if (inventoryUIPanel.SelectableNow)
            {
                this.SendCommand(new SelectItemCommand(this));
                return;
            }
            if (PointerStack.Instance.ItemOnHold)
            {
                if (itemStackUI == null)
                    this.SendCommand(new TryDropStackOnEmptySlotCommand(this));
                else
                    this.SendCommand(new TryDropStackOnNonemptyCommand(this));
            }
            else
            {
                if (itemStackUI != null)
                    this.SendCommand(new TryHoldStackCommand(this));
            }
        }

        private void OnDestroy()
        {
            UnHover();
            TimerManager.Instance.TryToStopTimer("HoverTimer");
        }
    }
}