using UnityEngine;

namespace Anita
{
    public class AVGEventDefault : MonoBehaviour, AnitaEvent
    {
        //return true to continue to play the next one
        //返回true继续播放下一句
        public bool ReceiveEvent(string e)
        {
            return NoTargetEvent(e);
        }

        public bool NoTargetEvent(string e)
        {
            Debug.Log("Event: '" + e + "' has no function to call at ReceiveEvent(string e) at AVGEventDefault.cs");
            return true;
        }
    }
}
