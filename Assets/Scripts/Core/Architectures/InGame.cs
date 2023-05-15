using Core.Inventory_And_Item.Models;
using Core.Save_And_Load.Systems;
using Core.Save_And_Load.Utilities;
using Core.Systems;
using QFramework;

namespace Core.Architectures
{
    public class InGame : Architecture<InGame>
    {
        protected override void Init()
        {
            RegisterModel(new InventoryModel());
            RegisterUtility<SaveUtility>(new JsonSaveUtility());
            // RegisterSystem(new InventoryArchiveSystem());
            RegisterSystem(new GameObjectSaveSystem());
        }
    }
}