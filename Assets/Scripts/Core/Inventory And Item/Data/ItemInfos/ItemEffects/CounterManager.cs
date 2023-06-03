using System.Collections.Generic;

namespace Core.Inventory_And_Item.Data.ItemInfos.ItemEffects
{
    public static class CounterManager
    {
        public class Counter
        {
            public int value;

            public static implicit operator int(Counter counter)
            {
                return counter.value;
            }

            public static implicit operator Counter(int value)
            {
                return new Counter
                { value = value };
            }
        }

        private static Dictionary<object, Counter> counters = new Dictionary<object, Counter>();

        public static Counter Get(object sender)
        {
            counters.TryAdd(sender, new Counter());
            return counters[sender];
        }
    }
}