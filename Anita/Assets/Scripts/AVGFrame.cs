using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Anita
{
    [RequireComponent(typeof(AVGController))]
    public class AVGFrame : MonoBehaviour, AnitaAVG
    {
        [SerializeField] private AVGSceneManager sceneManager;
        private AVGController controller;

        private AnitaLoader loader;
        private AnitaAnimation anim;
        private AnitaAround around;
        private AnitaAction action;
        private AnitaInput input;
        private AnitaEvent event0;

        private List<AVGModel> modelList;

        public bool autoStart = true;

        public string imageUrlPrefix = "Image";
        public string audioUrlPrefix = "Audio";

        public TextAsset textAsset;

        public bool useExcel = false;
        public string excelName;

        [Header("UI Component")]
        public Canvas canvas;
        public Text text;
        public Text nameText;
        public Image characterImage;
        public Image backgroundImage;
        public AudioSource characterVoice;
        public AudioSource backgroundMusic;
        [Header("Choose Button")]
        public GameObject buttonPanel;
        public Button[] buttons;

        public AVGSceneManager SceneManager { get => sceneManager; set => sceneManager = value; }
        //实现接口后挂在在此脚本同一物体后自动注入，否则使用默认
        public AnitaAnimation Anim { set { anim = value; controller.Anim = anim; } }
        public AnitaAround Around { set { around = value; controller.Around = around; } }
        public AnitaAction Action { set { action = value; controller.Action = action; } }
        public AnitaLoader Loader { set { loader = value; controller.Loader = loader; } }
        public AnitaInput Input0 { set { input = value; controller.Input0 = input; } }
        private AnitaEvent Event0 { set { event0 = value; controller.Event0 = event0; } }

        private bool isReady = false;
        private Coroutine startCoro = null;

        private void Awake()
        {
            ComponentsInit();
        }

        private void Start()
        {
            try
            {
                if (loader.Analysis(out modelList, useExcel ?
                    (object)(AVGSetting.EXCEL_PATH + excelName) :
                    textAsset))
                {
                    controller.ModelList = modelList;
                    isReady = true;
                    if (autoStart)
                    {
                        Begin();
                    }
                }
                else
                {
                    Debug.LogError("TextAsset analysis failed!");
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning("AVGFrame " + gameObject.name + "::" + e.Message + e.StackTrace);
                enabled = false;
            }

            if (SceneManager != null)
            {
                PlaySave current = AVGGameManager.ins.CurrentPlaySave;
                if (current != null)
                {
                    autoStart = false;
                    if (current.useExcel == useExcel)
                    {
                        if ((useExcel && current.excel.Equals(excelName)) ||
                            (!useExcel && current.script == textAsset))
                        {
                            int i = current.index - 1;
                            if (i < 0) i = 0;
                            SetCurrentIndexAndRun(i);
                        }
                    }
                }
            }
        }

        private IEnumerator BeginCoro()
        {
            while (!isReady)
            {
                yield return null;
            }
            yield return null;
            controller.Begin();
        }

        private void ComponentsInit()
        {
            controller = GetComponent<AVGController>();
            if (controller == null)
            {
                controller = gameObject.AddComponent<AVGController>();
            }
            controller.SetPrefix(imageUrlPrefix, audioUrlPrefix);
            controller.InitUIComponent(canvas, nameText, text, characterImage, backgroundImage,
                characterVoice, backgroundMusic);
            controller.SetChooseButton(buttonPanel, buttons);

            if (useExcel)
            {
                Loader = (AnitaLoader)ComponentCheck<AnitaLoader>(loader, typeof(AVGExcelLoader));
            }
            else
            {
                Loader = (AnitaLoader)ComponentCheck<AnitaLoader>(loader, typeof(AVGLoader));
            }

            Anim = (AnitaAnimation)ComponentCheck<AnitaAnimation>(anim, typeof(AVGAnimationDefault));

            Action = (AnitaAction)ComponentCheck<AnitaAction>(action, typeof(AVGActionDefault));

            Around = (AnitaAround)ComponentCheck<AnitaAround>(around, typeof(AVGAroundDefault));

            Input0 = (AnitaInput)ComponentCheck<AnitaInput>(input, null, true);

            Event0 = (AnitaEvent)ComponentCheck<AnitaEvent>(event0, typeof(AVGEventDefault));
        }

        private object ComponentCheck<T>(T obj, Type defaultImpl = null, bool defaultNull = false)
        {
            object o = obj;
            if ((o = GetComponent<T>()) == null)
            {
                if (defaultNull)
                {
                    return default(T);
                }
                if (defaultImpl == null)
                {
                    Debug.Log("ComponentCheck Error ~ The implement of T is null at ComponentCheck<T>() at AVGFrame.cs");
                    return default(T);
                }
                o = gameObject.AddComponent(defaultImpl);
            }
            return o;
        }

        //Controller
        public void Begin()
        {
            if (SceneManager != null)
            {
                SceneManager.SetCurrentFrame(this);
            }
            if (startCoro == null)
            {
                if (!isReady)
                {
                    StartCoroutine(BeginCoro());
                    return;
                }
                controller.Begin();
            }
        }

        public void AVGStop() { controller.Stop(); }

        public void AVGContinue() { controller.Continue(); }

        public List<TextModel> GetPreviousText(int length) { return controller.GetPreviousText(length); }

        public void Restart() { controller.Begin(); }

        public int CurrentIndex() { return controller.CurrentIndex(); }

        public void SetCurrentIndexAndRun(int i)
        {
            if (SceneManager != null)
            {
                SceneManager.SetCurrentFrame(this);
            }
            controller.SetCurrentIndexAndRun(i);
        }
    }
}