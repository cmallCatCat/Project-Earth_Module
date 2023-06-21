using System.Linq;
using Core.Inventory_And_Item.Data.ItemInfos.ItemFeatures;
using Core.Inventory_And_Item.Filters;
using QFramework;
using UnityEngine;

namespace Core.Inventory_And_Item.Data
{
    public abstract class EquipmentHolder : MonoBehaviour, IController
    {
        [field: SerializeField]
        public Inventory Inventory { get; private set; }

        protected ItemFilter[] itemFilters;

        private void Awake()
        {
            Inventory = new Inventory(7, transform);

            itemFilters = new ItemFilter[7];
            itemFilters[0] = new ItemFilter(FilterType.Feature, new[]
            { typeof(OnHead).FullName });
            itemFilters[1] = new ItemFilter(FilterType.Feature, new[]
            { typeof(OnBody).FullName });
            itemFilters[2] = new ItemFilter(FilterType.Feature, new[]
            { typeof(OnLegs).FullName });
            itemFilters[3] = new ItemFilter(FilterType.Feature, new[]
            { typeof(Accessory).FullName });
            itemFilters[4] = new ItemFilter(FilterType.Feature, new[]
            { typeof(Accessory).FullName });
            itemFilters[5] = new ItemFilter(FilterType.Feature, new[]
            { typeof(Accessory).FullName });
            itemFilters[6] = new ItemFilter(FilterType.Feature, new[]
            { typeof(Accessory).FullName });
            
            for (int i = 0; i < itemFilters.Length; i++)
            {
                Inventory.Slots[i].SetItemInfoFilter(itemFilters[i]);
            }
        }

        public ItemStack[] Get<T>() where T : Equipment
        {
            string feature = typeof(T).FullName;
            return Inventory.Slots
                .Where(slot => slot.ItemFilter.strings.Contains(feature) && slot.ItemStack != null)
                .Select(slot => slot.ItemStack).ToArray();
        }

        public abstract IArchitecture GetArchitecture();
    }
}