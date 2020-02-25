using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System;
using FairyGUI;

namespace Anita
{
    // 框架实例
    public class GameFrameworkEx : MonoBehaviour
    {
        private GameBase game;
        public EventSystem eventSystem { get; private set; }

        public static GameFrameworkEx Create()
        {
            var go = new GameObject("GameFramework");
            DontDestroyOnLoad(go);

            var instance = go.AddComponent<GameFrameworkEx>();
            return instance;
        }

        public void Run(GameBase game)
        {
            this.game = game;
            InitApplication();
            InitGui();
            InitEventSystem();
            InitResSystem();
            InitTest();
        }

        private void InitApplication()
        {
            Application.runInBackground = true;
            Debug.Log("初始化Application");
        }

        private void InitGui()
        {
            // todo 初始化FairyGUI
            
            UIObjectFactory.SetLoaderExtension(typeof(GLoaderExtensions));

            Debug.Log("初始化GUI!!");
        }

        public void InitEventSystem()
        {
            eventSystem = new EventSystem();
            Debug.Log("初始化EventSystem");
        }

        void InitResSystem()
        {
            StartCoroutine(OnResInitFinish());
            Debug.Log("初始化ResSystem");
        }

        private IEnumerator OnResInitFinish()
        {
            ServerConf conf = null;
            var uri = string.Format("{0}server.json?t={1}", game.resServerUri, UnityEngine.Random.value);
            var www = UnityWebRequest.Get(uri);
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                conf = new ServerConf();
                conf.res_file = "";
            }
            else
            {
                conf = JsonUtility.FromJson<ServerConf>(www.downloadHandler.text);
            }

            if (game != null)
            {
                game.Init(this, conf);
            }
        }

        private void OnScreenResize(int width, int height)
        {
            if (game == null)
                return;
        }

        private void Update()
        {
            if (game != null)
                game.Update();
        }

        private void FixedUpdate()
        {
            if (game != null)
                game.OnFixedUpdate();
        }

        internal Coroutine RunCoroutine(IEnumerator enumerable)
        {
            return StartCoroutine(enumerable);
        }

        private void InitTest()
        {
            //GuiManager.ShowWindow<TestWindow>((TestWindow win) =>
            //{
                //AnitaDebug.Log("初始化测试Window");
            //});
            
        }
    }
}
