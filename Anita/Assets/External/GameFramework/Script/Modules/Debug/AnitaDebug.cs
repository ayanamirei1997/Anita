using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public enum DebugType
{
    Limit,
    Normal
}

/// <summary>
/// 区别于引擎自带的Debug
/// 框架自带的AnitaDebug方法，受控于LogEnabled
/// </summary>
public class AnitaDebug
{
    private static bool[] s_AnitaDebugEnabled;

    [Conditional("OPEN_ANITA_DEBUG")]
    public static void Init()
    {
        var array = Enum.GetValues(typeof(DebugType));
        if (array.Length == 0)
            return;
        s_AnitaDebugEnabled = new bool[array.Length];

        // 默认全部显示
        for (var i = s_AnitaDebugEnabled.Length - 1; i >= 0; i--)
        {
            s_AnitaDebugEnabled[i] = true;
        }

    }
    
    
    public static bool IsDebugEnabled(DebugType debugType)
    {
        if (s_AnitaDebugEnabled == null)
        {
            Debug.Log("AnitaDebug未初始化，请检查！");
            return false;
        }
        return s_AnitaDebugEnabled[(int)debugType];
    }

    [Conditional("OPEN_ANITA_DEBUG")]
    public static void SetDebugEnabled(DebugType debugType, bool enabled)
    {
        if (s_AnitaDebugEnabled == null)
        {
            Debug.Log("AnitaDebug未初始化，请检查！");
            return ;
        }
        s_AnitaDebugEnabled[(int)debugType] = enabled;
    }

    [Conditional("OPEN_ANITA_DEBUG")]
    public static void Assert(DebugType debugType, bool condition)
    {
        if (CheckShow(debugType))
            Debug.Assert(condition);
    }

    [Conditional("OPEN_ANITA_DEBUG")]
    public static void Assert(DebugType debugType, bool condition, object message)
    {
        if (CheckShow(debugType))
            Debug.Assert(condition, message);
    }

    [Conditional("OPEN_ANITA_DEBUG")]
    public static void Log(DebugType debugType, object message)
    {
        if (CheckShow(debugType))
            Debug.Log(message);
    }

    [Conditional("OPEN_ANITA_DEBUG")]
    public static void LogWarning(DebugType debugType, object message)
    {
        if (CheckShow(debugType))
            Debug.LogWarning(message);
    }

    [Conditional("OPEN_ANITA_DEBUG")]
    public static void LogError(DebugType debugType, object message)
    {
        if (CheckShow(debugType))
            Debug.LogError(message);
    }

    [Conditional("OPEN_ANITA_DEBUG")]
    public static void AssertFormat(DebugType debugType, bool condition, string format, params object[] message)
    {
        if (CheckShow(debugType))
            Debug.AssertFormat(condition, format, message);
    }

    [Conditional("OPEN_ANITA_DEBUG")]
    public static void LogFormat(DebugType debugType, string format, params object[] message)
    {
        if (CheckShow(debugType))
            Debug.LogFormat(format, message);
    }

    [Conditional("OPEN_ANITA_DEBUG")]
    public static void LogWarningFormat(DebugType debugType, string format, params object[] message)
    {
        if (CheckShow(debugType))
            Debug.LogWarningFormat(format, message);
    }

    [Conditional("OPEN_ANITA_DEBUG")]
    public static void LogErrorFormat(DebugType debugType, string format, params object[] message)
    {
        if (CheckShow(debugType))
            Debug.LogErrorFormat(format, message);
    }

    [Conditional("OPEN_ANITA_DEBUG")]
    public static void LogException (DebugType debugType, Exception exception)
    {
        if (CheckShow(debugType))
            Debug.LogException(exception);
    }
    
    private static bool CheckShow(DebugType debugType)
    {
        if (s_AnitaDebugEnabled == null)
        {
            Debug.LogError("AnitaDebug 未初始化，请先调用AnitaDebug.Init方法");
            return false;
        }
        return s_AnitaDebugEnabled[(int)debugType];
    }

    #region Normal Debug Log

    [Conditional("OPEN_ANITA_DEBUG")]
    public static void Assert(bool condition)
    {
        Assert(DebugType.Normal, condition);
    }

    [Conditional("OPEN_ANITA_DEBUG")]
    public static void Assert(bool condition , object message)
    {
        Assert(DebugType.Normal, condition, message);
    }

    [Conditional("OPEN_ANITA_DEBUG")]
    public static void Log(object message)
    {
        Log(DebugType.Normal, message);
    }

    [Conditional("OPEN_ANITA_DEBUG")]
    public static void LogWarning(object message)
    {
        LogWarning(DebugType.Normal, message);
    }

    [Conditional("OPEN_ANITA_DEBUG")]
    public static void LogError(object message)
    {
        LogError(DebugType.Normal, message);
    }

    [Conditional("OPEN_ANITA_DEBUG")]
    public static void AssertFormat(bool condition , string format, params object[] message)
    {
        AssertFormat(DebugType.Normal, condition, format, message);
    }

    [Conditional("OPEN_ANITA_DEBUG")]
    public static void LogFormat(string format, params object[] message)
    {
        LogFormat(DebugType.Normal, format, message);
    }

    [Conditional("OPEN_ANITA_DEBUG")]
    public static void LogWarningFormat(string format, params object[] message)
    {
        LogWarningFormat(DebugType.Normal, format, message);
    }

    [Conditional("OPEN_ANITA_DEBUG")]
    public static void LogErrorFormat(string format, params object[] message)
    {
        LogErrorFormat(DebugType.Normal, format, message);
    }

    [Conditional("OPEN_ANITA_DEBUG")]
    public static void LogException(Exception exception)
    {
        LogException(DebugType.Normal, exception);
    }
    #endregion

}
