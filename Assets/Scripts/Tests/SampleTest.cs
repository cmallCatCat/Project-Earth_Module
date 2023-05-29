using System;
using Core;
using Core.Architectures;
using InventoryAndItem.Core.Inventory_And_Item.Data;
using InventoryAndItem.Core.Inventory_And_Item.Data.ItemInfos;
using InventoryAndItem.Core.Inventory_And_Item.Data.ItemInfos.ItemEffects;
using QFramework;
using UI;
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
                Instantiate(loadSync);
            }
            
            if (GUILayout.Button("添加"))
            {
                IEnvironment.Instance.Player.GetComponentInChildren<InventoryHolderExample>().Inventory
                    .Add(new ItemStack(itemInfo, new ItemDecorator(), 1, transform));
            }

            if (GUILayout.Button("打开"))
            {
                GameUI.Instance.OpenBackpackUI();
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