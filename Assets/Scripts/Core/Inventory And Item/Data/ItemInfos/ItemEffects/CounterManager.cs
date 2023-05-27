﻿using System.Collections.Generic;

namespace InventoryAndItem.Core.Inventory_And_Item.Data.ItemInfos.ItemEffects
{
    public static class CounterManager
    {
        public class Counter
        {
            public int value;
            public static implicit operator int(Counter counter) => counter.value;
            public static implicit operator Counter(int value) => new Counter { value = value };
        }
        
        private static  Dictionary<object,Counter> counters = new Dictionary<object, Counter>();
        public static Counter Get(object sender)
        {
            counters.TryAdd(sender, new Counter());
            return counters[sender];
        }
        
    }

    
}