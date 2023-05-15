using System;
using UnityEngine;

namespace Core.Inventory_And_Item.Data.ItemIdentifications.ItemEffects
{
    [Serializable]
    [CreateAssetMenu(menuName = "Create CreateGameObject", fileName = "CreateGameObject", order = 0)]
    public class CreateGameObject : ItemEffect
    {
        public GameObject toCreate;
        public override void Work(ItemStack stack)
        {
            Instantiate(toCreate);
        }
    }
    
    public class AddBuff : ItemEffect
    {
        public override void Work(ItemStack stack)
        {
            
        }
    }
}