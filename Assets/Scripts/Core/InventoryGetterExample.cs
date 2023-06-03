using Core.Architectures;
using Core.Inventory_And_Item.Controllers;
using Core.Root.Expand;
using QFramework;

namespace Core
{
    public class InventoryGetterExample : InventoryGetter
    {
        protected override InventoryHolder GetInventoryHolder => transform.GetComponentInCounterparts<InventoryHolder>();

        public override IArchitecture GetArchitecture()
        {
            return Game.Interface;
        }
    }
}