using UnityEngine;

namespace Anita
{
    public class AVGActionDefault : MonoBehaviour, AnitaAction
    {
        public void GetButtonChoose(string eventTag, Choose choose)
        {
            Debug.Log(eventTag + ": " + choose.Index + " " + choose.Text);
        }
    }
}
