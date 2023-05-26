using Core.Architectures;
using InventoryAndItem.Core.Inventory_And_Item.Controllers;
using InventoryAndItem.Core.Inventory_And_Item.Data.ItemInfos.ItemEffects;
using QFramework;

namespace Core
{
    public class InventoryHolderExample: InventoryHolder
    {

        protected override IEnvironment GetEnvironment => SceneEnvironment.Instance;

        public override IArchitecture GetArchitecture()
        {
            return InGame.Interface;
        }
    }
}