using Core.Architectures;
using Core.Inventory_And_Item.Controllers.UI.InventoryUI;
using QFramework;

namespace UI.GameExample
{
    public class ItemSlotUIExample : ItemSlotUI
    {
        protected override void Hover(ItemSlotUI itemSlotUI)
        {
            GameUI.Instance.OpenItemInfoUI(itemSlotUI);
        }

        protected override void UnHover()
        {
            GameUI.Instance.CloseItemInfoUI();
        }

        public override IArchitecture GetArchitecture()
        {
            return Game.Interface;
        }
    }
}