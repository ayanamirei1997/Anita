using System;
using System.Collections.Generic;
using UnityEngine;

namespace Anita
{
    /**
    # 具体参考 demo.txt
    # 文本指令: T/角色名/对话文本/语音/延迟索引:延迟时间/.../ (不定长)
    # 例子: T/Jack/Hello/Jack01.mp3/2:0.5/   (在播放到e时候延迟0.5s)
    # 角色名不填则会继承上一句的名字，但如果使用C命令则会清空所有文本
    # 尽量不要连续延迟,如: T/Ketty/12345//2:0.5/3:0.5/4:0.5/ 可改成 => T/Ketty/12 3 45//2:0.5/4:0.5/6:0.5/
    # 
    # 系统指令: C/角色img/img位置/x偏移/y偏移/背景img/背景audio/
    # 例子: C/p01/0/0/80/bk2/汐.mp3/
    # x,y偏移默认为0，区分正负
    # img位置只能填入0 1 2,  0-靠左，1-居中，2-靠右
    # 只有audio相关需带上文件后缀
    # 
    # 选择按钮指令: B/事件名称/选项1/选项2/.../ (不定长)
    # 例子: B/Event01/Study/Shop/
    # 
    # 动画指令: A/动画类型/
    # 动画类型不区分大小写
    # 自带动画: 'ch_shake'角色晃动 'bk_shake'背景晃动
    # 例子: A/ch_shake/
    # 自定义动画请重写AnitaAnimation接口并挂载在AVG Frame同一物体
    # 
    # 所有指令以/结尾， 若某参数不填需要用//留出
    # 例子: T/Mary/Goodbye!///  语音和延迟均不使用
    # 
    # 图片文件不加后缀，音频文件加后缀
    # 
    # 脚本中的空行需要#占位
     */
    public class AVGLoader : MonoBehaviour, AnitaLoader
    {
        //解析TextAsset返回实体模型列表
        public virtual bool Analysis(out List<AVGModel> modelList, object textAsset)
        {
            try
            { 
                modelList = new List<AVGModel>();
                string[] split = ((TextAsset)textAsset).text.Split('\n');

                int i = 0;
                foreach (string s in split)
                {
                    AVGModel model = null;

                    if (s[0] == '#' || s[0] == '\n' || s[0] == ' ') { i++; continue; } //省略注释

                    string firstChar = s[0].ToString().ToLower();

                    switch (firstChar)
                    {
                        case "c":
                            model = DealwithCommand(s);
                            if (model == null)
                            {
                                throw new Exception("Command analysis error! at index: " + i);
                            }
                            break;
                        case "t":
                            model = DealwithText(s);
                            if (model == null)
                            {
                                throw new Exception("Text analysis error! at index: " + i);
                            }
                            break;
                        case "b":
                            model = DealwithChooseButton(s);
                            if (model == null)
                            {
                                throw new Exception("Choose analysis error! at index: " + i);
                            }
                            break;
                        case "a":
                            model = DealwithAnimation(s);
                            if (model == null)
                            {
                                throw new Exception("Animation analysis error! at index: " + i);
                            }
                            break;
                        case "e":
                            model = DealwithEvent(s);
                            if (model == null)
                            {
                                throw new Exception("Event analysis error! at index: " + i);
                            }
                            break;
                        default:
                            throw new Exception("Model type analysis error! at index: " + i);
                    }

                    //if (firstChar.Equals("c"))
                    //{
                    //    model = DealwithCommand(s);
                    //    if (model == null)
                    //    {
                    //        throw new Exception("Command analysis error! at index: " + i);
                    //    }
                    //}
                    //else if (firstChar.Equals("t"))
                    //{
                    //    model = DealwithText(s);
                    //    if (model == null)
                    //    {
                    //        throw new Exception("Text analysis error! at index: " + i);
                    //    }
                    //}
                    //else if (firstChar.Equals("b"))
                    //{
                    //    model = DealwithChooseButton(s);
                    //    if (model == null)
                    //    {
                    //        throw new Exception("Choose analysis error! at index: " + i);
                    //    }
                    //}
                    //else if (firstChar.Equals("a"))
                    //{
                    //    model = DealwithAnimation(s);
                    //    if (model == null)
                    //    {
                    //        throw new Exception("Animation analysis error! at index: " + i);
                    //    }
                    //}
                    //else if (firstChar.Equals("e"))
                    //{
                    //    model = DealwithEvent(s);
                    //    if (model == null)
                    //    {
                    //        throw new Exception("Event analysis error! at index: " + i);
                    //    }
                    //}
                    //else
                    //{
                    //    throw new Exception("Model type analysis error! at index: " + i);
                    //}

                    i++;
                    AddModel(modelList, model);
                }
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message + " at Analysis() at AVGLoader.cs\n" + e.StackTrace);
                modelList = null;
                return false;
            }
        }

        //事件指令: E/事件名称/
        private AVGModel DealwithEvent(string s)
        {
            try
            {
                return new EventModel(ParamUtil.StringNotNull(s.Split('/')[1]));
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message + " at DealwithEvent() at AVGLoader.cs\n" + e.StackTrace);
                return null;
            }
        }

        /**
         * 系统指令: C/角色img/img位置/x偏移/y偏移/背景img/背景audio/
         * x,y偏移默认为0，区分正负
         * img位置只能填入0 1 2,  0-靠左，1-居中，2-靠右
         * 只有audio相关需带上文件后缀
         */
        private AVGModel DealwithCommand(string s)
        {
            try
            {
                string[] split = s.Split('/');
                return new CommandModel(
                    ParamUtil.StringNotNull(split[1]),
                    ParamUtil.ParseString2Int(split[2], 1),
                    ParamUtil.ParseString2Int(split[3], 0),
                    ParamUtil.ParseString2Int(split[4], 0),
                    ParamUtil.StringNotNull(split[5]),
                    ParamUtil.StringNotNull(split[6])
                );
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message + " at DealwithCommand() at AVGLoader.cs\n" + e.StackTrace);
                return null;
            }
        }

        //文本指令: T/角色名/对话文本/语音/延迟索引:延迟时间/.../ (不定长)
        //exp: T/Jack/Hello/Jack01.mp3/2:0.5/   (在播放到e时候延迟0.5s)
        private AVGModel DealwithText(string s)
        {
            try
            {
                string[] split = s.Split('/');
                List<TextModel.TextDelay> delayList = new List<TextModel.TextDelay>();
                for (int i = 4; i < split.Length - 1; i++)
                {
                    string[] delaySplit = split[i].Split(':');
                    if (delaySplit.Length == 2)
                    {
                        delayList.Add(new TextModel.TextDelay(
                            ParamUtil.ParseString2Float(delaySplit[1], 0),
                            ParamUtil.ParseString2Int(delaySplit[0], 0)));
                    }
                    else
                    {
                        if (delaySplit[0] == "")
                            continue;
                        Debug.LogError("Text Delay Format Error: " + i + ": " + split[i]);
                    }
                }
                return new TextModel(
                    ParamUtil.StringNotNull(split[1]),
                    ParamUtil.StringNotNull(split[2]),
                    ParamUtil.StringNotNull(split[3]),
                    delayList);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message + " at DealwithText at AVGLoader.cs\n" + e.StackTrace);
                return null;
            }
        }

        //选择按钮指令: B/事件名称/选项1/选项2/.../ (不定长)
        //exp: B/DoWhat/Study/Shop/
        private AVGModel DealwithChooseButton(string s)
        {
            try
            {
                string[] split = s.Split('/');
                ChooseModel res = new ChooseModel(ParamUtil.StringNotNull(split[1]));
                for (int i = 2; i < split.Length - 1; i++)
                {
                    res.Chooses.Add(new Choose(ParamUtil.StringNotNull(split[i]), i - 1));
                }
                return res;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message + " at DealwithChooseButton at AVGLoader.cs\n" + e.StackTrace);
                return null;
            }
        }

        /**
         * 动画指令: A/动画类型/
         * 动画类型不区分大小写
         * 自带动画: 'ch_shake'角色晃动 'bk_shake'背景晃动
         * exp: A/ch_shake/
         */
        private AVGModel DealwithAnimation(string s)
        {
            try
            {
                return new AnimationModel(ParamUtil.StringNotNull(s.Split('/')[1]));
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message + " at DealwithAnimation at AVGLoader.cs\n" + e.StackTrace);
                return null;
            }
        }

        //添加模型
        protected void AddModel(List<AVGModel> modelList, AVGModel model)
        {
            modelList.Add(model);
        }

        public virtual Sprite LoadImage(string path)
        {
            return Resources.Load<Sprite>(path);
        }

        public virtual ResourceRequest LoadImageAsync(string path)
        {
            return Resources.LoadAsync<Sprite>(path);
        }

        public virtual AudioClip LoadAudio(string path)
        {
            return Resources.Load<AudioClip>(path);
        }

        public virtual ResourceRequest LoadAudioAsync(string path)
        {
            return Resources.LoadAsync<AudioClip>(path);
        }
    }
}