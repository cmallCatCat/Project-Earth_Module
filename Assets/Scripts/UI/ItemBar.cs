using Core.Architectures;
using Core.Inventory_And_Item.Data;
using Core.Inventory_And_Item.Events;
using Core.Inventory_And_Item.Filters;
using Core.Inventory_And_Item.Models;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.UI
{
    public class ItemBarData : UIPanelData
    {
    }

    public partial class ItemBar : UIPanel, IController
    {
        private InventoryModel inventoryModel;

        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as ItemBarData ?? new ItemBarData();
            // please add init code here
            inventoryModel = this.GetModel<InventoryModel>();
            this.RegisterEvent<ItemChangedEvent>(Refresh);
        }

        private void Refresh(ItemChangedEvent obj)
        {
            if (obj.holder != inventoryModel.PlayerKey)
            {
                return;
            }

            ItemSlot[] itemSlots = inventoryModel.Search(obj.holder, SlotFilters.All);
            for (var i = 0; i < Slots.Length; i++)
            {
                UISlot uiSlot = Slots[i];
                uiSlot.Refresh(itemSlots[i]);
            }
        }

        protected override void OnOpen(IUIData uiData = null)
        {
        }

        protected override void OnShow()
        {
        }

        protected override void OnHide()
        {
        }

        protected override void OnClose()
        {
        }

        public IArchitecture GetArchitecture()
        {
            return InGame.Interface;
        }
    }
}