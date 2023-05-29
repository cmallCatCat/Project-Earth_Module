using System.Collections.Generic;
using JetBrains.Annotations;
using QFramework;
using UnityEngine;

namespace UI
{
    [UsedImplicitly]
    public class GameUIController : Singleton<GameUIController>
    {
        private Dictionary<string, UIPanel> panelDic = new Dictionary<string, UIPanel>();

        public void OpenPanel<T>(IUIData uiData, string key, PanelOpenType openType) where T : UIPanel
        {
            if (panelDic.ContainsKey(key))
            {
                Debug.LogError("UI: 已经存在key为" + key+"的UI");
                return;
            }
            T openPanel = UIKit.OpenPanel<T>(uiData, openType);
            panelDic.Add(key, openPanel);
        }
        
        public void ClosePanel(string key)
        {
            if (panelDic.ContainsKey(key))
            {
                UIKit.ClosePanelUnSafe(panelDic[key]);
                panelDic.Remove(key);
            }
        }


        public override void OnSingletonInit()
        {
            base.OnSingletonInit();
        }

        public override void Dispose()
        {
            base.Dispose();
        }


        public void CloseAllPanel()
        {
            UIKit.CloseAllPanel();
        }

        private GameUIController()
        {
        }
    }
}