using FairyGUI;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Networking;
using System;
using Anita;

using ResourceManager;
using System.IO;

public class GLoaderExtensions : GLoader
{
    
    class CallBackObject
    {
        public bool isDestroy = false;
        //public LoadObjectCompleteHandler callBack;
        internal string path;
    }

    private const int WEB_RES_MAX = 50; // 最多缓存50个头像
    private const int WEB_RES_DESTROY_COUNT = 20; // 一次销毁20个
    private static List<string> s_texUseList = new List<string>();

    public static Dictionary<string, NTexture> texturesDic = new Dictionary<string, NTexture>();
    public static Dictionary<string, int> texturesCount = new Dictionary<string, int>();
    public static Dictionary<int, string> texturesUrl = new Dictionary<int, string>();

    private static bool s_isFreeAvailable = true;

    protected bool disposed = false;
    public string textureName = string.Empty;
    private CallBackObject callBackObject;

    private static List<string> s_removeList = new List<string>();
    // 设置是否打开释放功能
    public static void SetFreeAvailable(bool flag)
    {
        if (s_isFreeAvailable == flag)
        {
            return;
        }
        s_isFreeAvailable = flag;
        if (s_isFreeAvailable)
        {
            // 做一次销毁
            //s_removeList.Clear();
            //var e = texturesDic.GetEnumerator();
            //while (e.MoveNext())
            //{
            //    string url = e.Current.Key;
            //    int count = texturesCount[url];
            //    if (count <= 0)
            //    {
            //        s_removeList.Add(url);
            //    }

            //}
            //for (int i = 0; i < s_removeList.Count; i++)
            //{
            //    string url = s_removeList[i];
            //    NTexture ntex = texturesDic[url];
            //    texturesCount.Remove(url);
            //    texturesDic.Remove(url);
            //    texturesUrl.Remove(ntex.nativeTexture.GetHashCode());
            //    ntex.Dispose();
            //    GotDebug.LogWarning("[ChatWindow] Destroy One Texture");
            //}
            //s_removeList.Clear();
           // DestroyCache();
        }
    }

    protected override void LoadExternal()
    {
//        NTexture texture;
//        if (texturesDic.TryGetValue(this.url, out texture))
//        {
//            setTexture(texture);
//            return;
//        }
//        string pathEx = string.IsNullOrEmpty(textureName) == true ? this.url : this.url + "/" + textureName;
//        var tempCallBackObj = new CallBackObject();
//        tempCallBackObj.path = pathEx;
//        tempCallBackObj.callBack = delegate (UnityEngine.Object o)
//        {
//            if (!tempCallBackObj.isDestroy)
//            {
//                loadTextureComplete((Texture2D)o, DestroyMethod.Unload);
//            }
//        };
//        callBackObject = tempCallBackObj;
//        ResourceManager.ResourceFunc.LoadTextureAsync(null, pathEx, tempCallBackObj.callBack);
    }

    // GLoader扩展
    private string tempUrl;
//    protected override void LoadFromWeb()
//    {
//        DestroyCache(); // 清理缓存
//        CheckIsLoaded(this.url);
//    }
    public void CheckIsLoaded(string url)
    {
//        NTexture texture;
//        if (texturesDic.TryGetValue(url, out texture))
//        {
//            setTexture(texture);
//            return;
//        }
//        if (LoadingTextures.ContainsKey(this.url))
//        {
//            if (!LoadingTextures[url].ContainsKey(this))
//            {
//                LoadingTextures[url].Add(this, this.loadTextureComplete);
//            }
//            return;
//        }
//        else
//        {
//            LoadingTextures.Add(url, new Dictionary<GLoader, LoadTextureCompleteHandler>());
//            LoadingTextures[url].Add(this, this.loadTextureComplete);
//        }
//        ResourceMgr.Instance.StartCoroutine(LoadWebRequest(url));
    }
//    protected override IEnumerator LoadWebRequest(string texUrl)
//    {
//        //GotDebug.LogWarning("[ChatWindow] LoadWebRequest = " + texUrl);
//        //float downloadStartTime = Time.time;
//        string filePath = "";
//        var ta = texUrl.Split('/');
//        var texName = ta[ta.Length-1].Replace(":","");
//        var requestUrl = texUrl;
//#if !UNITY_WEBGL
//#if UNITY_EDITOR
//            filePath = Path.Combine(Path.Combine(Application.dataPath, "../AssetBundles/windows_assetbundle/headIcon"),texName);
//#else
//            filePath = Path.Combine(Path.Combine(Application.dataPath, "../AssetBundles/headIcon"), texName);
//#endif
//            if (File.Exists(filePath)){
//               requestUrl = filePath;
//            }
//#endif
//        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(requestUrl))
//        {
//            DateTime beginTime = DateTime.Now;
//            yield return uwr.SendWebRequest();
//            
//            if (uwr.isNetworkError || uwr.isHttpError)
//            {
//                 GotDebug.Log("LoadWebRequest err url==" + texUrl);
//                onExternalLoadFailed();
//            }
//            else
//            {
//#if !UNITY_WEBGL
//                // PC端首先保存到本地缓存
//                if(requestUrl != filePath){
//                        string savetPath = Path.GetDirectoryName(filePath);
//                        if (!Directory.Exists(savetPath))
//                        {
//                            Directory.CreateDirectory(savetPath);
//                        }
//                        //Debug.Log("Local 找到资源2:" + filePath);
//                        File.WriteAllBytes(filePath, uwr.downloadHandler.data);
//                }
//              
//#endif
//
//                //float downloadSpeed = uwr.downloadedBytes / (Time.time - downloadStartTime);
//                //GotDebug.LogWarning("[ChatWindow] uwr.downloadedBytes = " + uwr.downloadedBytes + " | speed : " + downloadSpeed + "bytes/s");
//                GotDebug.LogFormat("LoadWebRequest Ok time = {0}  url== {1} ",(DateTime.Now - beginTime).TotalMilliseconds,texUrl);
//                Dictionary<GLoader, LoadTextureCompleteHandler> handlers;
//                if (LoadingTextures.TryGetValue(texUrl, out handlers))
//                {
//                    var tempKey = new List<GLoader>(handlers.Keys);
//                    for (int i = 0; i < tempKey.Count; i++)
//                    {
//                        if (texUrl == tempKey[i].url)
//                        {
//                            handlers[tempKey[i]](DownloadHandlerTexture.GetContent(uwr), DestroyMethod.Destroy);
//                        }
//                        RemoveHandle(texUrl, tempKey[i]);
//                    }
//                }
//            }
 //       };
  //  }

//    protected override void RemoveHandle(string url, GLoader loader)
//    {
////        if (LoadingTextures == null || string.IsNullOrEmpty(url))
////        {
////            return;
////        }
////        Dictionary<GLoader, LoadTextureCompleteHandler> handlers;
////        if (LoadingTextures.TryGetValue(url, out handlers))
////        {
////            if (handlers.ContainsKey(loader))
////            {
////                handlers.Remove(loader);
////                if (handlers.Count == 0)
////                {
////                    LoadingTextures.Remove(url);
////                }
////            }
////        }
//    }

    //protected void loadTextureComplete(Texture2D tex, int reqID)
    //{
    //    loadTextureComplete(tex);
    //}

    protected void loadTextureComplete(Texture2D texture, DestroyMethod destroyMethod)
    {
        if (disposed)
        {
            int count;
            if (!texturesCount.TryGetValue(this.url, out count) && texture != null)
            {
                Texture2D.Destroy(texture);
                //Resources.UnloadAsset(texture);
            }
            return;
        }

        if (texture == null)
        {
            onExternalLoadFailed();
            return;
        }
        NTexture nText;
        if (texturesDic.ContainsKey(this.url))
        {
            nText = texturesDic[this.url];
        }
        else
        {
            nText = new NTexture(texture);
            nText.destroyMethod = destroyMethod;// DestroyMethod.Unload;
            texturesCount.Add(this.url, 0);
            texturesDic.Add(this.url, nText);
            texturesUrl.Add(texture.GetHashCode(), this.url);
        }
        setTexture(nText);
        //GotGame.eventManager.TriggerEvent(ClientEvent.webPicLoadedCompleted);
    }

    protected void setTexture(NTexture texture)
    {
        texturesCount[this.url]++;
        onExternalLoadSuccess(texture);
        //GotGame.eventManager.TriggerEvent(ClientEvent.webPicLoadedCompleted);
        SetUsed(this.url);
    }

    public override void Dispose()
    {
        disposed = true;
//        if (isFromWeb())
//        {
//            RemoveHandle(this.url, this);
//        }
        base.Dispose();
    }

    protected override void FreeExternal(NTexture texture)
    {
        if (texture.nativeTexture == null)
        {
            //GotDebug.LogWarning("FreeExternal texture.nativeTexture is nil.");
            return;
        }

        int hasCode = texture.nativeTexture.GetHashCode();
        if (texturesUrl.ContainsKey(hasCode))
        {
            string url = texturesUrl[hasCode];
            int count = --texturesCount[url];
            if (count <= 0 && s_isFreeAvailable)
            {
                // 不删除
                //RemoveTextureInternal(url);
            }
        }
        else if (callBackObject != null)
        {
            callBackObject.isDestroy = true;
            callBackObject = null;
        }
    }

    // 标记使用
    private static void SetUsed(string url)
    {
        if (s_texUseList.Contains(url))
        {
            s_texUseList.Remove(url);
        }
        s_texUseList.Add(url);
    }

    // 销毁缓存
    private static void DestroyCache()
    {
        if (texturesDic.Count > WEB_RES_MAX)
        {
            s_removeList.Clear();
            // 超上限, 销毁一部分
            int destroyCount = 0;
            for (int i = 0; i < s_texUseList.Count; i++)
            {

                string url = s_texUseList[i];
                int count = texturesCount[url];
                if (count <= 0)
                {
                    s_removeList.Add(url);
                    destroyCount++;
                }
                if (destroyCount >= WEB_RES_DESTROY_COUNT)
                {
                    break;
                }
            }
            for (int i = 0; i < s_removeList.Count; i++)
            {
                string url = s_removeList[i];
                RemoveTextureInternal(url);
            }
            s_removeList.Clear();
        }
        
    }

    private static void RemoveTextureInternal(string url)
    {
        if (!texturesDic.ContainsKey(url))
        {
            return;
        }
        NTexture ntex = texturesDic[url];
        texturesCount.Remove(url);
        texturesDic.Remove(url);
        texturesUrl.Remove(ntex.nativeTexture.GetHashCode());
        ntex.Dispose();
        s_texUseList.Remove(url);
        //GotDebug.LogWarning("[ChatWindow] Destroy One Texture");
    }
}
