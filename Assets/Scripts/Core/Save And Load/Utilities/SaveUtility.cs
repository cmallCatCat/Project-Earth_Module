using System.Collections.Generic;
using Framework;

namespace Core.Save_And_Load.Utilities
{
    public abstract class SaveUtility : IUtility
    {
        public abstract void SaveDictionary<S, T>(Dictionary<S, T> inventories, string path, bool unity=true);
        public abstract void SaveList<S>(List<S> inventories, string path, bool unity=true);

        public abstract void SaveString(string toSave, string path);
        public abstract void SaveInt(int value, string path);
        public abstract void SaveObject<T>(T obj, string path);
        public abstract Dictionary<S, T> LoadDictionary<S, T>(string path, Dictionary<S, T> defaultValue = null, bool unity = true);
        public abstract List<S> LoadList<S>(string path, bool unity = true);
        public abstract string LoadString(string path, string defaultValue = "");
        public abstract int LoadInt(string path, int defaultValue = 0);

        public abstract T LoadObject<T>(string path, T defaultValue = default);
    }
}