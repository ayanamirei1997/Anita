using System.Collections.Generic;

namespace Anita
{
    public class TextModel : AVGModel
    {
        private string name; //角色名称
        private string text; //对话文本
        private string voice; //语音文件名
        private List<TextDelay> delayList; //延时列表

        public override string ToString()
        {
            string dl = "";
            foreach (TextDelay d in delayList)
            {
                dl += d.ToString();
            }
            return " TextModel[ name: " + name + "text: " + text + " voice: " + voice + dl + "] ";
        }

        public TextModel(string name = null, string text = null, string voice = null, List<TextDelay> delayList = null)
        {
            this.name = name;
            this.text = text;
            this.voice = voice;
            this.delayList = delayList;
        }

        //使用SubString显示文本
        //delayIndex表示延迟的index，从1开始
        public class TextDelay
        {
            private float delayTime; //延时时长
            private int delayIndex; //延时索引

            public override string ToString()
            {
                return " TextDelay[ delayTime: " + delayTime + " delayIndex: " + delayIndex + "] ";
            }

            public TextDelay(float delayTime, int delayIndex)
            {
                this.delayTime = delayTime;
                this.delayIndex = delayIndex;
            }

            public float DelayTime { get => delayTime; set => delayTime = value; }
            public int DelayIndex { get => delayIndex; set => delayIndex = value; }
        }

        public string Name { get => name; set => name = value; }
        public string Text { get => text; set => text = value; }
        public string Voice { get => voice; set => voice = value; }
        public List<TextDelay> DelayList { get => delayList; set => delayList = value; }
    }
}