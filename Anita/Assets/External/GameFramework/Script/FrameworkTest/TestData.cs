using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Anita
{
    public class TestData : BaseData
    {
        private int TestNum;
    
        
        public override void OnInit()
        {
            base.OnInit();
            SetTestData();

        }
        
        private void SetTestData()
        {
            TestNum = 996;
            AnitaDebug.Log("框架测试：（Data模块）TsetNum  "+"已经设置为  "+TestNum);
        }
    }
    
    
    
}