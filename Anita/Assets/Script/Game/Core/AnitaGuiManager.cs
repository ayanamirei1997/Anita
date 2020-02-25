using Boo.Lang;
using FairyGUI;

using UnityEngine;
using ResourceManager;
using TestWindow.TestWindow;

namespace Anita
{
    public class AnitaGuiManager : GuiManager
    {
//        private LoadCompleteHandler loadDefaultResCompleteHandler;
//        private LoadingTransferWindow m_LoadingWindow;          // 全屏loading窗
//        private GotWindowMaskBG bgMask;

        private const int MAX_RANDOM_COUNT = 3;
        private System.Random random;
        private int tipsCount = -1;
        private int randomCount;            //随机次数


        private int m_SceneLoadFileTotal = -1;                // 加载场景总文件数

        // 公共UI资源
        public readonly List<UICfg> DefaultCfgs = new List<UICfg>
        {
        };

        private BaseFont m_defaultFont;
        

        private const bool IS_USE_DEFAULT_FONT = false; // 使用老字体

        private const string GLOBAL_FONT = IS_USE_DEFAULT_FONT? "default" : "Remote:fonts/notosans_medium";//"default";//"Remote:NotoSans-Medium";

        private const string PRELOAD_FONT = IS_USE_DEFAULT_FONT? "default" : "default_combine";// 预加载字体

        private bool m_isUsePreloadFont;

        private bool m_showLoadTips;

        public AnitaGuiManager(GameFrameworkEx fw, Data data) : base(fw)
        {
            InitBinding();

            //初始化全局字体
            //UIConfig.defaultFont = "方正正准黑简体";

            var lang = PlayerPrefs.GetInt("GotUserLang", 1);
            m_isUsePreloadFont = IsNeedPreloadFont(lang);
            if (m_isUsePreloadFont)
            {
                UIConfig.defaultFont = PRELOAD_FONT;
                m_defaultFont = FontManager.GetFont(PRELOAD_FONT);
                m_defaultFont.customBold = true;
            }
            else
            {
                UIConfig.defaultFont = GLOBAL_FONT;
                m_defaultFont = FontManager.GetFont(GLOBAL_FONT);
                m_defaultFont.customBold = true;
            }

            UIConfig.renderingTextBrighterOnDesktop = true;
        }
        

        // 非CJK地区的语言，优先加载本地字体
        private bool IsNeedPreloadFont(int lang)
        {
            if (IS_USE_DEFAULT_FONT)
            {
                return false;
            }
            return false;
            //return lang != 4 && lang != 5 && lang != 11 && lang != 12;
        }

        private void OnMouseWheel(Event evt)
        {
            //if (evt.type == EventType.ScrollWheel)
            //{
            //    if (GotGame.sceneManager.IsInScene(GotSceneType.OuterCity))
            //    {
            //        GotGame.eventManager.TriggerEvent<float>(ClientEvent.ViewChange, evt.delta.y);
            //    }
            //}
        }


        // UI扩展类型注册
        private void InitBinding()
        {
            // 初始化UI绑定 请按字母顺序放置
         TestWindowBinder.BindAll();
         
        }


        // 加载通用UI资源
//        public void LoadDefaultRes(LoadCompleteHandler handler)
//        {
//            Debug.Log(" load default ui resources ");
//            loadDefaultResCompleteHandler = handler;
//            ResourceFunc.ReadUIList(DefaultCfgs, OnDefaultResLoaded, (c, t) =>
//            {
//                c += GotGame.configManager.TotalCount;
//                t += GotGame.configManager.TotalCount;
//                LoadingProgress.UpdateGameLoading(c / t);
//            });
//        }

//        private void OnDefaultResLoaded()
//        {
//            HitPointUtils.HitPointRequest(HitPoint.loadDefaultRes);
//            Init();
//
//            if (loadDefaultResCompleteHandler != null)
//            {
//                loadDefaultResCompleteHandler();
//                loadDefaultResCompleteHandler = null;
//            }
//
//#if UNITY_WEBGL && !UNITY_EDITOR
//            var loginToken = ChannelSDKManager.GetLoginToken();
//            if (loginToken != null && loginToken == "\"testModel\"")
//            {
//                
//            }
//            else
//            {
//                GotGame.guiManager.StartLoading();
//            }
//#endif
//            LoadingProgress.UpdateGameLoading(1);
//        }

        public void Init()
        {
            AnitaDebug.Log("初始化UI系统");

            GRoot root = GRoot.inst;
            root.displayObject.parent.gameObject.name = "FairyGUI";
            root.displayObject.gameObject.name = "UIRoot";
            
            AnitaGame.guiManager.ShowWindow<TestWindow>();
#if UNITY_EDITOR
            // 控件名称显示
            //root.displayObject.parent.gameObject.AddComponent<UIPathPicker>();
#endif

            // 默认tooltip控件
            //IConfig.tooltipsWin = "ui://CommonUI/publicButton1";

//            InitLayer();
//            InitMask();
//
//            m_WindowLoader = new GotWindowLoader(this);
//            m_WindowLoader.onWindowReady += InternalShowWindow;
//
//            //边缘填充底纹
//            var layer = FindLayer(GotUILayer.LayerEnd);
//            if (layer != null)
//            {
//                BorderFrameManager.Instance.Init(layer.layerRoot);
//            }
//            //资源条
//            ResourcePanel = UI_ResourcePanel.CreateInstance();
//            layer = FindLayer(GotUILayer.TopResource);
//            if (layer != null)
//            {
//                ResourcePanel.Init(layer.layerRoot);
//                UIManager.AddUIToCache("ResourcePanel", ResourcePanel, 0, false);
//            }
        }

        private void InitLayer()
        {
            // 请注意创建顺序才是真正决定层级顺序
//            AddLayer(GotUILayer.BackGroup, "BackGroup");
//            AddLayer(GotUILayer.Main, "Main");
//            AddLayer(GotUILayer.Normal, "Normal");
//            AddLayer(GotUILayer.TopResource, "TopResource");
//            AddLayer(GotUILayer.LoadingMask, "LoadingMask");
//            AddLayer(GotUILayer.PopLayer, "PopLayer");
//            AddLayer(GotUILayer.Guide_Forbiden, "GuideForbiden");
//            AddLayer(GotUILayer.Loading, "Loading");
//            AddLayer(GotUILayer.Guide, "Guide");
//            AddLayer(GotUILayer.TopMost, "Topmost");
        }

//        private void InitMask()
//        {
//            BgMask = new GotWindowMaskBG(this);
//            var mask = new GotWindowMask(this);
//            mask.tooltips = GameString.BackToLast;
////            mask.RegistEvent();
//            mask.onClickAction = MaskAction.CloseWindow;
//            BgMask.MaskComponent.gameObjectName = "Normal_BgMask";
//            mask.maskComponent.gameObjectName = "Normal_LayerMask";
//            SetLayerMask(GotUILayer.Normal, mask, BgMask);
//
//            var guideBgMask = new GotWindowMaskBG(this);
//            var guideMask = new GotWindowMask(this);
////            guideMask.RegistEvent();
//            guideBgMask.MaskComponent.gameObjectName = "Guide_BgMask";
//            guideMask.maskComponent.gameObjectName = "Guide_LayerMask";
//            SetLayerMask(GotUILayer.Guide, guideMask, guideBgMask);
//
//            var popMask = new GotWindowMask(this);
////            popMask.RegistEvent();
//            popMask.onClickAction = MaskAction.Nothing;
//            popMask.maskComponent.gameObjectName = "PopLayer_LayerMask";
//            SetLayerMask(GotUILayer.PopLayer, popMask, null);
//
//            var normalTopMask = new GotWindowMask(this);
////            normalTopMask.RegistEvent();
//            normalTopMask.onClickAction = MaskAction.CloseWindow;
//            normalTopMask.maskComponent.gameObjectName = "TopResource_LayerMask";
//            SetLayerMask(GotUILayer.TopResource, normalTopMask, null);
//        }
//
//        public void SetBgMaskVisible(bool visible)
//        {
//            this.BgMask.MaskComponent.visible = visible;
//        }

//        // 加载主场景ui资源
//        public void LoadMainRes(LoadCompleteHandler handler)
//        {
//            // 此处不要再加载任何UI包了，资源加载放在每个window里加载
//            List<UICfg> cfgs = new List<UICfg>
//            {
//                new UICfg { pkg = "SkillIcon", path = "GUI/SkillIcon" },
//                new UICfg { pkg = "CityUI", path = "GUI/CityUI" },
//                new UICfg { pkg = "CommonUI", path = "GUI/CommonUI" } ,
//                new UICfg { pkg = "BadgeCom" , path = "GUI/BadgeCom"},
//                new UICfg { pkg = "SoldierHead", path = "GUI/SoldierHead" },
//                new UICfg { pkg = "commomPC",path="GUI/commomPC" },
//            };
//
//            ResourceFunc.ReadUIList(cfgs, () =>
//            {
//                if (handler != null)
//                {
//                    handler();
//                }
//            });
//        }
//
//        // 加载主场景ui资源
//        public void LoadMainResLate()
//        {
//            //此处不要再加载任何UI包了，资源加载放在每个window里加载
//            List<UICfg> cfgs = new List<UICfg>
//            {
//                new UICfg { pkg = "ItemIcon", path = "GUI/ItemIcon" },
//                new UICfg { pkg = "CommonIcon_Rank", path = "GUI/CommonIcon_Rank" },
//            };
//
//            GotDebug.Log("加载主场景ui资源");
//
//            ResourceFunc.ReadUIList(cfgs, () =>
//            {
//                GotGame.eventManager.TriggerEvent(ClientEvent.LoadItemIconEnd);
//            });
//        }
//
//        public void BackToMainMenu()
//        {
//            var layer = FindLayer(GotUILayer.Normal);
//            if (layer != null)
//            {
//                layer.HideAll();
//            }
//        }

        public override void OnWindowCreate(GuiWindow win)
        {
            base.OnWindowCreate(win);
            //UIManager.RegisterBubble(win.Config.window, win.rootComponent, 0);
        }

        public override void OnWindowShow(GuiWindow win)
        {
            base.OnWindowShow(win);
            //UIManager.AddUIToCache(win.Config.window, win.rootComponent, 0, false);
        }

        public override void OnWindowHide(GuiWindow win)
        {
            base.OnWindowHide(win);
            //UIManager.RemoveUIFromCache(win.Config.window);
        }

        public override void OnWindowDispose(GuiWindow win)
        {
            base.OnWindowDispose(win);
            //UIManager.UnregisterBubble(win.Config.window);
        }

        private float m_StartLoadingTime = -1;
        //private LoadingTransferWindow.LoadingTransferWindowParam loadingParam = null;
        public void StartLoading(int fileMax = -1, bool showLoadTips = false)
        {
           // GotDebug.Log("显示Loading界面");
            
#if UNITY_STANDALONE_OSX
            StartLoadWindow.Instance.Hide(() =>
            {
                GotDebug.Log("关闭游戏启动loading页");
            });
#endif
            
            m_SceneLoadFileTotal = fileMax;

            m_StartLoadingTime = Time.time;
            m_showLoadTips = showLoadTips;

//            if (loadingParam == null)
//            {
//                loadingParam = new LoadingTransferWindow.LoadingTransferWindowParam();
//            }
//            loadingParam.showLoadingTips = showLoadTips;
//            loadingParam.beforeShowAction = OnLoadingWindowLoaded;
//            ShowWindow(typeof(LoadingTransferWindow), loadingParam, false);
        }

//        private void OnLoadingWindowLoaded(LoadingTransferWindow win)
//        {
//            GotDebug.Assert(m_StartLoadingTime != -1);
//            if (m_LoadingWindow == null)
//            {
//                m_LoadingWindow = win;
//                win.fileTotal = m_SceneLoadFileTotal;
//                GotDebug.LogFormat("设置加载总进度 {0}", m_SceneLoadFileTotal);
//            }
//            else
//            {
//                GotDebug.LogWarning("已经有一个loading页了，请检查调用是否正确 ");
//                GotDebug.Assert(false);
//            }
//
//            if (m_LoadingWindow != null && m_showLoadTips)
//            {
//                win.UpdateLoadTips();
//            }
//        }

        public void StopLoading()
        {
//            GotDebug.Log("隐藏Loading界面");
//            if (m_StartLoadingTime > Time.time)
//            {
//                GotDebug.Assert(m_LoadingWindow != null);
//                m_StartLoadingTime = -1;
//            }
//
//            if (m_StartLoadingTime != -1)
//            {
//                PerformanceHitPoint.Instance.LoadingTimeList.Add(Time.time - m_StartLoadingTime);
//                m_StartLoadingTime = -1;
//            }
//
//            if (m_LoadingWindow != null)
//            {
//                // 让LoadingWindow跑完进度条
//                m_LoadingWindow.showComplete(() =>
//                {
//                    GotDebug.Log("关闭Loading界面");
//                    if (m_LoadingWindow != null)
//                    {
//                        m_LoadingWindow.Hide();
//                        m_LoadingWindow = null;
//                    }
//                    GameBase.eventManager.TriggerEvent(ClientEvent.scene_change_finish);
//                });
//            }
//            else
//            {   // 没有显示loading窗口
//                GameBase.eventManager.TriggerEvent(ClientEvent.scene_change_finish);
//                GotDebug.LogWarning("LoadingWindow is null");
//            }
        }

        // 加速进度条播放
        public void StopLoadingImmediately()
        {
//            if (m_LoadingWindow != null)
//            {
//                m_LoadingWindow.stepValue = 0.5f;
//            }
        }

        public void Clear()
        {
//            GotDebug.Log("GuiManager清空资源");
//
//            foreach (var layer in m_Layers.Values)
//            {
//                layer.HideAll();
//            }

            var list = new List<GuiWindow>(m_WindowInstances.Values);
            foreach (var window in list)
            {
                AnitaDebug.Log("window: " + window.Config.window + " 清空资源 ");
                UninstallWindow(window, true);
            }

            UnLoadUI();
        }

        public void UnLoadUI()
        {
            // 释放UI的pkg资源
            foreach (var notUnloadedRes in notUnloadedResDic)
            {
                //ResourceFunc.UnLoadUI(notUnloadedRes.Key, notUnloadedRes.Value);
            }
        }

        // 转菊花
        //private Common.UI_smallLoading loadingComponent;
        public void NetLoadingVisible(bool visible = false)
        {
//            if (visible)
//            {
//                if (loadingComponent == null)
//                {
//                    loadingComponent = Common.UI_smallLoading.CreateInstance();
//                }
//                loadingComponent.SetSize(GRoot.inst.width, GRoot.inst.height);
//                loadingComponent.visible = true;
//                this.ShowObject(loadingComponent, GotUILayer.TopMost, true);
//            }
//            else
//            {
//                if (loadingComponent != null)
//                {
//                    loadingComponent.visible = false;
//                }
//            }
        }

        public string GetRandowTips()
        {
//            if (tipsCount < 0)
//            {
//                tipsCount = GotGame.configManager.loading_tipsConfig.GetId(2).items.Count;
//                random = new System.Random();
//            }
//            int tipsId = random.Next(0, tipsCount);
//            int level = GotGame.Data.BuildData.GetMaxLevelBuild(BuildingConfig.CASTLE).GetLevel();
//            randomCount = 0;
//            Loading_tips config;
//            while (randomCount < MAX_RANDOM_COUNT)
//            {
//                config = GotGame.configManager.loading_tipsConfig.Get(2, (uint)tipsId + 1);
//                if (config.castle_lv <= level)
//                {
//                    return LangUtil.Str(config.tips_text);
//                }
//                randomCount++;
//            }
            return "";
        }
    }
}
