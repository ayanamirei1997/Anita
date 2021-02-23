namespace Anita
{
    public class EventModel : AVGModel
    {
        private string event0;

        public EventModel(string event0)
        {
            this.event0 = event0;
        }

        public string Event0 { set => event0 = value; get => event0; }
    }
}
