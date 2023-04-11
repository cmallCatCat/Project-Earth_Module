/****************************************************************************
 * Copyright (c) 2015 - 2022 liangxiegame UNDER MIT License
 * 
 * http://qframework.cn
 * https://github.com/liangxiegame/QFramework
 * https://gitee.com/liangxiegame/QFramework
 ****************************************************************************/

#if UNITY_EDITOR
using UnityEditor;

namespace QFramework
{
    public interface IPackageKitView
    {
        EditorWindow EditorWindow { get; set; }
        
        void Init();

        void OnUpdate();
        void OnGUI();

        void OnWindowGUIEnd();

        void OnDispose();
        void OnShow();
        void OnHide();
    }
}
#endif