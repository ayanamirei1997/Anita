using System.Collections.Generic;

namespace Anita
{
    public abstract class GameBase
    {
        // 框架实例
        private static GameFrameworkEx _gameFramework;
        public static GameFrameworkEx gameFramework { get { return _gameFramework;  } }

        // 事件管理
        public static EventSystem eventManager { get {  
            if(gameFramework != null)
                return gameFramework.eventSystem;
            else
                return null;
        } }

        // 逻辑插件
        private List<LogicPluginBase> mPlugins = new List<LogicPluginBase>();

        public void InstallPlugin<T>() where T : LogicPluginBase, new()
        {
            var plugin = new T();
            mPlugins.Add(plugin);
            plugin.Init(this, gameFramework);
        }

        public void UnInstallPlugin<T>() where T : LogicPluginBase
        {
            foreach(var p in mPlugins)
            {
                if (p.GetType() == typeof(T))
                {
                    UnInstallPlugin(p);
                    break;
                }
            }
        }

        public void UnInstallPlugin(LogicPluginBase plugin)
        {
            if (!mPlugins.Contains(plugin))
                return;

            plugin.Release();
            mPlugins.Remove(plugin);
        }

        // 资源地址 目前用不到
        public abstract string resServerUri { get; }

        // 引擎初始化
        public void Init(GameFrameworkEx fw, ServerConf conf)
        {
            _gameFramework = fw;
            OnFrameworkReady(conf);
        }

        // 引擎初始化完毕
        protected virtual void OnFrameworkReady(ServerConf conf) { }
        public virtual void Update() { }
        public virtual void OnFixedUpdate() { }
        public virtual void OnScreenResize(float width, float height) { }
    }
}
