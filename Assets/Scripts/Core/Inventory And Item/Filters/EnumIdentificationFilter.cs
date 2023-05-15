#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Core.Inventory_And_Item.Data.ItemIdentifications;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Inventory_And_Item.Filters
{
    [Serializable]
    public class EnumIdentificationFilter : IdentificationFilter, ISerializationCallbackReceiver
    {
        internal EnumIdentificationFilter(string[] itemIdentifications) :
            base(GetPredicate(itemIdentifications))
        {
            _itemIdentifications = itemIdentifications;
        }

        [SerializeField] private string[] _itemIdentifications;

        private static Predicate<ItemIdentification?> GetPredicate(string[] itemIdentifications)
        {
            return i => i == null || itemIdentifications.Contains(i.GetType().FullName);
        }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            _isMatch = GetPredicate(_itemIdentifications);
        }
    }

    [Serializable]
    public class U_EnumIdentificationFilter : IdentificationFilter, ISerializationCallbackReceiver
    {
        internal U_EnumIdentificationFilter(string[] itemIdentifications) :
            base(GetPredicate(itemIdentifications))
        {
            _itemIdentifications = itemIdentifications;
        }

        [SerializeField] private string[] _itemIdentifications;
        private static Predicate<ItemIdentification?> GetPredicate(string[] itemIdentifications)
        {
            return i => i == null || !itemIdentifications.Contains(i.GetType().FullName);
        }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            _isMatch = GetPredicate(_itemIdentifications);
        }
    }

    [Serializable]
    public class FeatureFilter : IdentificationFilter, ISerializationCallbackReceiver
    {
        public FeatureFilter(string[] features) :
            base(GetPredicate(features))
        {
            _features = features;
        }

        
        [SerializeField] private string[] _features;
        private static Predicate<ItemIdentification?> GetPredicate(string[] features)
        {
            return i => i == null || features.Any(i.HasFeature);
        }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            _isMatch= GetPredicate(_features);
        }
        
    }

    [Serializable]
    public class U_FeatureFilter : IdentificationFilter, ISerializationCallbackReceiver
    {
        public U_FeatureFilter(string[] features) :
            base(GetPredicate(features))
        {
            _features = features;
        }

        
        [SerializeField] private string[] _features;
        private static Predicate<ItemIdentification?> GetPredicate(string[] features)
        {
            return i => i == null || !features.Any(i.HasFeature);
        }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            _isMatch= GetPredicate(_features);
        }
    }
}