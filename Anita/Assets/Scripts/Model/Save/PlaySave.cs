using UnityEngine;

namespace Anita
{
    [System.Serializable]
    public class PlaySave
    {
        public int currentScene;
        public bool useExcel;
        public TextAsset script;
        public string excel;
        public int index;

        public PlaySave(int currentScene, bool useExcel, TextAsset script, string excel, int index)
        {
            this.currentScene = currentScene;
            this.useExcel = useExcel;
            this.script = script;
            this.excel = excel;
            this.index = index;
        }

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }
    }
}
