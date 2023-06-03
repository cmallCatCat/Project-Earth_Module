using Core.Inventory_And_Item.Controllers.UI.InventoryUI.Singles;
using Core.Inventory_And_Item.Data;
using QFramework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core.Inventory_And_Item.Controllers.UI.InventoryUI
{
    public abstract partial class ItemSlotUI : MonoBehaviour, IController, IPointerClickHandler, IScrollHandler,
        IPointerEnterHandler, IPointerExitHandler
    {
        public InventoryUIPanel inventoryUIPanel;

        public int displayIndex;

        public Inventory Inventory => inventoryUIPanel.inventory;

        public ItemSlot ItemSlot => Inventory.GetSlot(InventoryIndex);

        public int InventoryIndex => displayIndex + inventoryUIPanel.inventoryUISetting.startIndex;

        public RectTransform RectTransform;

        public ItemStackUI itemStackUI;

        private Image backgroundImage;

        public ItemSlotUI Init(InventoryUIPanel inventoryUIPanel, int displayIndex)
        {
            this.inventoryUIPanel = inventoryUIPanel;
            this.displayIndex     = displayIndex;
            SetLayout();
            return this;
        }

        private void SetLayout()
        {
            RectTransform.sizeDelta = inventoryUIPanel.inventoryUISetting.slotSize;
            backgroundImage.sprite  = inventoryUIPanel.inventoryUISetting.slotDefaultBackground;
            backgroundImage.type    = Image.Type.Sliced;
        }

        public void Refresh()
        {
            if (transform.childCount == 0 && ItemSlot.ItemStack != null) itemStackUI = ItemStackCreator.Instance.Create(this);

            InventoryUISetting setting = inventoryUIPanel.inventoryUISetting;
            backgroundImage.sprite = inventoryUIPanel.IsSelected(displayIndex)
                ? setting.slotSelectedBackground
                : setting.slotBackground.TryGetValue(displayIndex, out Sprite value)
                    ? value
                    : setting.slotDefaultBackground;

            if (transform.childCount != 0 && ItemSlot.ItemStack == null)
            {
                for (int i = 0; i < transform.childCount; i++) DestroyImmediate(transform.GetChild(i).gameObject);

                itemStackUI = null;
            }

            if (itemStackUI != null) itemStackUI.Refresh();
        }

        private void Awake()
        {
            backgroundImage = GetComponent<Image>();
            RectTransform   = GetComponent<RectTransform>();
        }

        public abstract IArchitecture GetArchitecture();
    }
}