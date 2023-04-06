using System.Collections.Generic;
using System.Linq;
using Core.QFramework;

namespace Core.Save_And_Load.DictionaryAndList
{
    public static class DictionaryTransList
    {
        public static List<KeyValueStruct<S, T>> DictionaryToList<S, T>(Dictionary<S, T> dic)
        {
            return dic.Select(item => new KeyValueStruct<S, T>(item.Key, item.Value)).ToList();
        }

        public static Dictionary<S, T> ListToDictionary<S, T>(List<KeyValueStruct<S, T>> list)
        {
            return list.ToDictionary(item => item.Key, item => item.Value);
        }
    }
}