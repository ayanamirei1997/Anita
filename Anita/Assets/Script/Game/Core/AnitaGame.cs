using System;
using UnityEngine;

namespace Anita
{
    public class AnitaGame : GameBase
    {

#if UNITY_STANDALONE
        public static bool IS_PC_PUBLISH_MODEL = true;
#endif

        // 数据
        public static Data Data { get; private set; }

         public override string resServerUri
        {
            //get { return Data.SystemData.resServerUri; }
            get{return "Anita"; }
         }

        // 配置
        //public static AnitafigManager configManager { get; private set; }

        // 网络
        //public static AnitaNetworkManager networkManager { get; private set; }

        // ui
        public static AnitaGuiManager guiManager { get; private set; }

        // scene
        //public static AnitaSceneManager sceneManager { get; private set; }

        // 多语言
        //public static AnitaLanguageManager languageManager { get; private set; }

        //public static SpriteAnimationManager spriteAnimationManager { get; private set; }

        //public static AnitaAudioManager AudioManager { get; private set; }

        //public static UIDynamicEffectManager UIDynamicEffectManager { get; private set; }
        // 快捷键
        //public static ShortcutManager shortcutManager { get; private set; }

        //public static AnitaParticalManager ParticalManager { get; private set; }

        // UI点击限制
        //public static ClickLimitManager clickLimitManager { get; private set; }

        public AnitaGame()
        {
            Data = new Data();
            //ScreenScaleMonitor.Instance.SetDesignResolution(1366, 768);
        }

        protected override void OnFrameworkReady(ServerConf conf)
        {
            //Data.SystemData.serverConf = conf;

            InitSystem();
        }

        private void InitSystem()
        {
            Data.OnInit();
            // networkManager = new AnitaNetworkManager(gameFramework);
            // networkManager.Init(gameFramework.eventSystem);

            // languageManager = new AnitaLanguageManager(); // 优先初始化语言

             guiManager = new AnitaGuiManager(gameFramework, Data);
            // configManager = new AnitaConfigManager();

            // shortcutManager = new ShortcutManager();
            // clickLimitManager = new ClickLimitManager();
            //AnitaGame.guiManager.ShowWindow<TestWindow>();


            var sceneRoot = new GameObject("SceneRoot");
            sceneRoot.transform.parent = gameFramework.gameObject.transform;
            sceneRoot.SetActive(true);
            GameObject.Instantiate(sceneRoot, gameFramework.transform);

            // sceneManager = new AnitaSceneManager();
            // sceneManager.Init(sceneRoot);

            // spriteAnimationManager = new SpriteAnimationManager(gameFramework);

            // AudioManager = new AnitaAudioManager(gameFramework.gameObject.transform);
            // UIDynamicEffectManager = new UIDynamicEffectManager(gameFramework.gameObject.transform);
            // ParticalManager = new AnitaParticalManager(gameFramework);

            // languageManager.InitGameLanguage(WaitForFontReady);
            guiManager.Init();
          

#if SHOW_FPS
            //gameFramework.gameObject.AddComponent<FrameRateMonitor>();
#endif
        }

        private void WaitForFontReady()
        {
            // HitPointUtils.HitPointRequest(HitPoint.startLoadLanguage);
            // ResourceManager.ResourceMgr.Instance.StartCoroutine(CoWaitForFontReady());
        }

        private System.Collections.IEnumerator CoWaitForFontReady()
        {
            yield return new WaitUntil(delegate ()
            {
                return true;
                //guiManager.IsFontLoaded;
            });
            OnLanguageLoaded();
        }

        private void OnLanguageLoaded()
        {
            // try
            // {
            //     AudioInit.Instance.Initialize();//TODO 此处需要修改 传入语言类型
            // }catch(Exception e)
            // {
            //     AnitaDebug.LogException(e);
            // }
            
            // LoadingProgress.UpdateGameLoading(0);

            // HitPointUtils.HitPointRequest(HitPoint.loadConfigStart);

            // configManager.LoadAll(() =>
            // {
            //     HitPointUtils.HitPointRequest(HitPoint.loadConfigComplete);
            //     shortcutManager.Init();
            //     guiManager.LoadDefaultRes(OnGameStart);
            // }, (c, t) =>
            // {
            //     if (null != guiManager.DefaultCfgs)
            //     {
            //         t += guiManager.DefaultCfgs.Count;
            //     }
            //     LoadingProgress.UpdateGameLoading(c / t);
            // });
        }

        private void OnGameStart()
        {
            InitPlugins();
            //CameraController.Instance.InitController();
            //NetErrorUtility.Instance.init();
            // 模块数据初始化
            Data.OnInit();
            //BubbleUtility.Instance.Init();//红点数据初始化
            //Debug.Log("屏幕模式:" + Screen.fullScreenMode + "    " + Screen.fullScreen);
            //gameFramework.eventSystem.TriggerEvent(ClientEvent.GameStart);
        }

        private void InitPlugins()
        {
            Debug.Log("初始化逻辑插件");

            //InstallPlugin<BagPlugin>();

        }

        /// <summary>
        /// 刷新游戏
        /// </summary>
        public static void Refresh()
        {
            Debug.Log("刷新游戏");

            // if (guiManager != null)
            // {
            //     guiManager.HideWindow<MainUiView>();
            //     guiManager.Clear();
            // }

            // if (sceneManager != null)
            // {
            //     sceneManager.UnloadCurrentScene();
            // }

            // if (MainCityPlugin.instance != null)
            // {
            //     MainCityPlugin.instance.Reset();
            // }

            // if (BubbleUtility.Instance != null)
            // {
            //     BubbleUtility.Instance.Clear();
            //     BubbleUtility.Instance.Init();//红点数据初始化
            // }

            // UIManager.Clear();
            // if (Data != null)
            // {
            //     Data.Clear();
            // }

            // if (networkManager != null)
            // {
            //     networkManager.ResetNetwork();
            //     networkManager.OnLogin();
            // }
        }

        public override void Update()
        {
            int i = 1;
            if (guiManager != null)
            {
                guiManager.Update();
            }

            i++;
            if (i == 4)
            {
                // AnitaGame.guiManager.ShowWindow<TestWindow>();
            }
            
        }

        public override void OnFixedUpdate()
        {
            // if (sceneManager != null)
            // {
            //     sceneManager.OnFixedUpdate();
            // }
        }

        public override void OnScreenResize(float width, float height)
        {
            // if (guiManager != null)
            // {
            //     guiManager.Resize(width, height);
            // }
        }
    }

}