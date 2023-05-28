using System;
using Core;
using Core.Architectures;
using InventoryAndItem.Core.Inventory_And_Item.Controllers.UI;
using InventoryAndItem.Core.Inventory_And_Item.Data;
using InventoryAndItem.Core.Inventory_And_Item.Data.ItemInfos;
using InventoryAndItem.Core.Inventory_And_Item.Data.ItemInfos.ItemEffects;
using QFramework;
using UnityEngine;

namespace Tests
{
    public class SampleTest : MonoBehaviour, IController
    {
        private ResLoader resLoader = ResLoader.Allocate();

        public ItemInfo itemInfo;

        [SerializeField]
        private Sprite modSprite;

        private void Awake()
        {
            ResKit.Init();
            UIKit.CloseAllPanel();
        }

        private void OnGUI()
        {
            if (GUILayout.Button("创建"))
            {
                if (IEnvironment.Instance.Player != null)
                {
                    throw new Exception("Player is already created");
                }

                GameObject loadSync = resLoader.LoadSync<GameObject>(QAssetBundle.Player_prefab.PLAYER);
                GameObject instantiate = Instantiate(loadSync);
                InventoryUIPanelData inventoryUIPanelData = new InventoryUIPanelData(
                    instantiate.GetComponentInChildren<InventoryHolderExample>().Inventory,
                    new InventoryUISetting());
                UIKit.OpenPanel<InventoryUIPanel>(inventoryUIPanelData);
                instantiate.name = "Player";
                instantiate.GetComponentInChildren<InventoryHolderExample>().Inventory
                    .Add(new ItemStack(itemInfo, new ItemDecorator(), 1, transform));
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