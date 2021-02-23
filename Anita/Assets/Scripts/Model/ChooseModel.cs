using System.Collections.Generic;

namespace Anita
{
    public class ChooseModel : AVGModel
    {
        private List<Choose> chooses; //选择列表
        private string eventTag; //事件名称
        public ChooseModel(string eventTag)
        {
            chooses = new List<Choose>();
            this.eventTag = eventTag;
        }

        public List<Choose> Chooses { get => chooses; set => chooses = value; }
        public string EventTag { get => eventTag; }
    }

    public class Choose
    {
        string text;
        int index;

        public Choose(string text, int index)
        {
            this.text = text;
            this.index = index;
        }

        public string Text { get => text; set => text = value; }
        public int Index { get => index; set => index = value; }
    }
}