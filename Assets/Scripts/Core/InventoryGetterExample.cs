using Core.Architectures;
using Core.Inventory_And_Item.Controllers;
using QFramework;

namespace Core
{
    public class InventoryGetterExample: InventoryGetter
    {
        public override IArchitecture GetArchitecture()
        {
            return InGame.Interface;
        }
    }
}