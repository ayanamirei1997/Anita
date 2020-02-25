using System;
using FairyGUI;
using Anita;
using System.Collections.Generic;
using ResourceManager;

namespace Anita
{
    public class RealWindow : GuiWindow
    {
        private bool _isBought;
        private int _rewardDay;

        private string _bgUrl;
        private const int WESTEROS_CALENDAR = 2;


        public override void InitConfig()
        {
            Config.res = "GUI/TestWindow";
            Config.package = "TestWindow";
            Config.window = "RealWindow";
            Config.style = WindowStyle.FixSize;
            //Config.layer = GotUILayer.Normal;
            //Config.useMask = true;
            Config.maskAlpha = 0;
        }

        protected override List<UICfg> CreateUICfgs()
        {
            return new List<UICfg>
            {
            };
        }

        protected override void OnInitWindow(GComponent root)
        {
        }

        protected override void BeforeShow(GuiWindowParam param)
        {
            base.BeforeShow(param);
        }

        protected override void OnShow()
        {
            base.OnShow();
            AnitaDebug.Log("Real window !");
            UpdateView();
        }

        private void UpdateView()
        {
          
        }
        
      

        public override void OnHide()
        {
            base.OnHide();
        }

        protected override void afterHide()
        {
            base.afterHide();

        }

        public class ShowViewParam : GuiWindowParam
        {
            public override Type windowType => typeof(RealWindow);
        }
    }
}
