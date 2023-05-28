using Core.Architectures;
using InventoryAndItem.Core.Inventory_And_Item.Controllers;
using InventoryAndItem.Core.Inventory_And_Item.Data.ItemInfos.ItemEffects;
using QFramework;

namespace Core
{
    public class InventoryHolderExample: InventoryHolder
    {
        
        public override IArchitecture GetArchitecture()
        {
            return Game.Interface;
        }
    }
}