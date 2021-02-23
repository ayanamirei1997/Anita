namespace Anita
{
    //可自行实现此接口挂在到AVGFrame同物体下以自动注入
    //这些方法会在AVGController执行开始和结束时调用
    public interface AnitaAround : AnitaAVG
    {
        void Before();

        void After();
    }
}