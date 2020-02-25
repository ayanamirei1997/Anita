using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Anita
{
    public class TestDataCopy : BaseData
    {
        private int TsetNumCopy;
    
        
        public override void OnInit()
        {
            base.OnInit();
            SetTestData();

        }
        
        private void SetTestData()
        {
            TsetNumCopy = 120;
            AnitaDebug.Log("框架测试：（Data模块）TsetNumCopy  "+"已经设置为  "+TsetNumCopy);
        }
    }
    
    
    
}