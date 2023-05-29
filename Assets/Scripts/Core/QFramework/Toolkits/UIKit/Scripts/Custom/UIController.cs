using System.Collections.Generic;
using JetBrains.Annotations;
using QFramework;
using UnityEngine;

namespace QFramework
{
    [UsedImplicitly]
    public class UIController : Singleton<UIController>
    {
        private Dictionary<string, UIPanel> panelDic = new Dictionary<string, UIPanel>();
        public bool isPaused = false;


        public void OpenPanel<T>(IUIData uiData, string key, PanelOpenType openType) where T : UIPanel
        {
            
            if (panelDic.ContainsKey(key))
            {
                Debug.LogWarning($"UI: 已经存在key为{key}的UI");
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
        
        public bool IsOpenPanel(string key)
        {
            return panelDic.ContainsKey(key);
        }


        public void CloseAllPanel()
        {
            UIKit.CloseAllPanel();
        }

        private UIController()
        {
        }
    }
}