﻿using System.Collections;
using Core.Inventory_And_Item.Controllers.UI.InventoryUI;
using Core.Inventory_And_Item.Data;
using Core.Inventory_And_Item.Data.ItemInfos;
using QFramework;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tests
{
    public class InventoryUITest : MonoBehaviour
    {
        public int capacity;

        [HideInEditorMode]
        public Inventory inventory;

        [SerializeField]
        private ItemInfo itemInfo;

        private void Awake()
        {
            ResKit.Init();
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(0.2f);
            inventory = new Inventory(capacity, transform);
            inventory.Add(new ItemStack(itemInfo, new ItemDecorator(), 1, transform));
            UIController.Instance.OpenPanel<InventoryUIPanel>(new InventoryUIPanelData(inventory, new InventoryUISetting()),
                "InventoryUIPanel", PanelOpenType.Multiple);
        }
    }
}