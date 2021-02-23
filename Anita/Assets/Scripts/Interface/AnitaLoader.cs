using System.Collections.Generic;
using UnityEngine;

namespace Anita
{
    //可自行实现此接口挂在到AVGFrame同物体下以自动注入
    public interface AnitaLoader : AnitaAVG
    {
        //解析脚本文件并返回实体模型AVGModel列表
        bool Analysis(out List<AVGModel> modelList, object textAsset);

        //所有加载默认使用Resources
        //图片加载
        Sprite LoadImage(string path);

        //异步图片加载
        ResourceRequest LoadImageAsync(string path);

        //音频加载
        AudioClip LoadAudio(string path);

        //异步音频加载
        ResourceRequest LoadAudioAsync(string path);
    }
}