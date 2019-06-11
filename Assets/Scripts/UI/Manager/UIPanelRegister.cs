using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class UIPanelRegister
    {
        private const string path = "Config/UIPanelConfig";
        private static Dictionary<UIPanelType, string> panelPaths = new Dictionary<UIPanelType, string>();
        private static Dictionary<UIPanelType, BasePanel> panels = new Dictionary<UIPanelType, BasePanel>();

        static UIPanelRegister()
        {
            TextAsset tx = ResourcesManager.instance.Load<TextAsset>(path);
            UIPanels panels = JsonUtility.FromJson<UIPanels>(tx.text);
            for (int i = 0; i < panels.list.Count; i++)
            {
                if (panelPaths != null)
                {
                    UIPanelInfo info = panels.list[i];
                    panelPaths[info.type] = info.path;
                }
            }
        }
        public static BasePanel GetPanel(UIPanelType type)
        {
            if (panels == null)return null;
            BasePanel panel;
            if (!panels.TryGetValue(type, out panel))
            {
                Transform temp = ResourcesManager.instance.LoadAndInstantiate<Transform>(panelPaths[type]);
                temp.name = type.ToString();
                temp.parent = UIManager.instance.uiRootTrnasform;
                temp.localPosition = Vector3.zero;
                temp.localScale = Vector3.zero;
                panel = temp.GetComponent<BasePanel>();
                panel.UIPanelType = type;
                RegisterPanel(type,panel);
            }
            return panel;
        }

        public static void RegisterPanel(UIPanelType type, BasePanel panel)
        {
            if (!panels.ContainsKey(type))
                panels[type] = panel;
        }
        public static void UnRegisterPanel(BasePanel type)
        {

        }
    }
}
