using Core.Architectures;
using Core.Inventory_And_Item.Controllers;
using QFramework;

namespace Core
{
    public class InventoryGetterExample: InventoryGetter
    {
        protected override InventoryHolder GetInventoryHolder => GetComponent<InventoryHolder>();

        public override IArchitecture GetArchitecture()
        {
            return InGame.Interface;
        }
    }
}