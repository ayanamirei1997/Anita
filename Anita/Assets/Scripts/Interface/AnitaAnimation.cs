namespace Anita
{
    //可自行实现此接口挂在到AVGFrame同物体下以自动注入
    public interface AnitaAnimation : AnitaAVG
    {
        void Init();

        void StartAnimation(string type);
    }
}