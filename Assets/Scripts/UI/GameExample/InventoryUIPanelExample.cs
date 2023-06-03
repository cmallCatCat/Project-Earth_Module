using Core.Architectures;
using Core.Inventory_And_Item.Controllers.UI.InventoryUI;
using QFramework;

namespace UI.GameExample
{
    public class InventoryUIPanelExample : InventoryUIPanel
    {
        public override IArchitecture GetArchitecture()
        {
            return Game.Interface;
        }
    }
}