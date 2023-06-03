using System;
using Core;
using Core.Architectures;
using Core.Root.Base;
using InventoryAndItem.Core.Inventory_And_Item.Controllers;
using InventoryAndItem.Core.Inventory_And_Item.Data;
using InventoryAndItem.Core.Inventory_And_Item.Data.ItemInfos;
using QAssetBundle;
using QFramework;
using UI;
using UnityEngine;

namespace Tests
{
    public class SampleTest : MonoBehaviour, IController
    {
        private ResLoader resLoader = ResLoader.Allocate();

        public ItemInfo itemInfo;

        private void Awake()
        {
            ResKit.Init();

            GameObject pickerPrefab = resLoader.LoadSync<GameObject>(Itempickerprefab_prefab.ITEMPICKERPREFAB);
            GameObject instantiate
                = IEnvironment.Instance.Instantiate(pickerPrefab, false, new Vector3(10, 0, 0), Quaternion.identity);
            instantiate.GetComponent<ItemPicker>().Init(new ItemStack(itemInfo, new ItemDecorator(), 1, transform));
        }

        private void OnGUI()
        {
            if (GUILayout.Button("创建"))
            {
                if (IEnvironment.Instance.Player != null) throw new Exception("Player is already created");

                GameObject loadSync = resLoader.LoadSync<GameObject>(Player_prefab.PLAYER);
                GameObject instantiate
                    = IEnvironment.Instance.Instantiate(loadSync, false, new Vector3(0, 0, 0), Quaternion.identity);
                instantiate.GetComponentInChildren<ItemGravitater>().Init(5, 40);
            }

            if (GUILayout.Button("添加"))
                IEnvironment.Instance.Player.GetComponentInChildren<InventoryHolderExample>().Inventory
                    .Add(new ItemStack(itemInfo, new ItemDecorator(), 1, transform));

            if (GUILayout.Button("打开"))
            {
                GameUI.Instance.OpenBackpackUI();
                GameUI.Instance.OpenEquipmentUI();
                UIController.Instance.isPaused = true;
            }

            if (GUILayout.Button("关闭"))
            {
                GameUI.Instance.CloseBackpackUI();
                UIController.Instance.isPaused = false;
            }
        }

        private void OnDestroy()
        {
            resLoader.Recycle2Cache();
            resLoader.Dispose();
        }

        public IArchitecture GetArchitecture()
        {
            return Game.Interface;
        }
    }
}