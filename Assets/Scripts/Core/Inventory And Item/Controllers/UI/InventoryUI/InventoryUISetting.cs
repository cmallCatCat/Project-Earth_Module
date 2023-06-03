using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Core.Root.Utilities;
using UnityEngine;

namespace InventoryAndItem.Core.Inventory_And_Item.Controllers.UI.InventoryUI
{
    [CreateAssetMenu(menuName = "Create InventoryUISetting", fileName = "InventoryUISetting", order = 0)]
    public class InventoryUISetting : ScriptableObject
    {
        public int                               startIndex;
        public bool                              selectable;
        public int                               displayCapacity;
        public int                               maxX;
        public int                               maxY;
        public RectOffset                        padding;
        public Vector2                           spacing;
        public Vector2                           slotSize;
        public Vector2                           anchoredPosition;
        public Vector2                           anchor;
        public Vector2                           pivot;
        public Sprite                            panelBackground;
        public Sprite                            slotDefaultBackground;
        public Sprite                            slotSelectedBackground;
        public SerializedDictionary<int, Sprite> slotBackground;

        public static InventoryUISetting Create(
            int                               startIndex             = 0,
            bool                              selectable             = false,
            int                               displayCapacity        = 10,
            int                               maxX                   = 10,
            int                               maxY                   = 1,
            RectOffset                        padding                = null,
            Vector2?                          spacing                = null,
            Vector2?                          slotSize               = null,
            Vector2?                          anchoredPosition       = null,
            Vector2?                          anchorMin              = null,
            Vector2?                          pivot                  = null,
            Sprite                            panelBackground        = null,
            Sprite                            slotBackground         = null,
            Sprite                            slotSelectedBackground = null,
            SerializedDictionary<int, Sprite> slotSelectedSprites    = null)
        {
            InventoryUISetting inventoryUISetting = CreateInstance<InventoryUISetting>();

            inventoryUISetting.startIndex             = startIndex;
            inventoryUISetting.selectable             = selectable;
            inventoryUISetting.displayCapacity        = displayCapacity;
            inventoryUISetting.maxX                   = maxX;
            inventoryUISetting.maxY                   = maxY;
            inventoryUISetting.padding                = padding ?? new RectOffset(5, 5, 5, 5);
            inventoryUISetting.spacing                = spacing ?? new Vector2(5, 5);
            inventoryUISetting.slotSize               = slotSize ?? new Vector2(50, 50);
            inventoryUISetting.anchoredPosition       = anchoredPosition ?? new Vector2(0f, 0f);
            inventoryUISetting.anchor                 = anchorMin ?? new Vector2(0.5f, 0f);
            inventoryUISetting.pivot                  = pivot ?? new Vector2(0.5f, 0f);
            inventoryUISetting.panelBackground        = panelBackground;
            inventoryUISetting.slotDefaultBackground  = slotBackground;
            inventoryUISetting.slotSelectedBackground = slotSelectedBackground;
            inventoryUISetting.slotBackground         = slotSelectedSprites ?? new SerializedDictionary<int, Sprite>();

            return inventoryUISetting;
        }

        public void Deconstruct(
            out int                               startIndex,
            out bool                              selectable,
            out int                               displayCapacity,
            out int                               maxX, out int maxY,
            out RectOffset                        padding,
            out Vector2                           spacing,
            out Vector2                           slotSize,
            out Vector2                           anchoredPosition,
            out Vector2                           anchor,
            out Vector2                           pivot,
            out Sprite                            panelBackground,
            out Sprite                            slotDefaultBackground,
            out Sprite                            slotSelectedBackground,
            out SerializedDictionary<int, Sprite> slotBackground)
        {
            startIndex             = this.startIndex;
            selectable             = this.selectable;
            displayCapacity        = this.displayCapacity;
            maxX                   = this.maxX;
            maxY                   = this.maxY;
            padding                = this.padding;
            spacing                = this.spacing;
            slotSize               = this.slotSize;
            anchoredPosition       = this.anchoredPosition;
            pivot                  = this.pivot;
            anchor                 = this.anchor;
            panelBackground        = this.panelBackground;
            slotDefaultBackground  = this.slotDefaultBackground;
            slotSelectedBackground = this.slotSelectedBackground;
            slotBackground         = this.slotBackground;
        }
    }
}