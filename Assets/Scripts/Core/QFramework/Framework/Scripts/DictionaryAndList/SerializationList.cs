using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Save_And_Load.DictionaryAndList
{
    [Serializable]
    public class SerializationList<T>
    {
        [SerializeField] private List<T> target;

        public SerializationList(List<T> target)
        {
            this.target = target;
        }

        public List<T> ToList()
        {
            return target;
        }
    }
}