using System;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

namespace InventoryAndItem.Core.Inventory_And_Item.Controllers.UI.InventoryUI
{
    [CreateAssetMenu(menuName = "Create InventoryUISetting", fileName = "InventoryUISetting", order = 0)]
    public class InventoryUISetting : ScriptableObject
    {
        public int startIndex;
        public int displayCapacity;
        public int maxX;
        public int maxY;
        public RectOffset padding;
        public Vector2 spacing;
        public Vector2 slotSize;
        public Vector2 anchoredPosition;
        public Vector2 anchorMin;
        public Vector2 anchorMax;
        public Vector2 pivot;
        public Sprite panelBackground;
        public Sprite slotBackground;
        public Sprite slotSelectedBackground;

        public static InventoryUISetting Create(int startIndex = 0, int displayCapacity = 10, int maxX = 10, int maxY = 1,
            RectOffset padding = null, Vector2? spacing = null, Vector2? slotSize = null, Vector2? anchoredPosition = null,
            Vector2? anchorMin = null, Vector2? anchorMax = null, Vector2? pivot = null, Sprite panelBackground = null,
            Sprite slotBackground = null, Sprite slotSelectedBackground = null)
        {
            InventoryUISetting inventoryUISetting = CreateInstance<InventoryUISetting>();

            inventoryUISetting.startIndex = startIndex;
            inventoryUISetting.displayCapacity = displayCapacity;
            inventoryUISetting.maxX = maxX;
            inventoryUISetting.maxY = maxY;
            inventoryUISetting.padding = padding ?? new RectOffset(5, 5, 5, 5);
            inventoryUISetting.spacing = spacing ?? new Vector2(5, 5);
            inventoryUISetting.slotSize = slotSize ?? new Vector2(50, 50);
            inventoryUISetting.anchoredPosition = anchoredPosition ?? new Vector2(0f, 0f);
            inventoryUISetting.anchorMin = anchorMin ?? new Vector2(0.5f, 0f);
            inventoryUISetting.anchorMax = anchorMax ?? new Vector2(0.5f, 0f);
            inventoryUISetting.pivot = pivot ?? new Vector2(0.5f, 0f);
            inventoryUISetting.panelBackground = panelBackground;
            inventoryUISetting.slotBackground = slotBackground;
            inventoryUISetting.slotSelectedBackground = slotSelectedBackground;

            return inventoryUISetting;
        }

        public void Deconstruct(out int startIndex, out int displayCapacity, out int maxX, out int maxY, out RectOffset padding, out Vector2 spacing,
            out Vector2 slotSize, out Vector2 anchoredPosition, out Vector2 anchorMin, out Vector2 anchorMax, out Vector2 pivot,
            out Sprite panelBackground, out Sprite slotBackground, out Sprite slotSelectedBackground)
        {
            startIndex = this.startIndex;
            displayCapacity = this.displayCapacity;
            maxX = this.maxX;
            maxY = this.maxY;
            padding = this.padding;
            spacing = this.spacing;
            slotSize = this.slotSize;
            anchoredPosition = this.anchoredPosition;
            pivot = this.pivot;
            anchorMin = this.anchorMin;
            anchorMax = this.anchorMax;
            panelBackground = this.panelBackground;
            slotBackground = this.slotBackground;
            slotSelectedBackground = this.slotSelectedBackground;
        }
    }
}