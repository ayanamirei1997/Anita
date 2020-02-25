using FairyGUI;
using Anita;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ResourceManager;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.Experimental.UIElements;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine.Experimental.UIElements;

namespace Anita
{
    public abstract class GuiWindow
    {
        public static int ALL_GUI_WINDOW_INSTANCE_COUNT = 0;            // 所有窗口实例总数
        
        public static List<string> s_gcWindowNameList = new List<string>();
        
        // 可见设置
        private bool m_IsVisibleLogic = false;         // 是否逻辑上可见(窗口可能因为加载原因没有显示)
        private bool m_IsVisibleRender = false;        // 是否渲染上可见(OnShow和OnHide之后)
        public GuiManager guiManager;
        public bool isVisibleLogic { get { return m_IsVisibleLogic; } set { m_IsVisibleLogic = value; } }
        public bool isVisibleRender { get { return m_IsVisibleRender;  } }
        
        // 顶层的窗口控件
        private GComponent m_RootComponent;
        public GComponent rootComponent { get { return m_RootComponent; } }        
        
        public float m_HideTime;            // 窗口隐藏时间
        
        // 一些回调
        public delegate void LoadingUpdateDelegate(GuiWindow win);
        public delegate void LoadingUpdatingDelegate(float current, float total);
        public event LoadingUpdateDelegate onLoadingFinished;
        public event LoadingUpdatingDelegate onLoadingUpdating;

        // 窗口配置
        protected GuiWindowConfig m_Config = new GuiWindowConfig();
        public GuiWindowConfig Config { get { return m_Config; } }
        
        // 设置ui相关数据的接口
        public abstract void InitConfig();

        
        /// <summary>
        /// fade
        /// </summary>
        /// <param name="root"></param>
        
//        private GoKitLite.Tween fadeInTween;
//        private GoKitLite.Tween fadeOutTween;
//        
        // 自定义的窗口初始化接口
        protected virtual void OnInitWindow(GComponent root) { }
        
        protected virtual void BeforeShow(GuiWindowParam param) {}
        protected virtual void OnShow() {}

        public virtual void Update() {}

        public virtual void SetScale(Vector2 vec2) {}
        
        protected virtual void afterHide() {}

        protected virtual void OnDispose() { }
        // 没有关闭直接重新放到最上层显示时会被调用
        public virtual void OnReShow(GuiWindowParam param)
        {
            //rootComponent.touchable = true;
        }

        /*
         * 仅当该window已经显示，并且该window上叠加的所有window关闭，再次看到该window时被调用
         * 注：window叠加时下层的window并没有被隐藏或销毁，一直在显示
         */
        public virtual void OnTurnShow(){}

        public virtual string GetWindowName() { return GetType().Name; }

        // 打开窗口后动态检测是否需要显示模糊背景
        public virtual bool CheckShowScreenTextureDynamic() { return true; }

        private Action _showComplete;
        
        // 设置UI相关的数据接口
        ////////////////////////////////////////////////////////////////////////
        public GuiWindow()
        {
            ALL_GUI_WINDOW_INSTANCE_COUNT++;
            InitConfig();
            ModifyUICfg();
        }

        private void ModifyUICfg()
        {
            if (string.IsNullOrEmpty(Config.package))
            {
                AnitaDebug.LogError("需要指定该window属于哪个包");
                return;
            }

            List<UICfg> uiCfgs = UILoadConfig.Instance.GetUICfg(GetType());
            if (uiCfgs == null)
            {
                uiCfgs = CreateUICfgs();
                if (uiCfgs == null)
                {
                    uiCfgs = new List<UICfg> { new UICfg { pkg = Config.package, path = Config.res == null ? "GUI/" + Config.package : Config.res } };
                }
                else
                {
                    uiCfgs.Add(new UICfg { pkg = Config.package, path = Config.res == null ? "GUI/" + Config.package : Config.res });
                }
                UILoadConfig.Instance.AddUICfg(GetType(), uiCfgs);
            }
            Config.cfgs = uiCfgs;
        }

        ~GuiWindow()
        {
            ALL_GUI_WINDOW_INSTANCE_COUNT--;
            AnitaDebug.LogFormat("窗口GC:{0}", GetWindowName());
            s_gcWindowNameList.Remove(GetWindowName());
        }

        protected virtual List<UICfg> CreateUICfgs()
        {
            return null;
        }

        public virtual void SetWindowVisible(bool isShow)
        {
            if (m_RootComponent != null)
                m_RootComponent.visible = isShow;
            OnwWindowVisibleChange(isShow);
        }

        public virtual void OnwWindowVisibleChange(bool isShow)
        {
            
        }
        
        
        // 窗口进行预加载
        public virtual void DoLoad()
        {
            DoLoadingCompleted();
        }
        
        // 资源加载进度完成
        protected void DoLoadingCompleted()
        {
            if (onLoadingFinished != null)
                onLoadingFinished(this);
        }
        
        // 资源加载进度更新
        protected void DoLoadingUpdate(float current, float total)
        {
            if (onLoadingUpdating != null)
            {
                onLoadingUpdating(current, total);
            }
        }
        
        // 窗口初始化
        public void InitWindow()
        {
            if (m_RootComponent != null)
                return;

            m_RootComponent = CreateComponent();
            OnInitWindow(m_RootComponent);

            if (m_RootComponent != null)
            {
                //Resize(GRoot.inst.width, GRoot.inst.height);
            }
        }
        
        // 创建UI组件的接口
        protected virtual GComponent CreateComponent()
        {
            UIPackage.AddPackage("GUI/TestWindow");
            var obj = UIPackage.CreateObject(Config.package, Config.window).asCom;
            if (obj != null)
            {
                //obj.name = Config.window;
                obj.name = GetType().Name;
            }
            else
            {
                AnitaDebug.AssertFormat(false, "创建窗口组件失败：{0} {1}", Config.package, Config.window);
            }
            obj.OnRootComponentCreate();
            return obj;
        }

        public virtual void Resize(float width, float height)
        {
            if (m_RootComponent == null)
            {
                return;
            }
            var type = GetType();
//            if (type != typeof(MainUiView) && type != typeof(MiniMapWindow) 
//                                           && type != typeof(CastleUpgradeWindow) && type != typeof(BigMapWindow) && type != typeof(ChooseServerNew)
//                                           && type != typeof(StoryDialogWindow) && type != typeof(SubTitlesWindow) 
//                                           && type != typeof(GuideWindow) && type != typeof(LoadingTransferWindow)
//                                           && type != typeof(ChatMainPanel) && type != typeof(ChatSettingWindow)
//                //&& type != typeof(DragonMainWindow)
//            )
//            {
//                width = ScreenScaleMonitor.Instance.RealWidth;
//                height = ScreenScaleMonitor.Instance.RealHeight;
//            }
            
            switch (Config.style)
            {
                case WindowStyle.FullScreen:
                {
                    //GuiManager.Zoom(m_RootComponent, width, height);
                    break;
                }
                case WindowStyle.FixSize:
                {
                    //m_RootComponent.scale = Vector2.one * ScreenScaleMonitor.Instance.ScaleFactor;
                    m_RootComponent.x = (int)((Screen.width - m_RootComponent.actualWidth)/2 );
                    m_RootComponent.y = (int)((Screen.height - m_RootComponent.actualHeight) / 2);
                    break;
                }
            }
        }
        
        
        
        
        
        public void Show(GuiWindowParam param, GComponent parent, Action showComplete)
        {
            _showComplete = showComplete;
            CancelFadeOut();

            if (m_RootComponent == null)
            {
                InitWindow();
                guiManager.OnWindowCreate(this);
            }
            
            guiManager.OnWindowShow(this);

            if (param != null && param.windowLoadedCallback != null)
            {
                param.windowLoadedCallback(this);
            }

            CancelFadeOut();

            if (m_IsVisibleRender)
            {
                AnitaDebug.Assert(m_IsVisibleLogic);
                AnitaDebug.Assert(m_RootComponent.displayObject.stage != null);
                AnitaDebug.Assert(m_IsVisibleRender);
                DoShowOnFront(param);
                if (_showComplete != null)
                {
                    _showComplete.Invoke();
                    _showComplete = null;
                }
            }
            else
            {
                AttachParent(parent);
                if (param != null && param.windowBeforeShowHandler != null)
                {
                    param.windowBeforeShowHandler(this);
                }
                
                AnitaDebug.Assert(m_IsVisibleLogic);
                AnitaDebug.Assert(m_RootComponent.displayObject.stage != null);
                AnitaDebug.Assert(m_IsVisibleRender);
                
                BeforeShow(param);

                if (param != null && param.windowOnShowHandler != null)
                {
                    param.windowOnShowHandler(this);
                }
                OnShow();

                DoFadeIn();
            }
        }

        private bool NeedFadeEff()
        {
            if (!Config.isEffectFade)
                return false;

            if (m_RootComponent == null)
                return false;

            if (Config.style == WindowStyle.FullScreen)
                return false;

            return true;
        }
        
                private void DoFadeIn()
        {
            if (!NeedFadeEff())
            {
                OnFadeInComplete();
                if (_showComplete != null)
                {
                    _showComplete.Invoke();
                    _showComplete = null;
                }
            }
            else
            {
                ClearFade();
                //fadeInTween = FadeUtil.FadeIn(m_RootComponent, FadeType.Top2Botton, OnFadeInComplete);
            }
        }

        private void OnFadeInComplete()
        {
            //fadeInTween = null;
            if (_showComplete != null)
            {
                _showComplete.Invoke();
                _showComplete = null;
            }
            ////打开界面时， 开启界面内的触碰事件
            //GotDebug.Assert(m_RootComponent != null);
            rootComponent.touchable = true;
        }

        private void DoFadeOut(Action<GuiWindow> onHide)
        {
            //窗口在隐藏期间，禁用鼠标事件  注意：此时鼠标是可以点击到窗口后面的组件或窗口
            rootComponent.touchable = false;
            if (!NeedFadeEff())
            {
                //DoHideInternal();
                if (onHide != null)
                {
                    onHide(this);
                }
                return;
            }

            // 开始渐隐消失
            //OnFadeOut();
            ClearFade();

//            fadeOutTween = FadeUtil.FadeOut(m_RootComponent, FadeType.Top2Botton, () =>
//            {
//                DoHideInternal();
//                if (onHide != null)
//                {
//                    onHide(this);
//                }
//            });
        }

        private void ClearFade()
        {
//            if (fadeInTween != null)
//            {
//                //FadeUtil.CancelFade(fadeInTween);
//                //fadeInTween = null;
//
//                if (m_RootComponent != null)
//                {
//                    m_RootComponent.alpha = 1;
//                }
//            }

            CancelFadeOut();
        }

        private void CancelFadeOut()
        {
//            if (fadeOutTween != null)
//            {
//                //FadeUtil.CancelFade(fadeOutTween);
//                fadeOutTween = null;
//
//                if (m_RootComponent != null)
//                {
//                    m_RootComponent.alpha = 1;
//                }
//            }
        }

        private void DoShowOnFront(GuiWindowParam param)
        {
            if (m_RootComponent != null && m_RootComponent.parent != null)
            {
                m_RootComponent.parent.AddChildAt(m_RootComponent, m_RootComponent.parent.numChildren);
                OnReShow(param);
            }
        }
        
        public void Show()
        {
            if (guiManager != null)
            {
                guiManager.ShowWindow(GetType(), null);
            }
        }
        
        public void Hide()
        {
            guiManager.HideWindow(GetType());
        }
        
                public virtual void OnClickMask()
        {
            if (rootComponent != null)
            {
                if (rootComponent.touchable)
                {
                    Hide();
                }
            }
        }

        public void DoHide(Action<GuiWindow> onHide)
        {
            DoFadeOut(onHide);
        }

        public void DoHideImmediatelly(Action<GuiWindow> onHide)
        {
//            if (OuterCityScene.Instance == null || !OuterCityScene.Instance.IsPlayingCameraTimeline)
//            {
//                GameBase.eventManager.TriggerEvent(ClientEvent.changeMainUIState, true);
//            }
           
            DoHideInternal();
            if (onHide != null)
            {
                onHide(this);
            }
        }

        private void DoHideInternal()
        {
            //fadeOutTween = null;
            //窗口在真正隐藏后，重启鼠标事件
            rootComponent.touchable = true;
            if (!m_IsVisibleRender)
            {
                AnitaDebug.LogFormat("GuiWindow::DoHide, 窗口并非显示状态，类型{0}", GetType().ToString());
                return;
            }

            AnitaDebug.Assert(!m_IsVisibleLogic);

            guiManager.OnWindowHide(this);

            // 通知特定组件，窗口关闭
            TellComponentHideWindow(m_RootComponent);

            OnHide();       // 从父节点移除
            afterHide();    // window清理工作

            // 隐藏界,禁用界面内的触碰事件
            //GotDebug.Assert(m_RootComponent != null);
            //m_RootComponent.touchable = false;

        }

        private void TellComponentHideWindow(GComponent rootComponent)
        {
            AnitaDebug.Assert(rootComponent != null);

            var numChildren = rootComponent.numChildren;
            int i = 0;
            while(i < numChildren)
            {
                GObject child = rootComponent.GetChildAt(i);

                // 角色展示窗口通知
//                var _3dModel = child as Got.commomPC.UI_Com_lord_model;
//                if(_3dModel != null)
//                {
//                    _3dModel.OnHideWindow();
//                }

                // recursive
                var childCom = child as GComponent;
                if(childCom != null)
                {
                    TellComponentHideWindow(childCom);
                }

                i++;
            }
        }

        protected virtual void OnFadeOut()
        {

        }

        public virtual void OnHide()
        {
            if (m_RootComponent != null)
            {
                m_RootComponent.RemoveFromParent();
                m_IsVisibleRender = false;
            }
            else
            {
                AnitaDebug.Assert(false);        // 不应该被重复调用
            }
        }

        private void UpdateTouchable()
        {

        }

        public int GetIndex(GComponent com)
        {
            if (m_RootComponent == null || m_RootComponent.parent == null || m_RootComponent.parent != com)
            {
                return -1;
            }

            return m_RootComponent.parent.GetChildIndex(m_RootComponent);
        }

        protected void AttachParent(GComponent parent)
        {
            if (parent == null || m_RootComponent == null)
            {
                return;
            }
            parent.AddChild(m_RootComponent);
            m_IsVisibleRender = true;
            //Resize(GRoot.inst.width, GRoot.inst.height);
            //            var parentWidth = ScreenScaleMonitor.Instance.RealWidth;
            //            var parentHeight = ScreenScaleMonitor.Instance.RealHeight;
            //            if (!m_RootComponent.width.IsEqual(parentWidth)  || !m_RootComponent.height.IsEqual(parentHeight))
            //            {
            //                Resize(parentWidth, parentHeight);
            //            }
        }
        
        
        
        

        public void Dispose()
        {
            OnDispose();

            AnitaGame.guiManager.UninstallWindow(this);
            
            // 销毁UI组件
            AnitaDebug.Assert(m_RootComponent.parent == null) ; // 应该已经从父节点拿下
            m_RootComponent.Dispose();
            m_RootComponent = null;
            
            s_gcWindowNameList.Add(GetWindowName());
        }
        
        public virtual Vector2 GetWindowSize()
        {
            if (m_RootComponent != null)
            {
                return m_RootComponent.size;
            }
            return Vector2.zero;
        }

    }
}
