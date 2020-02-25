using FairyGUI;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using UnityEngine;
using UnityEngine.Networking.NetworkSystem;

namespace Anita
{
    class UIPackageState
    {
        public const int Nothing = 0;                   // 未定义
        public const int NotUseDestory = 1;             // 用完销毁
        public const int OutMemoryNotUseDestory = 2;    // 超出内存临界值,再用完销毁
        
    }

    public class GuiManager : ModuleBase
    {
        private static float WINDOW_DISPOSE_TIME = 10;  // n秒窗口不可见销毁
        protected WindowLoader m_WindowLoader;
        protected EventSystem m_EventSystem;

        //protected Dictionary<int, GuiLa>
        protected Dictionary<Type, GuiWindow> m_WindowInstances = new Dictionary<Type, GuiWindow>();    // 所有窗口实例
        protected List<Action> updateCallback = new List<Action>();
        
        
        public GuiManager(GameFrameworkEx fw) : base(fw, ManagerType.GuiManager)
        {
            m_EventSystem = fw.eventSystem;
        }

        // 未卸载的UI资源, 被标记为永不释放的UIPackage
        protected Dictionary<string, string> notUnloadedResDic = new Dictionary<string, string>();      // <pkg name, path>

        public virtual void OnWindowCreate(GuiWindow win) { }
        public virtual void OnWindowShow(GuiWindow win) { }
        public virtual void OnWindowHide(GuiWindow win) { }
        public virtual void OnWindowDispose(GuiWindow win) { }
        
        public int GetWindowInstancesCount()
        {
            return m_WindowInstances.Count;
        }


        // 显示类型为T的窗口
        public void ShowWindow<T>(bool showLoading = true)
        {
            ShowWindow(typeof(T), null, showLoading);
        }

        public void ShowWindow(GuiWindowParam param)
        {
            if (param == null)
                return;

            ShowWindow(param.windowType, param);
        }

        // 打开窗口. 带一个onshow的Action
        public void ShowWindow<T>(Action<T> beforeShowAction, bool showLoading = true) where T : GuiWindow
        {
            ShowWindow(typeof(T), new GuiWindowParam(null, win =>
            {
                T w = (T)win;
                beforeShowAction(w);
            },null), showLoading);
        }

        public void ShowWindowWithOnShowAction<T>(Action<T> OnShowAction, bool showLoading = true) where T : GuiWindow
        {
            ShowWindow(typeof(T), new GuiWindowParam(null, null,win =>
            {
                T w = (T)win;
                OnShowAction(w);
            }), showLoading);
        }
        public void ShowWindow(Type windowType, GuiWindowParam param, bool showLoading = true)
        {
            LoadAndShowWindow(windowType, param, showLoading);
        }

        // 加载并显示窗口
        private void LoadAndShowWindow(Type windowType, GuiWindowParam param, bool showLoading)
        {
            if (windowType == null)
            {
                AnitaDebug.LogWarning("GuiManager::LoadAndShowWindow, 无效的窗口类型");
                return;
            }

            //Debug.LogFormat("LoadAndShowWindow 加载并显示窗口: {0}", windowType);

            // 每种类型的ui窗口都只有一个实例
            GuiWindow win = null;
            if (!m_WindowInstances.TryGetValue(windowType, out win))
            {
                // 创建新的ui窗口实例
                // 动态创建 所以用activetor
                win = (GuiWindow)Activator.CreateInstance(windowType);
                win.guiManager = this;
                m_WindowInstances[windowType] = win;
            }

            if (win == null)
            {
                return;
            }

            // 标记显示
            win.isVisibleLogic = true;

            // windowLoader负责处理窗口依赖资源的加载，加载完成后才真正调用窗口的显示相关函数
            if (m_WindowLoader != null)
            {
                m_WindowLoader.Load(win, param, showLoading);
            }
            else
            {
                InternalShowWindow(win, param);
            }
            
            
        }
        
        // 显示窗口(不做资源检查)
        protected void InternalShowWindow(GuiWindow win, GuiWindowParam param,bool fromCache = false)
        {
            if (win == null)
            {
                AnitaDebug.LogWarning("GuiManager::InternalShowWindow, 窗口为null");
                return;
            }
            //var type = win.GetType();
            //win.Resize(GRoot.inst.width, GRoot.inst.height);
            if(!win.isVisibleLogic)
            {   // 窗口还未加载就被隐藏
                //AnitaDebug.Assert(!win.isVisibleRender);
                return;
            }

            if (win.Config.style == WindowStyle.FullScreen)
            {
                
            }

            var myroot = GRoot.inst;
            
            win.Show(param,myroot  , null);


        }


        public void HideWindow<T>() where T : GuiWindow
        {
            HideWindow(typeof(T));
        }

        public void HideWindow(Type windowType)
        {
            GuiWindow win = null;
            if (m_WindowInstances.TryGetValue(windowType, out win))
            {
                HideWindow(win);
            }
        }

        private void HideWindow(GuiWindow win)
        {
            if (win == null)
            {
                return;
            }

            // 标记为隐藏
            win.isVisibleLogic = false;


            if (win.Config.style == WindowStyle.FullScreen)
            {
                
            }
        }


        private void AddUpdateEvent(Action callback)
        {
            updateCallback.Add(callback);
        }

        private void RemoveUpdateEvent(Action callback)
        {
            for (int i = updateCallback.Count - 1; i >= 0; i++)
            {
                if (updateCallback[i] == callback)
                {
                    updateCallback.RemoveAt(i);
                }
                
            }
        }

        public virtual void Update()
        {
            for (int i = 0; i < updateCallback.Count; i++)
            {
                updateCallback[i]();
            }

            if (Time.frameCount % 30 == 0)          // 间隔n帧检测一次
            {
                UpdateCheckUninstall();
            }
        }
        
        // 检测窗口释放（一次只销毁一个）
        private float m_lastCheckTime = 0;

        private void UpdateCheckUninstall()
        {
            float deltaTime = Time.fixedTime - m_lastCheckTime;
            m_lastCheckTime = Time.fixedTime;

            GuiWindow disposeWindow = null;

            var winEnumerator = m_WindowInstances.GetEnumerator();
            while (winEnumerator.MoveNext())
            {
                var window = winEnumerator.Current.Value;
                
                if(!window.Config.notUseUninstall)
                    continue;
                
                if(window.isVisibleLogic || window.isVisibleRender)
                    continue;

                window.m_HideTime += deltaTime;

                if (window.m_HideTime > WINDOW_DISPOSE_TIME)
                {
                    disposeWindow = window;
                    break;
                    
                }


            }

            if (disposeWindow != null)
            {
                disposeWindow.Dispose();
            }
        }
        
        
        
        
        public void UninstallWindow(GuiWindow window, bool isUnLoadRes = false)
        {
            if (window == null )
                return;
             
            OnWindowDispose(window);
            Type t = window.GetType();
            m_WindowInstances.Remove(t);
        }
        
        
        // 检测某个UI界面是否正在显示
        public GuiWindow CheckUIWindowVisible(Type winType)
        {
            GuiWindow win = null;
            m_WindowInstances.TryGetValue(winType, out win);
            if (win != null && win.isVisibleRender)
            {
                return win;
            }
            return null;
        }

        public GuiWindow GetWindowInstance(Type winType)
        {
            GuiWindow win = null;
            m_WindowInstances.TryGetValue(winType, out win);
            return win;
        }
        
        // 删除某个窗口实例（选服地图需重新初始化）
        public void DeleteWindowInstance(Type winType)
        {
            if (m_WindowInstances.ContainsKey(winType))
            {
                m_WindowInstances.Remove(winType);
            }
        }
        
         // 将窗口移动到特定位置
        public void TransformWindow<T>(WindowLocation type) where T : GuiWindow
        {
            TransformWindow(typeof(T), type);
        }

        public void TransformWindow(Type windowType, WindowLocation type)
        {
            GuiWindow win = null;
            if (m_WindowInstances.TryGetValue(windowType, out win))
            {
                TransformWindow(win, type);
            }
        }

        public void TransformWindow(GuiWindow win, WindowLocation type)
        {
            if (type == WindowLocation.Left)
            {
                win.rootComponent.position = new Vector3(-GRoot.inst.width / 2 + 50, win.rootComponent.position.y, 0);
                win.Config.useMask = false;
            }

            if (type == WindowLocation.Center)
            {
                win.rootComponent.position = new Vector3(GRoot.inst.width / 2 - win.rootComponent.actualWidth / 2, win.rootComponent.position.y, 0);
                win.Config.useMask = true;
            }
        }

        public static void ResizeComponet(GComponent rootGo, bool isFullScreen)
        {
            if (rootGo == null)
            {
                return;
            }
            var width = isFullScreen ? Screen.width : 0 ;
            var height = isFullScreen ? Screen.height : 0;
            //Zoom(rootGo, width, height);
        }

        /// <summary>
        /// GUI组件适配尺寸和缩放
        /// 全屏显示: width > 0 && height > 0， 会遍历rootGo内部组件进行缩放
        /// 窗口显示: 直接对rootGo进行缩放
        /// </summary>
        /*public static void Zoom(GComponent rootGo, float width = 0, float height = 0)
        {
            if (width.IsEqual(0) && width.IsEqual(0))
            {
                rootGo.scale = Vector2.one * ScreenScaleMonitor.Instance.ScaleFactor;
                return;
            }

            rootGo.SetSize(width, height);
            rootGo.Center();
            var scale = Vector2.one * ScreenScaleMonitor.Instance.ScaleFactor;
            var scale2 = ScreenScaleMonitor.Instance.Scale;
            var prefix = "__";
            for (int i = 0, n = rootGo.numChildren; i < n; i++)
            {
                var go = rootGo.GetChildAt(i);
                if (go.name.StartsWith(prefix))
                {
                    if (scale2.x.IsEqual(scale2.y))
                    {
                        go.width = width;
                        go.height = height;
                    }
                    else if (scale2.x > scale2.y)
                    {
                        go.width = width;
                    }
                    else
                    {
                        go.height = height;
                    }
                    continue;
                }
                go.scale = scale;
            }
        }*/

        public enum WindowLocation
        {
            Left,
            Center,
        }
        
        
        
        
        
        
        
        
        
        }
    }
        








    




