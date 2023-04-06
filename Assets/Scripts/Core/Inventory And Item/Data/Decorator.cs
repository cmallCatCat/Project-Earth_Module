using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Inventory_And_Item.Data
{
    [Serializable]
    public class Decorator
    {
        [SerializeReference] internal List<Decoration> decorations;

        public Decorator()
        {
            decorations = new List<Decoration>();
        }

        internal void Add(Decoration decoration)
        {
            decorations.Add(decoration);
        }

        internal void Remove(Decoration decoration)
        {
            decorations.Remove(decoration);
        }
    }
}