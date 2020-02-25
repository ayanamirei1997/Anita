
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anita
{
    public class LogicPluginBase
    {
        private GameFrameworkEx gameFramework;

        public EventSystem EventSystem
        {
            get { return gameFramework.eventSystem; }
        }


        public virtual void Init(GameBase game, GameFrameworkEx gf)
        {
            gameFramework = gf;
            OnInstall();
        }

        public virtual void Release()
        {
            OnUninstall();
        }


        protected virtual void OnInstall()
        {
        }

        protected virtual void OnUninstall()
        {
        }
    }
}
