using Core.Architectures;
using Core.Inventory_And_Item.Controllers;
using Framework;

namespace Others
{
    public class InventoryHolderExample : InventoryHolder
    {
        

        public override IArchitecture GetArchitecture()
        {
            return InGame.Interface;
        }
    }
}