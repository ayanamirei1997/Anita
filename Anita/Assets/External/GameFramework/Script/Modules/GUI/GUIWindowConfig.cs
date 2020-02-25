using System;
using System.Collections.Generic;
using Anita;
using ResourceManager;


/// <summary>
/// 窗口配置
/// </summary>

namespace Anita
{
    public enum WindowStyle 
    {
        FixSize,        // 自适应
        FullScreen,     // 全屏
    }

    public class GuiWindowConfig
    {
        public string res;                                          // 包资源路径
        public string package;                                      // UIPackage名
        public string window;                                       // 窗口名
        public List<UICfg> cfgs;                                    // 需要加载依赖的UI资源
        public int layer;                                           // 层级
        public WindowStyle style = WindowStyle.FixSize;             // 自适应/全屏
        public bool useMask = true;
        public float maskAlpha = 0;
        public bool userSceneTextureMask = true;
        public bool userSceneTextureMaskWithFullScreen = false;     // 全屏界面也显示模糊背景
        public bool isClickCloseWindow = true;                      // 点击空白处是否关闭窗口
        public bool isEffectFade = true;                            // 是否执行窗口淡入淡出效果
        public bool isFullScreen = true;                            // 全屏窗口，点击无法穿透到下一层界面
        public bool notUseUninstall = true;                         // true:要销毁
        public bool isHideBottomWindow = true;                      // 如果是一级界面，该window显示时是否隐藏下层的界面
        public bool forceSceneTextureMask = false;                  // 强制显示模糊背景 升星升阶用
        public bool isNeedHideNotTop = false;                       // 不在最顶层时，是否需要关闭
        public bool isBigWindow = false;                            // 是否是大窗口，如果是大窗口的话，会隐藏层级在他之下的面板

    }

}
