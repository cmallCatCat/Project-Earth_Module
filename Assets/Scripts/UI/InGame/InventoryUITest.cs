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
            inventory = new Inventory(capacity);
            inventory.Add(itemIdentification, 1);
            UIKit.OpenPanel<InventoryPanel>(new InventoryPanelData(inventory,12));
        }
    }
}