using UnityEngine;

namespace InventoryAndItem.Core.Inventory_And_Item.Controllers.UI
{
    public class InventoryUISetting
    {
        public int displayCapacity = 10;
        public int maxX = 10;
        public int maxY = 1;
        public RectOffset padding = new RectOffset(5, 5, 5, 5);
        public Vector2 spacing = new Vector2(5, 5);
        public Vector2 slotSize = new Vector2(50, 50);
        public Sprite panelBackground = null;
        public Sprite slotBackground = null;


        public void Deconstruct(out int displayCapacity, out int maxX, out int maxY, out RectOffset padding, out Vector2 spacing,
            out Vector2 slotSize, out Sprite panelBackground, out Sprite slotBackground)
        {
            displayCapacity = this.displayCapacity;
            maxX = this.maxX;
            maxY = this.maxY;
            padding = this.padding;
            spacing = this.spacing;
            slotSize = this.slotSize;
            panelBackground = this.panelBackground;
            slotBackground = this.slotBackground;
        }
    }
}