namespace Anita
{
    public interface AnitaEvent : AnitaAVG
    {
        //If continue to play the next one
        //是否继续播放下一句
        bool ReceiveEvent(string e);
    }
}