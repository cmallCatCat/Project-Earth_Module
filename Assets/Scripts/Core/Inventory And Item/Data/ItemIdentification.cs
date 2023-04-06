using System;
using UnityEngine;

namespace Core.Inventory_And_Item.Data
{
    [Serializable]
    [CreateAssetMenu(menuName = "Create ItemIdentification", fileName = "ItemIdentification", order = 0)]
    public class ItemIdentification : ScriptableObject
    {
        [SerializeField] private string itemName = "Unnamed Item";

        [SerializeField] private string itemDescription = "UnDefined Item Description";

        [SerializeField] private int maxStack = 1;

        [SerializeField] private Sprite spriteIcon;

        [SerializeField] private Sprite spriteInGame;

        [SerializeField] private Decorator decorator;


        public string ItemName => itemName;

        public string ItemDescription => itemDescription;

        public int MaxStack => maxStack;

        public Sprite SpriteIcon => spriteIcon;

        public Sprite SpriteInGame => spriteInGame;
        public Decorator Decorator => decorator;


        #region EqualsAndHashCode

        public static bool operator ==(ItemIdentification a, ItemIdentification b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
            return !ReferenceEquals(a, null) && !ReferenceEquals(b, null) && a.ItemName == b.ItemName &&
                   a.Decorator == b.Decorator;
        }

        public static bool operator !=(ItemIdentification a, ItemIdentification b)
        {
            return !(a == b);
        }

        protected bool Equals(ItemIdentification other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ItemIdentification)obj);
        }

        public override int GetHashCode()
        {
            return ItemName.GetHashCode();
        }

        #endregion
    }
}