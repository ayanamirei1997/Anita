// Game.cs
// 游戏常规启动入口对象
// 

// using LuaInterface;
// using MxLib;
// using Newtonsoft.Json;
using System;
using System.Collections;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace Client
{
    /// <summary>
    /// The game entry object
    /// </summary>
    public class Game : MonoBehaviour
    {
        /// <summary>
        /// Frame update event
        /// </summary>
        public event Action<float> UpdateEvent;

        /// <summary>
        /// Console (Lua/cs) prefab
        /// </summary>
        public GameObject ConsolePrefab;

        /// <summary>
        /// 全局Canvas
        /// </summary>
        public Canvas Canvas;

        /// <summary>
        /// window控制台
        /// </summary>
        public GameObject Win32Console;

        public static Game Instance
        {
            get;
            set;
        }

        /// <summary>
        /// lua启动脚本
        /// </summary>
        public string BootScript = "Boot.lua";

        /// <summary>
        /// 判断是否为了调试而启动（并非启动场景开启）
        /// </summary>
        public static bool StartupForTest
        {
            get { return Game.Instance == null; }
        }

        /// <summary>
        /// This frame elapsed time
        /// </summary>
        public float FrameTime
        {
            get;
            private set;
        }

        void PrintSystemInfo()
        {
            Debug.LogFormat("dataPath = {0} ", Application.dataPath);
            Debug.LogFormat("persistentDataPath = {0} ", Application.persistentDataPath);
            Debug.LogFormat("streamingAssetsPath = {0} ", Application.streamingAssetsPath);
            Debug.LogFormat("temporaryCachePath = {0} ", Application.temporaryCachePath);
        }

        /// <summary>
        /// Unity awake
        /// </summary>
        void Awake()
        {
            // 显示loading界面
            //Loading.SetActive(true);
            PrintSystemInfo();
            Instance = this;

            // 初始化全局模块
            // Instantiate(GlobalModules);

            // 不要删除自己
            // DontDestroyOnLoad(gameObject);

            // 进行一些Unity引擎参数的初始化
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            // 开启多点触控
            Input.multiTouchEnabled = true;
            
            // 增加各种管理器
            // this.gameObject.AddComponent<TelnetManager>();
            // this.gameObject.AddComponent<MoveManager>();

            // 增加AudioManager
            //this.gameObject.AddComponent<AudioManager>();

            // 创建控制台
            CreateConsole();

            // 切换LoadingCanvas相机
            SwitchUICamera();
        }

        void OnApplicationPause(bool pause)
        {
            Debug.LogFormat("ApplicationPause {0}", pause);
            //EventManager.FireScriptEvent("EVT_APP_PAUSE", pause.ToString());

        }

        void OnApplicationFocus(bool hasFocus)
        {
            Debug.LogFormat("ApplicationFocus {0}", hasFocus);
            //EventManager.FireScriptEvent("EVT_APP_FOCUS", hasFocus.ToString());
        }
        void OnApplicationQuit()
        {
            Debug.Log("ApplicationQuit{0}");
            //EventManager.FireScriptEvent("EVT_APP_QUIT", "");
        }

        IEnumerator Start()
        {
            // 加载启动脚本
            //yield return LuaManager.Instance.BootLua(BootScript);
            yield return null;
        }

        void Update()
        {
            float deltaTime = Time.deltaTime;
            this.FrameTime = deltaTime;
            if (UpdateEvent != null)
                UpdateEvent(deltaTime);
        }

        /// <summary>
        /// Get specified type of manager
        /// </summary>
        public T GetManager<T>()
        {
            return this.gameObject.GetComponent<T>();
        }

        /// <summary>
        /// 重置整个游戏
        /// </summary>
        public static void ResetGame()
        {
            // Loading.SetActive(true);
            // Loading.SetLoadingText("正在加载...");
            // Loading.SetProgress(0);
            Launcher.Instance.Reboot();
        }

        private static Dictionary<string, string> _config = new Dictionary<string, string>();
        public static void SetConfig(string key, string str)
        {
            _config[key] = str;
        }

        public static void DelConfig(string key)
        {
            _config.Remove(key);
        }
        
        public static string GetConfig(string key)
        {
            if (_config.ContainsKey(key))
                return _config[key];
            return null;
        }

        /// <summary>
        /// 所有Entity的根节点的transform
        /// </summary>
        public static Transform EntityRoot
        {
            get {
                //return GlobalObjects.Instance.EntityRoot;
                return null;
            }
        }

        public static GameObject MainCanvas
        {
            get { return Game.Instance.Canvas.gameObject; }
        }

        /// <summary>
        /// 创建游戏中的控制台对象
        /// </summary>
        void CreateConsole()
        {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            if (GameObject.Find("Win32Console") == null)
            {
                var console = GameObject.Instantiate(this.Win32Console);
                console.name = "Win32Console";
            }
#endif
        }

        private void OnDestroy()
        {
            // 退出关闭所有连接
            //NewNetworkManager.CloseAll();
        }

        void SwitchUICamera()
        {
            // 切换LoadingCanvas和BgCanvas相机
            var loadingCanvasOb = GameObject.Find("LoadingCanvas");
            var loadingCanvas = loadingCanvasOb.GetComponent<Canvas>();

            var uiCamera = GameObject.Find("UICamera").GetComponent<Camera>();

            loadingCanvas.worldCamera = uiCamera; //Camera.main;
            loadingCanvas.sortingLayerName = "UIEffect";

            var bgCanvasOb = GameObject.Find("BgCanvas");
            var bgCanvas = bgCanvasOb.GetComponent<Canvas>();

            bgCanvas.worldCamera = uiCamera; //Camera.main;
            bgCanvas.sortingLayerName = "UIBack";


            var loadSceneCanvasOb = GameObject.Find("LoadSceneCanvas");
            var loadSceneCanvas = loadSceneCanvasOb.GetComponent<Canvas>();
            loadSceneCanvas.worldCamera = uiCamera; //Camera.main;
            loadSceneCanvas.sortingLayerName = "UIEffect";
        }
    }
}
