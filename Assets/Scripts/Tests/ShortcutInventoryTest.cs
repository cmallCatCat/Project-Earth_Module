using System;
using Core;
using Core.Architectures;
using InventoryAndItem.Core.Inventory_And_Item.Data;
using InventoryAndItem.Core.Inventory_And_Item.Data.ItemInfos;
using QFramework;
using UI.InGame;
using UnityEngine;

namespace Tests
{
    public class ShortcutInventoryTest : MonoBehaviour, IController
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
                if (SceneEnvironment.Instance.Player != null)
                {
                    throw new Exception("Player is already created");
                }

                GameObject loadSync = resLoader.LoadSync<GameObject>(QAssetBundle.Player_prefab.PLAYER);
                GameObject instantiate = Instantiate(loadSync);
                UIKit.OpenPanel<InventoryUIPanel>(new InventoryUIPanelData(
                    instantiate.GetComponentInChildren<InventoryHolderExample>().Inventory, 12, 12, 1)
                );
                instantiate.name = "Player";
                instantiate.GetComponentInChildren<InventoryHolderExample>().Inventory
                    .Add(new ItemStack(itemInfo, new ItemDecorator(), 1, transform));
            }
        }

        public IArchitecture GetArchitecture()
        {
            return InGame.Interface;
        }
    }
}