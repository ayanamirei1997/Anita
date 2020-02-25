using System;
using System.Collections;
using System.Collections.Generic;
using Anita;
using FairyGUI;

using ResourceManager;

namespace Anita
{
    public class UILoadConfig
    {
        Dictionary<Type, List<UICfg>> uiCfgDict;

        private static UILoadConfig _instance;
        public static UILoadConfig Instance {
            get
            {
                if (_instance == null)
                    _instance = new UILoadConfig();
                return _instance;
            }
        }
        public UILoadConfig()
        {
            uiCfgDict = new Dictionary<Type, List<UICfg>>();
        }

        public List<UICfg> GetUICfg(Type type)
        {
            List<UICfg> result = null;
            uiCfgDict.TryGetValue(type, out result);
            return result;
        }

        public void AddUICfg(Type type, List<UICfg> uiCfgs)
        {
            if (uiCfgDict.ContainsKey(type))
            {
                uiCfgDict[type] = uiCfgs;
            }
            else
            {
                uiCfgDict.Add(type, uiCfgs);
            }
        }

        public void RemoveUICfg(Type type)
        {
            uiCfgDict.Remove(type);
        }

        public void Clear()
        {
            uiCfgDict.Clear();
        }
    }
}