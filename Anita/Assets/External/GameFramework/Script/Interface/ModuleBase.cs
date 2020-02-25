using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anita
{
    // 框架模块的基类
    public class ModuleBase
    {
        protected GameFrameworkEx gameFramework;

        public ModuleBase(GameFrameworkEx fw, ManagerType type)
        {
            gameFramework = fw;
        }

    }
}
