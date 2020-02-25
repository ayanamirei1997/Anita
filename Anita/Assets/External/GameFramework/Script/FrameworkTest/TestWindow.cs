using System;
using FairyGUI;
using Anita;
using System.Collections.Generic;
using ResourceManager;
using TestWindow.TestWindow;

namespace Anita
{
    public class TestWindow : GuiWindow
    {
        private bool _isBought;
        private int _rewardDay;

        private string _bgUrl;
        private const int WESTEROS_CALENDAR = 2;
        private UI_TestWindow _view;
        public override void InitConfig()
        {
            Config.res = "GUI/TestWindow";
            Config.package = "TestWindow";
            Config.window = "TestWindow";
            Config.style = WindowStyle.FixSize;
            //Config.layer = GotUILayer.Normal;
            //Config.useMask = true;
            Config.maskAlpha = 1;
        }

        protected override List<UICfg> CreateUICfgs()
        {
            return new List<UICfg>
            {
            };
        }

        protected override void OnInitWindow(GComponent root)
        {
            _view = root as UI_TestWindow;
        }

        protected override void BeforeShow(GuiWindowParam param)
        {
            base.BeforeShow(param);
        }

        protected override void OnShow()
        {
            base.OnShow();
            _view.m_btn1.onClick.Set(OnClick);
            AnitaDebug.Log("test window !");
            UpdateView();
        }

        private void UpdateView()
        {
         
        }

        private void OnClick()
        {
            AnitaGame.guiManager.ShowWindow<RealWindow>();
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
          ///  public override Type windowType => typeof(TestWindow);
        }
    }
}
