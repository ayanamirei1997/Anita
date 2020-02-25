using System;

namespace Anita
{
    /// <summary>
    /// 窗口参数的统一基类
    /// </summary>
    public class GuiWindowParam
    {
        // 多种参数的button回调函数类型
        public delegate void ButtonCallBack();
        public delegate void ButtonCallBackStr(string str);
        public delegate void ButtonCallBackInt(int n);
        //public delegate void ButtonCallBackMsg(CSMessage msg);
        public delegate void ButtonCallBackBool(bool flag);

        public Action<GuiWindow> windowLoadedCallback;
        public Action<GuiWindow> windowBeforeShowHandler;
        public Action<GuiWindow> windowOnShowHandler;
        public GuiWindowParam(Action<GuiWindow> handler = null)
        {
            windowLoadedCallback = handler;
        }

        public GuiWindowParam(Action<GuiWindow> loadHandler, Action<GuiWindow> beforeShowHandler, Action<GuiWindow> OnShowHandler)
        {
            windowLoadedCallback = loadHandler;
            windowBeforeShowHandler = beforeShowHandler;
            windowOnShowHandler = OnShowHandler;
        }

        /// <summary>
        /// 绑定的ui窗口类型
        /// </summary>
        public virtual Type windowType { get { return typeof(GuiWindow); } }

        public virtual bool isShowInGuideing { get { return true; } }
    }
}