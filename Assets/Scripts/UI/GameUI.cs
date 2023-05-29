using Core;
using InventoryAndItem.Core.Inventory_And_Item.Controllers.UI.InventoryUI;
using InventoryAndItem.Core.Inventory_And_Item.Data.ItemInfos.ItemEffects;
using QFramework;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace UI
{
    public class GameUI : MonoSingleton<GameUI>
    {
        private const string BackpackUiPanel = "InventoryUIPanel";
        private const string ShortcutUiPanel = "ShortcutUIPanel";
        private ResLoader resLoader = ResLoader.Allocate();

        private void Awake()
        {
            ResKit.Init();
            UIController.Instance.CloseAllPanel();
            IEnvironment.Instance.MainCamera.GetUniversalAdditionalCameraData().cameraStack.Add(IEnvironment.Instance.UiCamera);
        }

        public void OpenBackpackUI()
        {
            InventoryUISetting inventoryUISetting
                = resLoader.LoadSync<InventoryUISetting>(QAssetBundle.Backpack_inventoryuisetting_asset
                    .BACKPACK_INVENTORYUISETTING);
            GameObject instancePlayer = IEnvironment.Instance.Player;
            UIController.Instance.OpenPanel<InventoryUIPanel>(
                new InventoryUIPanelData(
                    instancePlayer.GetComponentInChildren<InventoryHolderExample>().Inventory,
                    inventoryUISetting
                ),
                BackpackUiPanel,
                PanelOpenType.Multiple
            );
        }

        public void CloseBackpackUI()
        {
            UIController.Instance.ClosePanel(BackpackUiPanel);
        }

        public void OpenShortcutUI()
        {
            InventoryUISetting inventoryUISetting
                = resLoader.LoadSync<InventoryUISetting>(QAssetBundle.Shoutcut_inventoryuisetting_asset
                    .SHOUTCUT_INVENTORYUISETTING);
            GameObject instancePlayer = IEnvironment.Instance.Player;
            UIController.Instance.OpenPanel<InventoryUIPanel>(
                new InventoryUIPanelData(
                    instancePlayer.GetComponentInChildren<InventoryHolderExample>().Inventory,
                    inventoryUISetting
                ),
                ShortcutUiPanel,
                PanelOpenType.Multiple
            );
        }


        public void CloseShortcutUI()
        {
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            resLoader.Recycle2Cache();
        }
    }
}