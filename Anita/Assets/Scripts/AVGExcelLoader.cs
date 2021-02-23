using Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;

namespace Anita
{
    public class AVGExcelLoader : AVGLoader
    {
        private DataRowCollection LoadExcel(string filePath)
        {
            FileStream fs = null;
            try
            {
                //开启Excel表的文件流
                fs = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                //读取
                IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fs);
                //返回读取到的集合
                DataSet dataSet = excelDataReader.AsDataSet();
                //返回表一
                return dataSet.Tables[0].Rows;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message + "::" + e.StackTrace);
                return null;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }

        //重写Analysis方法为Excel解析
        //AnitaLoader接口Analysis方法参数从TextAsset改为object，后强制转换
        public override bool Analysis(out List<AVGModel> modelList, object excelPath)
        {
            //调用上述方法，获取读取到的DataRowCollection对象
            //DataRowCollection对象可视为二维数组
            DataRowCollection excelData = LoadExcel((string)excelPath);

            modelList = new List<AVGModel>();
            //遍历表格每一row
            for (int i = 1; i < excelData.Count; i++)
            {
                AVGModel model = null;
                //排除注释
                if (excelData[i][0].ToString().Equals("#"))
                {
                    continue;
                }
                //switch每一行第一列
                switch (excelData[i][0].ToString().ToLower())
                {
                    case "c":
                        model = DealwithCommand(excelData[i][1].ToString(), excelData[i][2].ToString(),
                            excelData[i][3].ToString(), excelData[i][4].ToString(),
                            excelData[i][5].ToString(), excelData[i][6].ToString());
                        break;
                    case "t":
                        //2020.11.23更新
                        //获取Column长度，将非空元素加入到延迟列表
                        int delayCount = excelData[i].ItemArray.Length - 4;
                        if (delayCount > 0)
                        {
                            List<string> delays = new List<string>(delayCount);
                            for (int j = 0; j < delayCount; j++)
                            {
                                string s = excelData[i][j + 4].ToString();
                                if (!s.Equals(""))
                                {
                                    delays.Add(s);
                                }
                            }
                            model = DealwithText(excelData[i][1].ToString(), excelData[i][2].ToString(),
                                excelData[i][3].ToString(), delays);
                        }
                        else
                        {
                            model = DealwithText(excelData[i][1].ToString(), excelData[i][2].ToString(),
                                excelData[i][3].ToString(), new List<string>());
                        }
                        break;
                    case "b":
                        //同t，排除掉为空的单元格子
                        //2020.11.23更新
                        int buttonCount = excelData[i].ItemArray.Length - 2;
                        if (buttonCount > 0)
                        {
                            List<Choose> chooseList = new List<Choose>(buttonCount);
                            int chooseIndex = 1;
                            for (int j = 0; j < buttonCount; j++)
                            {
                                string s = excelData[i][j + 2].ToString();
                                if (!s.Equals(""))
                                {
                                    chooseList.Add(new Choose(s, chooseIndex++));
                                }
                            }
                            model = DealwithChooseButton(excelData[i][1].ToString(), chooseList);
                        }
                        else
                        {
                            throw new Exception("Button is empty with excel at row " + i + " at File: " + excelPath);
                        }
                        break;
                    case "a":
                        model = DealwithAnimation(excelData[i][1].ToString());
                        break;
                    case "e":
                        model = DealwithEvent(excelData[i][1].ToString());
                        break;
                    default:
                        throw new Exception("Parse Model error with Excel at row " + i + " at File: " + excelPath);
                }
                if (model == null)
                {
                    throw new Exception("Parse Model error with Excel at row " + i + " at File: " + excelPath);
                }
                AddModel(modelList, model);
            }
            return true;
        }
        //以下包装方法可在父类AVGLoader中书写通用方法，在本类中复用，以节省代码量
        //包装为事件对象
        private AVGModel DealwithEvent(string e)
        {
            return new EventModel(ParamUtil.StringNotNull(e));
        }
        //包装为命令对象
        private AVGModel DealwithCommand(string image, string pos, string xOffset,
            string yOffset, string bkImage, string bkMusic)
        {
            return new CommandModel(
                    ParamUtil.StringNotNull(image),
                    ParamUtil.ParseString2Int(pos, 1),
                    ParamUtil.ParseString2Int(xOffset, 0),
                    ParamUtil.ParseString2Int(yOffset, 0),
                    ParamUtil.StringNotNull(bkImage),
                    ParamUtil.StringNotNull(bkMusic)
                );
        }
        //包装为文本对象
        //2020.11.23更新
        private AVGModel DealwithText(string name, string text,
            string voice, List<string> delays)
        {
            List<TextModel.TextDelay> delayList = new List<TextModel.TextDelay>();

            for (int i = 0; i < delays.Count; i++)
            {
                string[] temp = delays[i].Split('-');
                if (temp.Length != 2)
                {
                    Debug.LogWarning("The format of delay is error: " + text + " :: " + i);
                    continue;
                }
                delayList.Add(new TextModel.TextDelay(
                    ParamUtil.ParseString2Float(temp[1], 0),
                    ParamUtil.ParseString2Int(temp[0], 0)
                    ));
            }

            return new TextModel(
                ParamUtil.StringNotNull(name),
                ParamUtil.StringNotNull(text),
                ParamUtil.StringNotNull(voice),
                delayList);
        }
        //包装为按钮选择对象
        //2020.11.23更新
        //同时修改Choose类，字段Chooses添加set访问器
        private AVGModel DealwithChooseButton(string tag, List<Choose> chooseList)
        {
            ChooseModel res = new ChooseModel(tag);
            res.Chooses = chooseList;
            return res;
        }
        //包装为动画对象
        private AVGModel DealwithAnimation(string type)
        {
            return new AnimationModel(type);
        }
    }
}
