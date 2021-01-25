// Launcher.cs
// 启动器组件，全世界最先初始化的组件

using Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using LuaInterface;

namespace Client {
    public class Launcher : MonoBehaviour
    {
        public static Launcher Instance { get; private set; }
        public Reporter reporter;

        // 游戏入口prefab路径
        //public string GamePrefabPath = "Assets/Prefabs/Game.prefab";

        private void Awake()
        {
            Debug.Assert(Instance == null);
            Instance = this;
        }

        void Start()
        {
#if !RELEASE_VERSION
            // 如果不是发布版本，创建之，方便排查问题
            // 启动时先创建Reporter
            if (GameObject.Find("Reporter") == null)
            {
                var rep = GameObject.Instantiate(reporter);
                rep.name = "Reporter";
            }
#endif
            // 加载基础组件和管理器
            // gameObject.AddComponent<UnityThreadDispatcher>();
            // gameObject.AddComponent<ResourceManager>();
            // gameObject.AddComponent<LuaManager>();
            // gameObject.AddComponent<UIManager>();
            // gameObject.AddComponent<UpdateManager>().GamePrefabPath = GamePrefabPath;
        }

        private IEnumerator RebootLuaInCo(string bootScript)
        {
            Debug.Log("正在重启Lua");
            // Loading.SetActive(true);
            // Loading.SetProgress(0);
            // Loading.SetLoadingText("正在重启...");
            yield return new WaitForEndOfFrame();
            // var lua = GetComponent<LuaManager>();
            // var loop = GetComponent<LuaLooper>();
            // Destroy(loop);
            // Destroy(lua);
            yield return new WaitForEndOfFrame();
            // lua = gameObject.AddComponent<LuaManager>();
            // yield return lua.BootLua(bootScript);
            Debug.Log("重启Lua完毕");
        }

        /// <summary>
        /// 重启lua虚拟机
        /// </summary>
        public void Reboot()
        {
            // 重新加载本场景
            SceneManager.LoadScene(0);
        }
    }
}

