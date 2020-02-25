
using System.Collections.Generic;

namespace Anita
{
    public class WindowLoadingParam
    {
        public GuiWindow pendingWindow;                 // 等待显示的窗口
        public GuiWindowParam pendingWindowParam;       // 窗口参数
        public bool showLoading;
    }
    
    // 窗口加载器
    public class WindowLoader
    {
        public delegate void WindowReadyDelegate(GuiWindow win, GuiWindowParam param,bool fromCache);
        public event WindowReadyDelegate onWindowReady;

        protected GuiManager guiManager;

        protected List<WindowLoadingParam> loadingParams = new List<WindowLoadingParam>();

        public WindowLoader(GuiManager guiMgr)
        {
            guiManager = guiMgr;
        }

        // 加载窗口
        public void Load(GuiWindow win, GuiWindowParam param, bool showLoading)
        {
            if (win == null)
                return;

            // 窗口已经在加载队列中
            foreach (var p in loadingParams)
            {
                if (p.pendingWindow == win)
                {
                    return;
                }
            }

            // 窗口是否需要loading
            //if (win.NeedLoading())
            //{
            //var wlp = new WindowLoadingParam();
            //    wlp.pendingWindow = win;
            //    wlp.pendingWindowParam = param;
            //    wlp.showLoading = showLoading;
            //    loadingParams.Add(wlp);

            //    if (showLoading)
            //    {
            //        // 需要loading，显示loading转圈圈
            //        DelayShowLoading(1);
            //    }

            //    win.onLoadingFinished += OnLoadingFinished;
            //    win.onLoadingUpdating += OnLoadingUpdated;
            //    win.DoLoad();
            //}
            //else
            //{
                // 不需要loading，直接显示
                OnWindowReady(win, param, true);
                //}
        }

        private void OnLoadingUpdated(float current, float total)
        {
            ChangeLoadingBar(current, total);
        }

        // 窗口加载结束的回调
        private void OnLoadingFinished(GuiWindow win)
        {
            if (win == null)
            {
                return;
            }

            foreach (var p in loadingParams)
            {
                if (p.pendingWindow == win)
                {
                    p.pendingWindow.onLoadingFinished -= OnLoadingFinished;
                    OnWindowReady(p.pendingWindow, p.pendingWindowParam,false);
                    loadingParams.Remove(p);


                    bool hideLoading = true;
                    //FIXME 如果有一个加载失败，打开那个窗口都hui显示狼头。。。。。
                    //foreach (var ppp in loadingParams)
                    //{
                    //    if (ppp.showLoading)
                    //    {
                    //        hideLoading = false;
                    //        break;
                    //    }
                    //}
                    if (hideLoading)
                        OnHideLoading();
                    break;
                }
            }
        }

        protected virtual void ChangeLoadingBar(float current, float total)
        {
        }

        private void OnWindowReady(GuiWindow win, GuiWindowParam param,bool fromCache = false)
        {
            if (win == null)
                return;

            if (onWindowReady != null)
            {
                onWindowReady(win, param, fromCache);
            }
        }

        // 显示loading转圈圈
        protected virtual void OnShowLoading() { }

        // 显示loading转圈圈
        protected virtual void DelayShowLoading(int delaySeconds) { }

        // 隐藏loading转圈圈
        protected virtual void OnHideLoading() { }

        public virtual void Resize(float width, float height) { }

    }
}
