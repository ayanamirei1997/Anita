using System;
using System.Collections.Generic;
using UnityEngine;

namespace Anita
{
    public abstract class BaseData
    {
        /// <summary>
        ///   游戏启动时执行，仅做模块内数据初始化逻辑
        /// </summary>
        public virtual void OnInit()
        {
        }

        /// <summary>
        ///     在所有模块接收完登陆数据后执行
        ///     若本模块需要其他模块数据, 请将逻辑写在此处
        /// </summary>
        public virtual void OnLogin()
        {
        }

        /// <summary>
        ///     清空模块数据
        /// </summary>
        public virtual void OnClear()
        {
        }
    }

    public class Data
    {
        private Dictionary<Type, BaseData> _baseData;

        public Data()
        {
            BindBaseData();
        }

        // 自定义数据放在这里
        public TestData TestData;
        public TestDataCopy TestDataCopy;
        
        private void BindBaseData()
        {
            _baseData = new Dictionary<Type, BaseData>();
            BindBaseData(TestData = new TestData());
            BindBaseData(TestDataCopy = new TestDataCopy());
        }

        private void BindBaseData(BaseData baseData)
        {
            _baseData.Add(baseData.GetType(), baseData);
        }
        
        public void Clear()
        {
            foreach (var baseData in _baseData.Values)
                if (null != baseData)
                    baseData.OnClear();
        }

        public void OnInit()
        {
            foreach (var baseData in _baseData.Values)
                if (null != baseData)
                    baseData.OnInit();
            AnitaDebug.Log("初始化Data!");
        }

        public void OnLogin()
        {
            foreach (var baseData in _baseData.Values)
                if (null != baseData)
                    baseData.OnLogin();
        }
    }
    
    
    
    
    
}
