using UnityEngine;

namespace Anita
{
    [System.Serializable]
    public class SettingSave
    {
        public float textChangeSpeed = 0.2f;
        public float bkAudioVolume = 1.0f;
        public bool breakCharVoice = true;

        public SettingSave(float textChangeSpeed, float bkAudioVolume,
            bool breakCharVoice)
        {
            this.textChangeSpeed = textChangeSpeed;
            this.bkAudioVolume = bkAudioVolume;
            this.breakCharVoice = breakCharVoice;
        }

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }
    }
}
