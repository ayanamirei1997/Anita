namespace Anita
{
    public class AVGGameManager
    {
        public static AVGGameManager ins = new AVGGameManager();

        private AVGGameManager()
        {
            //CurrentPlaySave = AVGArchive.LoadPlayModel("Slot1");
        }

        public PlaySave CurrentPlaySave { get; set; } = null;
    }
}
