using Core.Architectures;
using InventoryAndItem.Core.Inventory_And_Item.Controllers.UI;
using QFramework;

namespace UI.GameExample
{
    public class ItemSlotUIExample : ItemSlotUI
    {
        public override IArchitecture GetArchitecture()
        {
            return Game.Interface;
        }
    }
}