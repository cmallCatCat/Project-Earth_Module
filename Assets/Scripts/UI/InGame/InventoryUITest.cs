using System;
using System.Collections;
using Core.Inventory_And_Item.Data;
using Core.Inventory_And_Item.Data.ItemIdentifications;
using QFramework;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UI.InGame
{
    public class InventoryUITest : MonoBehaviour
    {
        public int capacity;

        [HideInEditorMode]
        public Inventory inventory;

        [SerializeField]
        private ItemIdentification itemIdentification;

        private void Awake()
        {
            ResKit.Init();
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(0.2f);
            inventory = new Inventory(capacity, transform);
            inventory.Add(new ItemStack(itemIdentification, new ItemDecorator(), 1));
            UIKit.OpenPanel<ShortcutInventory>(new ShortcutInventoryData(inventory, 12,12,1));
        }
    }
}