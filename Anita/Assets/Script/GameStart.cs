
using System.Collections;
using UnityEngine;

using Resources = UnityEngine.Resources;
using System.IO;
using System;
using Anita;
namespace Anita
{
    public class GameStart : MonoBehaviour
    {
        public static GameObject TaskSchedule;
        public GameObject GoKitLite;
        public GameObject UIAnimator;
        public GameObject SupportNode;
        public DateTime enterBackGroudTime;

        private AnitaGameConfig gameConfig; //游戏配置
        public bool LogEnabled;     //本地开关
        public bool IsDebug;        //是否为测试版本

#if UNITY_EDITOR
        public bool AB_MODE;        //是否为ab包模式
        public bool IsShowUIPath = true; //显示UI路径信息
#endif

        public bool IsProfiler = false;     // 打开游戏内调试器
        
        private bool useOutClientJson = false;

        void Awake()
        {
            AnitaDebug.Init(); // 初始化Log管理器
            AnitaDebug.Log("GameStart执行Awake!");
            enabled = true;
            
#if UNITY_EDITOR
            //ResourceManager.ResourceMgr.AB_MODE = AB_MODE;
#endif
            TryLoadGameConfig();

            Debug.unityLogger.logEnabled = LogEnabled;
            AnitaDebug.SetDebugEnabled(DebugType.Normal, LogEnabled);
            AnitaDebug.SetDebugEnabled(DebugType.Limit, IsDebug);
            Debug.Log("fullScreen:" + Screen.fullScreen + "   " + Screen.fullScreenMode);
        }

        private void TryLoadGameConfig()
        {
            gameConfig = LoadConfigFromWin();
            if (gameConfig == null)
            {
                return;
            }
            Debug.Log("使用外部的client.json");
            this.useOutClientJson = true;
        }

        void Start()
        {
            AnitaDebug.Log("GameStart执行Start!");
            //Debug.developerConsoleVisible = false;
            AnitaDebug.Log("程序开始运行");
            AddSupportComponent();
            StartCoroutine(StartFramework());
           

        }
        
        void Update()
        {
        }
        
        // 添加支持组件
        private void AddSupportComponent()
        {
            // UWA测试
            //UWAEngine.StaticInit();
            //UWAEngine.Start(UWAEngine.Mode.Overview);
            //UWAEngine.Start(UWAEngine.Mode.Mono);
            //UWAEngine.Start(UWAEngine.Mode.Assets);

            // 调试器节点
            var ProfilerGO = GameObject.Find("Profiler");
            if(ProfilerGO != null)
            {
                //GotProfile.GotProfiler.m_GameObject = ProfilerGO;
                ProfilerGO.SetActive(IsProfiler);
            }

            // 支持组件
            SupportNode = new GameObject("Support");
            SupportNode.transform.parent = gameObject.transform;

            // 设备管理
            GameObject SystemAgent = new GameObject("Agent");
            SystemAgent.transform.parent = SupportNode.transform;
            //SystemAgent.AddComponent<Agent.DeviceAgent>();

            // Agent.DeviceAgent.Instance.RegisterAction((Agent.QualityEnum quality) =>
            // {
            //     CommonUtility.SetPlayerData("gameQuality", (int)quality + "", true);
            // });

            //             var saveQuality = CommonUtility.GetPlayerData("gameQuality", true);
            //             if (string.IsNullOrEmpty(saveQuality))
            //             {
            // #if UNITY_EDITOR || UNITY_STANDALONE
            //                 Agent.DeviceAgent.Instance.ChangeQuality(Agent.QualityEnum.High);
            // #elif UNITY_WEBGL
            //                 Agent.DeviceAgent.Instance.ChangeQuality(Agent.QualityEnum.Middle);
            // #endif
            //             }
            //             else
            //             {
            //                 int saveValue = Tool.Util.ConvertString2Int(saveQuality);
            //                 Agent.DeviceAgent.Instance.ChangeQuality((Agent.QualityEnum)saveValue);
            //             }

            GameObject HitPoint = new GameObject("HitPoint");
            HitPoint.transform.parent = SupportNode.transform;
            //SystemAgent.AddComponent<PerformanceHitPoint>();

            //TaskSchedule辅助组件
            TaskSchedule = new GameObject("TaskSchedule");
            TaskSchedule.transform.parent = SupportNode.transform;

            //GoKitLite组件
            GoKitLite = new GameObject("GoKitLite");
            //GoKitLite.AddComponent<GoKitLite>();
            GoKitLite.transform.parent = SupportNode.transform;
            
            //EditorOnly节点

            //UIPathPicker辅助组件
            GameObject EditorOnly = new GameObject("EditorOnly");
            EditorOnly.transform.parent = SupportNode.transform;
            if (IsShowUIPath)
                //EditorOnly.AddComponent<UIPathPicker>();

            UIAnimator = new GameObject("UIAnimator");
            //UIAnimator.AddComponent<UIAnimator>();
            UIAnimator.transform.parent = SupportNode.transform;

           
            //后处理效果节点
            GameObject PostEffectsNode = new GameObject("PostEffects");
            PostEffectsNode.transform.parent = gameObject.transform;
            //PostEffectsNode.AddComponent<EffectSwitch>();
        }
        
        // 初始化框架
        private IEnumerator StartFramework()
        {           
            yield return LoadClientConfig();
            var game = new AnitaGame();
            //AnitaGame.Data.UpdateClientConfig(gameConfig);
            var framework = GameFrameworkEx.Create();
            framework.Run(game);
            Debug.Log("游戏启动");

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IEnumerator LoadClientConfig()
        {
            //LoadConfigFromWin();

            if (gameConfig != null)
            {
                AnitaDebug.SetDebugEnabled(DebugType.Normal, gameConfig.LogEnabled);
                AnitaDebug.SetDebugEnabled(DebugType.Limit, gameConfig.DebugMode);
            }
            yield return 0;
        }

        private AnitaGameConfig LoadConfigFromLocal()
        {
            var text = Resources.Load<TextAsset>("client");
            return JsonUtility.FromJson<AnitaGameConfig>(text.text);
        }


        private AnitaGameConfig LoadConfigFromWin()
        {
            var path = Application.dataPath + "/../client.json";
            AnitaDebug.Log("尝试从本地加载配置文件:" + path);
            if (System.IO.File.Exists(path))
            {
                var str = System.IO.File.ReadAllText(path);
                return JsonUtility.FromJson<AnitaGameConfig>(str);
            }
            return null;
        }
        
        public void UpdateClientConfig(AnitaGameConfig cfg)
        {
            if (gameConfig == null)
            {
                gameConfig = cfg;
            }
            else
            {
                gameConfig.appVersion = cfg.appVersion;
                gameConfig.configVersion = cfg.configVersion;
            }
            Debug.LogFormat("appVersion:{0}  ResVersion:{1}  ExcelVersion:{2}", gameConfig.appVersion, gameConfig.resVersion, gameConfig.configVersion);
            
        }


        
        
    }
}
