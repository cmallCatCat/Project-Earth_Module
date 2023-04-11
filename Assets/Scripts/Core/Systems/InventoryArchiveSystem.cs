using System.Collections.Generic;
using Core.Inventory_And_Item.Data;
using Core.Inventory_And_Item.Models;
using Core.Save_And_Load.Event;
using Core.Save_And_Load.Utilities;
using QFramework;

namespace Core.Systems
{
    public class InventoryArchiveSystem : AbstractSystem
    {
        protected override void OnInit()
        {
            SaveUtility saveUtility = this.GetUtility<SaveUtility>();
            InventoryModel inventoryModel = this.GetModel<InventoryModel>();


            void OnSave(SaveEvent e)
            {
                saveUtility.SaveObject(inventoryModel.GetSave(), "Inventory");
            }

            void OnLoad(LoadEvent e)
            {
                InventoryModel.InventorySave inventorySave = saveUtility.LoadObject("Inventory",
                    new InventoryModel.InventorySave(new Dictionary<int, Inventory>(), 0));
                inventoryModel.GetLoad(inventorySave);
            }

            this.RegisterEvent<SaveEvent>(OnSave);
            this.RegisterEvent<LoadEvent>(OnLoad);
        }
    }
}