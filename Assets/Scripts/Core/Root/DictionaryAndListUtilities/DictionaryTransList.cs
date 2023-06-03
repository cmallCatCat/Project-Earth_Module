using System.Collections.Generic;
using System.Linq;
using Core.Root.Utilities;

namespace Core.Root.DictionaryAndListUtilities
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