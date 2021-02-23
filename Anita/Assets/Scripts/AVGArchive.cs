using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System;
using System.Text;

namespace Anita
{
    public class AVGArchive
    {
        private static string playDataPath = Application.persistentDataPath + "/Save/SlotList/";
        private static string settingDataPath = Application.persistentDataPath + "/Save/";

        private static void SaveJson(object obj, string path, string fileName)
        {
            StreamWriter sw = null;
            try
            {
                string jsonStr = JsonUtility.ToJson(obj);
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                sw = new StreamWriter(
                    new FileStream(path + "/" + fileName.Split('.')[0] + ".json", FileMode.Create),
                    Encoding.UTF8);
                sw.Write(jsonStr);
                sw.Flush();
            }
            catch (Exception e)
            {
                Debug.LogWarning(e.Message + "::" + e.StackTrace);
            }
            finally
            {
                sw.Close();
            }
        }

        private static T LoadJson<T>(string filePath)
        {
            StreamReader sr = null;
            try
            {
                if (!File.Exists(filePath))
                {
                    Debug.LogWarning("File not exist: " + filePath + " in LoadJson<T>");
                    return default(T);
                }
                sr = new StreamReader(new FileStream(filePath, FileMode.Open), Encoding.UTF8);
                string jsonStr = sr.ReadToEnd();
                return JsonUtility.FromJson<T>(jsonStr);
            }
            catch (Exception e)
            {
                Debug.LogWarning(e.Message + "::" + e.StackTrace);
                return default(T);
            }
            finally
            {
                sr.Close();
            }
        }

        public static void SavePlayModel(PlaySave model, string fileName)
        {
            SaveJson(model, playDataPath, fileName);
        }

        public static PlaySave LoadPlayModel(string fileName)
        {
            return LoadJson<PlaySave>(playDataPath + "/" + fileName.Split('.')[0] + ".json");
        }

        public static List<PlaySave> LoadPlayModelList()
        {
            string[] fileList = Directory.GetFiles(playDataPath + "/", "*.json");
            List<PlaySave> res = new List<PlaySave>();
            foreach (string file in fileList)
            {
                res.Add(LoadJson<PlaySave>(file));
            }
            return res;
        }

        public static void LoadPlayModelandLoadScene(string fileName)
        {
            PlaySave playSave = LoadPlayModel(fileName);
            AVGGameManager.ins.CurrentPlaySave = playSave;
            UnityEngine.SceneManagement.SceneManager.LoadScene(playSave.currentScene);
        }

        public static void SaveSettingModel(SettingSave model)
        {
            SaveJson(model, settingDataPath, "Setting.json");
        }

        public static void LoadSettingModel()
        {
            AVGSetting.SetPlayerSettingModel(LoadJson<SettingSave>(settingDataPath + "/Setting.json"));
        }
    }
}
