using InventoryAndItem.Core.Inventory_And_Item.Data;
using InventoryAndItem.Core.Inventory_And_Item.Events;
using QFramework;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryAndItem.Core.Inventory_And_Item.Controllers.UI
{
    public class InventoryUIPanelData : UIPanelData
    {
        public readonly Inventory inventory;
        public readonly InventoryUISetting inventoryUISetting;

        public InventoryUIPanelData(Inventory inventory, InventoryUISetting inventoryUISetting)
        {
            this.inventory = inventory;
            this.inventoryUISetting = inventoryUISetting;
        }
    }

    public abstract partial class InventoryUIPanel : UIPanel, IController
    {
        private Inventory inventory;
        private ItemSlotUI[] itemSlots;

        [SerializeField]
        private Transform slotsParent;

        [SerializeField]
        private GameObject slotPrefab;

        private InventoryUISetting inventoryUISetting;


        protected override void OnOpen(IUIData uiData = null)
        {
            if (uiData == null)
            {
                inventory = new Inventory(20, transform);
                uiData = new InventoryUIPanelData(inventory, new InventoryUISetting());
                Debug.LogError("uiData is null");
            }

            mUIPanelData = (InventoryUIPanelData)uiData;
            inventory = mUIPanelData.inventory;
            inventoryUISetting = mUIPanelData.inventoryUISetting;

            inventory.onItemChanged += Refresh;
            SetLayout();
            Refresh();
        }

        private void SetLayout()
        {
            //读取
            inventoryUISetting.Deconstruct(out int displayCapacity, out int maxX, out int maxY, out RectOffset padding,
                out Vector2 spacing, out Vector2 slotSize, out Sprite panelBackground, out Sprite slotBackground);
            //计算
            displayCapacity = Mathf.Min(displayCapacity, maxX * maxY, inventory.AllSlots().Length);
            int xNum = Mathf.Min(maxX, displayCapacity);
            int yNum = Mathf.Min(maxY, Mathf.CeilToInt(displayCapacity * 1.0f / xNum));
            Vector2 sizeDelta = new Vector2(xNum * slotSize.x + (xNum - 1) * spacing.x + padding.left + padding.right,
                yNum * slotSize.y + (yNum - 1) * spacing.y + padding.top + padding.bottom);
            //创建
            itemSlots = new ItemSlotUI[displayCapacity];
            for (int i = 0; i < displayCapacity; i++)
            {
                Transform instantiate = Instantiate(slotPrefab, slotsParent).transform;
                instantiate.GetComponent<RectTransform>().sizeDelta = slotSize;
                instantiate.GetComponent<Image>().sprite = slotBackground;
                instantiate.GetComponent<Image>().type = Image.Type.Sliced;
                itemSlots[i] = instantiate.GetComponent<ItemSlotUI>().Init(i, inventory);
            }

            //赋值
            GridLayoutGroup gridLayoutGroup = slotsParent.GetComponent<GridLayoutGroup>();
            gridLayoutGroup.padding.top = padding.top;
            gridLayoutGroup.padding.bottom = padding.bottom;
            gridLayoutGroup.padding.left = padding.left;
            gridLayoutGroup.padding.right = padding.right;
            gridLayoutGroup.spacing = spacing;
            gridLayoutGroup.cellSize = slotSize;
            slotsParent.GetComponent<RectTransform>().sizeDelta = sizeDelta;
            Image image = slotsParent.GetComponent<Image>();
            image.sprite = panelBackground;
            image.type = Image.Type.Sliced;

            //刷新
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)slotsParent);
        }

        [ContextMenu("Refresh")]
        private void Refresh()
        {
            int minLength = itemSlots.Length;
            if (itemSlots.Length > inventory.AllSlots().Length)
            {
                Debug.LogWarning(
                    $"the displayCapacity of inventory is not enough to display all slots: slotImages.Length = {itemSlots.Length}, inventory.AllSlots().Length = {inventory.AllSlots().Length}");
                minLength = inventory.AllSlots().Length;
            }

            for (int index = 0; index < minLength; index++)
            {
                itemSlots[index].Refresh();
            }
        }

        protected override void OnInit(IUIData uiData = null)
        {
        }

        protected override void OnShow()
        {
        }

        protected override void OnHide()
        {
        }

        protected override void OnClose()
        {
            inventory.onItemChanged -= Refresh;
        }

        public abstract IArchitecture GetArchitecture();
    }
}