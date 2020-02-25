using System;
using System.Collections.Generic;
using UnityEngine;

namespace Anita
{
    // 事件分发器(事件计数,防止内存泄漏)
    public class EventSystem
    {
        static public int ALL_EVENT_COUNTER = 0;

        private Dictionary<int, Delegate> m_EventMap;

        protected Dictionary<int, Delegate> m_Events
        {
            get
            {
                if (m_EventMap == null)
                {
                    m_EventMap = new Dictionary<int, Delegate>();
                }

                return m_EventMap;
            }
        }

        public EventSystem()
        {

        }

        public void AddEvent(int evtName, Action evt)
        {
            Delegate evts;
            if (m_Events.TryGetValue(evtName, out evts))
            {
                Action act = evts as Action;
                if (act != null)
                {
                    var oldLen = act.GetInvocationList().Length;
                    act -= evt;
                    act += evt;
                    var newLen = act.GetInvocationList().Length;
                    if (newLen > oldLen)
                        ALL_EVENT_COUNTER++;

                    m_EventMap[evtName] = act;
                }
                else
                {
                    throw new Exception("EventDispatch evt type is null");
                }
            }
            else
            {
                m_Events.Add(evtName, evt);
                ALL_EVENT_COUNTER++;
            }
            CheckEventNum();
        }

        public void RemoveEvent(int evtName, Action evt)
        {
            if ((m_EventMap == null) || (evt == null))
            {
                return;
            }

            Delegate evts;
            if (m_EventMap.TryGetValue(evtName, out evts))
            {
                Action act = evts as Action;
                if (act != null)
                {
                    var oldLen = act.GetInvocationList().Length;
                    act -= evt;
                    if (act == null)
                    {
                        m_EventMap.Remove(evtName);
                        ALL_EVENT_COUNTER--;
                    }
                    else
                    {
                        var newLen = act.GetInvocationList().Length;
                        if (newLen < oldLen)
                            ALL_EVENT_COUNTER--;
                        m_EventMap[evtName] = act;
                    }
                }
                else
                {
                    throw new Exception("EventDispatch evt type is null,嗨！你的事件可能没加参数");
                }

                CheckEventNum();
            }
        }

        public void AddEvent<T>(int evtName, Action<T> evt)
        {
            Delegate evts;
            if (m_Events.TryGetValue(evtName, out evts))
            {
                Action<T> act = evts as Action<T>;
                if (act != null)
                {
                    var oldLen = act.GetInvocationList().Length;
                    act -= evt;
                    act += evt;
                    var newLen = act.GetInvocationList().Length;
                    if (newLen > oldLen)
                        ALL_EVENT_COUNTER++;

                    m_EventMap[evtName] = act;
                }
                else
                {
                    throw new Exception("EventDispatch evt type is null");
                }
            }
            else
            {
                m_Events.Add(evtName, evt);
                ALL_EVENT_COUNTER++;
            }
            CheckEventNum();
        }

        public void RemoveEvent<T>(int evtName, Action<T> evt)
        {
            if ((m_EventMap == null) || (evt == null))
            {
                return;
            }

            Delegate evts;
            if (m_EventMap.TryGetValue(evtName, out evts))
            {
                Action<T> act = evts as Action<T>;
                if (act != null)
                {
                    var oldLen = act.GetInvocationList().Length;
                    act -= evt;
                    if (act == null)
                    {
                        m_EventMap.Remove(evtName);
                        ALL_EVENT_COUNTER--;
                    }
                    else
                    {
                        var newLen = act.GetInvocationList().Length;
                        if (newLen < oldLen)
                            ALL_EVENT_COUNTER--;
                        m_EventMap[evtName] = act;
                    }
                }
                else
                {
                    throw new Exception("EventDispatch evt type is null");
                }
            }
            CheckEventNum();
        }

        public void AddEvent<T, U>(int evtName, Action<T, U> evt)
        {
            Delegate evts;
            if (m_Events.TryGetValue(evtName, out evts))
            {
                Action<T, U> act = evts as Action<T, U>;
                if (act != null)
                {
                    var oldLen = act.GetInvocationList().Length;
                    act -= evt;
                    act += evt;
                    var newLen = act.GetInvocationList().Length;
                    if (newLen > oldLen)
                        ALL_EVENT_COUNTER++;

                    m_EventMap[evtName] = act;
                }
                else
                {
                    throw new Exception("EventDispatch evt type is null");
                }
            }
            else
            {
                m_Events.Add(evtName, evt);
                ALL_EVENT_COUNTER++;
            }
            CheckEventNum();

        }

        public void RemoveEvent<T, U>(int evtName, Action<T, U> evt)
        {
            if ((m_EventMap == null) || (evt == null))
            {
                return;
            }

            Delegate evts;
            if (m_EventMap.TryGetValue(evtName, out evts))
            {
                Action<T, U> act = evts as Action<T, U>;
                if (act != null)
                {
                    var oldLen = act.GetInvocationList().Length;
                    act -= evt;
                    if (act == null)
                    {
                        m_EventMap.Remove(evtName);
                        ALL_EVENT_COUNTER--;
                    }
                    else
                    {
                        var newLen = act.GetInvocationList().Length;
                        if (newLen < oldLen)
                            ALL_EVENT_COUNTER--;
                        m_EventMap[evtName] = act;
                    }
                }
                else
                {
                    throw new Exception("EventDispatch evt type is null");
                }
            }
            CheckEventNum();
        }

        public void AddEvent<T, U, V>(int evtName, Action<T, U, V> evt)
        {
            Delegate evts;
            if (m_Events.TryGetValue(evtName, out evts))
            {
                Action<T, U, V> act = evts as Action<T, U, V>;
                if (act != null)
                {
                    var oldLen = act.GetInvocationList().Length;
                    act -= evt;
                    act += evt;
                    var newLen = act.GetInvocationList().Length;
                    if (newLen > oldLen)
                        ALL_EVENT_COUNTER++;

                    m_EventMap[evtName] = act;
                }
                else
                {
                    throw new Exception("EventDispatch evt type is null");
                }
            }
            else
            {
                m_Events.Add(evtName, evt);
                ALL_EVENT_COUNTER++;
            }
            CheckEventNum();
        }

        public void RemoveEvent<T, U, V>(int evtName, Action<T, U, V> evt)
        {
            if ((m_EventMap == null) || (evt == null))
            {
                return;
            }

            Delegate evts;
            if (m_EventMap.TryGetValue(evtName, out evts))
            {
                Action<T, U, V> act = evts as Action<T, U, V>;
                if (act != null)
                {
                    var oldLen = act.GetInvocationList().Length;
                    act -= evt;
                    if (act == null)
                    {
                        m_EventMap.Remove(evtName);
                        ALL_EVENT_COUNTER--;
                    }
                    else
                    {
                        var newLen = act.GetInvocationList().Length;
                        if (newLen < oldLen)
                            ALL_EVENT_COUNTER--;
                        m_EventMap[evtName] = act;
                    }
                }
                else
                {
                    throw new Exception("EventDispatch evt type is null");
                }
            }
            CheckEventNum();
        }

        public void AddEvent<T, U, V, W>(int evtName, Action<T, U, V, W> evt)
        {
            Delegate evts;
            if (m_Events.TryGetValue(evtName, out evts))
            {
                Action<T, U, V, W> act = evts as Action<T, U, V, W>;
                if (act != null)
                {
                    var oldLen = act.GetInvocationList().Length;
                    act -= evt;
                    act += evt;
                    var newLen = act.GetInvocationList().Length;
                    if (newLen > oldLen)
                        ALL_EVENT_COUNTER++;

                    m_EventMap[evtName] = act;
                }
                else
                {
                    throw new Exception("EventDispatch evt type is null");
                }
            }
            else
            {
                m_Events.Add(evtName, evt);
                ALL_EVENT_COUNTER++;
            }
            CheckEventNum();
        }

        public void RemoveEvent<T, U, V, W>(int evtName, Action<T, U, V, W> evt)
        {
            if ((m_EventMap == null) || (evt == null))
            {
                return;
            }

            Delegate evts;
            if (m_EventMap.TryGetValue(evtName, out evts))
            {
                Action<T, U, V, W> act = evts as Action<T, U, V, W>;
                if (act != null)
                {
                    var oldLen = act.GetInvocationList().Length;
                    act -= evt;
                    if (act == null)
                    {
                        m_EventMap.Remove(evtName);
                        ALL_EVENT_COUNTER--;
                    }
                    else
                    {
                        var newLen = act.GetInvocationList().Length;
                        if (newLen < oldLen)
                            ALL_EVENT_COUNTER--;
                        m_EventMap[evtName] = act;
                    }
                }
                else
                {
                    throw new Exception("EventDispatch evt type is null");
                }
            }
            CheckEventNum();
        }

        public void TriggerEvent(int evtName)
        {
            if (m_EventMap == null)
            {
                return;
            }

            Delegate evts;
            if (m_EventMap.TryGetValue(evtName, out evts))
            {
                Delegate[] list = evts.GetInvocationList();
                for (int i = 0; i < list.Length; ++i)
                {
                    Action act = list[i] as Action;
                    if (act != null)
                    {
                        try
                        {
                            act();
                        }
                        catch (Exception e)
                        {
                            if (Debug.unityLogger.logEnabled)
                            {
                                Debug.LogErrorFormat("响应事件异常，事件id({0})\n{1}\n{2}", evtName, e.Message, e.StackTrace);
                            }
                        }
                    }
                }
            }
        }

        public void TriggerEvent<T>(int evtName, T V1)
        {
            if (m_EventMap == null)
            {
                return;
            }

            Delegate evts;
            if (m_EventMap.TryGetValue(evtName, out evts))
            {
                Delegate[] list = evts.GetInvocationList();
                for (int i = 0; i < list.Length; ++i)
                {
                    Action<T> act = list[i] as Action<T>;
                    if (act != null)
                    {
                        try
                        {
                            act(V1);
                        }
                        catch (Exception e)
                        {
                            if (Debug.unityLogger.logEnabled)
                            {
                                Debug.LogErrorFormat("响应事件异常，事件id({0})\n{1}\n{2}", evtName, e.Message, e.StackTrace);
                            }
                        }
                    }
                }
            }
        }

        public void TriggerEvent<T, U>(int evtName, T V1, U V2)
        {
            if (m_EventMap == null)
            {
                return;
            }

            Delegate evts;
            if (m_EventMap.TryGetValue(evtName, out evts))
            {
                Delegate[] list = evts.GetInvocationList();
                for (int i = 0; i < list.Length; ++i)
                {
                    Action<T, U> act = list[i] as Action<T, U>;
                    if (act != null)
                    {
                        try
                        {
                            act(V1, V2);
                        }
                        catch (Exception e)
                        {
                            if (Debug.unityLogger.logEnabled)
                            {
                                Debug.LogErrorFormat("响应事件异常，事件id({0})\n{1}\n{2}", evtName, e.Message, e.StackTrace);
                            }
                        }
                    }
                }
            }
        }

        public void TriggerEvent<T, U, V>(int evtName, T V1, U V2, V V3)
        {
            if (m_EventMap == null)
            {
                return;
            }

            Delegate evts;
            if (m_EventMap.TryGetValue(evtName, out evts))
            {
                Delegate[] list = evts.GetInvocationList();
                for (int i = 0; i < list.Length; ++i)
                {
                    Action<T, U, V> act = list[i] as Action<T, U, V>;
                    if (act != null)
                    {
                        try
                        {
                            act(V1, V2, V3);
                        }
                        catch (Exception e)
                        {
                            if (Debug.unityLogger.logEnabled)
                            {
                                Debug.LogErrorFormat("响应事件异常，事件id({0})\n{1}\n{2}", evtName, e.Message, e.StackTrace);
                            }
                        }
                    }
                }
            }
        }

        public void TriggerEvent<T, U, V, W>(int evtName, T V1, U V2, V V3, W V4)
        {
            if (m_EventMap == null)
            {
                return;
            }

            Delegate evts;
            if (m_EventMap.TryGetValue(evtName, out evts))
            {
                Delegate[] list = evts.GetInvocationList();
                for (int i = 0; i < list.Length; ++i)
                {
                    Action<T, U, V, W> act = list[i] as Action<T, U, V, W>;
                    if (act != null)
                    {
                        try
                        {
                            act(V1, V2, V3, V4);
                        }
                        catch (Exception e)
                        {
                            if (Debug.unityLogger.logEnabled)
                            {
                                Debug.LogErrorFormat("响应事件异常，事件id({0})\n{1}\n{2}", evtName, e.Message, e.StackTrace);
                            }
                        }
                    }
                }
            }
        }

        // 检查计数的正确性
        private void CheckEventNum()
        {
            return;
            var num = 0;
            var e = m_EventMap.GetEnumerator();
            while (e.MoveNext())
            {
                var value = e.Current.Value;
                num += value.GetInvocationList().Length;
            }

            if (num != ALL_EVENT_COUNTER)
            {
                Debug.LogError("event num error");
            }
        }

        internal void AddEvent<T>(object updateHistoryDetailRankWindow, Action<T> updateWindow)
        {
            throw new NotImplementedException();
        }
    }
    
//#endif

}