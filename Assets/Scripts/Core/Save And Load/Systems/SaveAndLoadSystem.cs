using System.Collections.Generic;
using Core.Save_And_Load.Event;
using Core.Save_And_Load.Interfaces;
using Core.Save_And_Load.Utilities;
using QFramework;

namespace Core.Save_And_Load.Systems
{
    public class SaveAndLoadSystem : AbstractSystem
    {
        private SaveUtility saveUtility;
        private List<ISave> saves = new List<ISave>();
        private List<string> keys = new List<string>();

        protected override void OnInit()
        {
            saveUtility = this.GetUtility<SaveUtility>();
            this.RegisterEvent<LoadEvent>(OnLoad);
            this.RegisterEvent<SaveEvent>(OnSave);
        }

        private void OnLoad(LoadEvent obj)
        {
            for (int index = 0; index < saves.Count; index++)
            {
                ISave loadObject = saveUtility.LoadObject<ISave>(keys[index]);
                saves[index].Load(loadObject);
            }
        }

        private void OnSave(SaveEvent obj)
        {
            for (int index = 0; index < saves.Count; index++)
            {
                object save = saves[index].Save();
                string key = keys[index];
                saveUtility.SaveObject(save, key);
            }
        }

        public void Register(ISave save, string key)
        {
            saves.Add(save);
            keys.Add(key);
        }

        public void Unregister(ISave save, string key)
        {
            saves.Remove(save);
            keys.Remove(key);
        }
    }
}