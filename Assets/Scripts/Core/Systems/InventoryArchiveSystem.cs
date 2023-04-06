using System.Collections.Generic;
using Core.Inventory_And_Item.Data;
using Core.Inventory_And_Item.Events;
using Core.Inventory_And_Item.Models;
using Core.Save_And_Load.Event;
using Core.Save_And_Load.Utilities;
using Framework;

namespace Core.Systems
{
    public class InventoryArchiveSystem : AbstractSystem
    {
        protected override void OnInit()
        {
            SaveUtility saveUtility = this.GetUtility<SaveUtility>();
            InventoryModel inventoryModel = this.GetModel<InventoryModel>();
            InventoryModel.InventorySave inventorySave = saveUtility.LoadObject("Inventory",
                new InventoryModel.InventorySave(new Dictionary<int, Inventory>(), 0));
            inventoryModel.GetLoad(inventorySave);

            void OnEvent(SaveEvent e)
            {
                saveUtility.SaveObject(inventoryModel.GetSave(), "Inventory");
            }

            this.RegisterEvent<SaveEvent>(OnEvent);
        }
    }
}