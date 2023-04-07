using Core.Architectures;
using Core.Inventory_And_Item.Controllers;
using Framework;

namespace Core
{
    public class ARInventoryHolder: InventoryHolder
    {
        public override IArchitecture GetArchitecture()
        {
            return InGame.Interface;
        }
    }
}