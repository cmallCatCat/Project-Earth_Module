using System;
using Core.Inventory_And_Item.Data;
using QFramework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.InGame
{
    public class ShortcutInventoryData : UIPanelData
    {
        public readonly Inventory inventory;
        public readonly int displayCapacity;
        public readonly int maxX;
        public readonly int maxY;

        public ShortcutInventoryData(Inventory inventory, int displayCapacity, int maxX, int maxY)
        {
            this.inventory = inventory;
            this.displayCapacity = displayCapacity;
            this.maxX = maxX;
            this.maxY = maxY;
        }
    }

    public partial class ShortcutInventory : UIPanel
    {
        private Inventory inventory;
        private ShortcutItemSlot[] itemSlots;


        [SerializeField]
        private Transform slotsParent;

        [SerializeField]
        private GameObject slotPrefab;

        [SerializeField]
        private int slotSize;

        [SerializeField]
        private Vector2 spacing;

        [SerializeField]
        private Vector4 padding;

        [SerializeField]
        private int maxX;

        [SerializeField]
        private int maxY;

        [SerializeField]
        private int displayCapacity;

        private void Refresh()
        {
            int minLength = itemSlots.Length;
            if (itemSlots.Length > inventory.AllSlots().Length)
            {
                Debug.LogWarning("the displayCapacity of inventory is not enough to display all slots: " +
                                 "slotImages.Length = " + itemSlots.Length + ", inventory.AllSlots().Length = " +
                                 inventory.AllSlots().Length);
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

        protected override void OnOpen(IUIData uiData = null)
        {
            if (uiData == null)
            {
                inventory = new Inventory(20, transform);
                uiData = new ShortcutInventoryData(inventory, displayCapacity, maxX, maxY);
                Debug.LogError("uiData is null");
            }

            mData = (ShortcutInventoryData)uiData;
            // please add init code here
            inventory = mData.inventory;
            displayCapacity = mData.displayCapacity;
            maxX = mData.maxX;
            maxY = mData.maxY;
            displayCapacity = Mathf.Min(displayCapacity, maxX * maxY, inventory.AllSlots().Length);

            // slotPrefab.GetComponent<RectTransform>().sizeDelta = new Vector2(slotSize, slotSize);
            GridLayoutGroup gridLayoutGroup = slotsParent.GetComponent<GridLayoutGroup>();
            gridLayoutGroup.padding.top = (int)padding.x;
            gridLayoutGroup.padding.bottom = (int)padding.y;
            gridLayoutGroup.padding.left = (int)padding.z;
            gridLayoutGroup.padding.right = (int)padding.w;
            gridLayoutGroup.cellSize = new Vector2(slotSize, slotSize);
            gridLayoutGroup.spacing = spacing;
            int xNum = Mathf.Min(maxX, displayCapacity);
            int yNum = Mathf.Min(maxY, Mathf.CeilToInt(displayCapacity * 1.0f / xNum));

            slotsParent.GetComponent<RectTransform>().sizeDelta = new Vector2(
                xNum * (slotSize + spacing.x) + padding.x + padding.y - spacing.x,
                yNum * (slotSize + spacing.y) + padding.z + padding.w - spacing.y);

            itemSlots = new ShortcutItemSlot[displayCapacity];
            for (int i = 0; i < displayCapacity; i++)
            {
                Transform instantiate = Instantiate(slotPrefab, slotsParent).transform;
                // instantiate.GetComponent<GridLayoutGroup>().cellSize = new Vector2(slotSize, slotSize);
                itemSlots[i] = instantiate.GetComponent<ShortcutItemSlot>();
                itemSlots[i].Init(i, inventory);
            }


            inventory.onItemChanged += Refresh;
            Refresh();
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
    }
}