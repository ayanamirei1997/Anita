namespace Anita
{
    public class AnitaGameConfig
    {
        // 资源服务器地址
        //public string dataServerUri;

        // 程序版本号
        public string appVersion;

        // 协议加密密钥
        public string secretKey;

        // 日志等级
        //public int logLevel;

        // 是否强制使用安全连接(https/wss)
        //public bool forceSecureConnection = false;

        // 是否锁定资源版本
        //public bool lockResVersion = false;

        // 锁定的资源版本号(lockResVersion为true时才生效)
        public string resVersion;

        // Log总开关
        public bool LogEnabled = true;

        // 是否为测试模式
        public bool DebugMode = false;

        // 配表版本号
        public string configVersion;
    }
}
