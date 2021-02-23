using UnityEngine;
using UnityEngine.SceneManagement;

namespace Anita
{
    public class AVGSceneManager : MonoBehaviour
    {
        [SerializeField] private AVGFrame[] frameList;
        [SerializeField] private AVGFrame currentFrame;

#if UNITY_EDITOR
        [SerializeField] private int currentScene;
        [SerializeField] private bool currentUseExcel;
        [SerializeField] private TextAsset currentScript;
        [SerializeField] private string currentExcel;
        [SerializeField] private int currentIndex;
#endif

        private void Awake()
        {
            foreach (AVGFrame frame in frameList)
            {
                frame.SceneManager = this;
            }
        }

        public void SetCurrentFrame(AVGFrame frame)
        {
            currentFrame = frame;
        }

#if UNITY_EDITOR
        [ContextMenu("Save to Slot1.json")]
        public void TestSave()
        {
            // AVGArchive.SavePlayModel(GetPlaySaveModel(), "Slot1");
            // AVGArchive.SaveSettingModel(AVGSetting.GetPlayerSettingModel());
        }

        [ContextMenu("Load Slot1.json")]
        public void TestLoad()
        {
            AVGArchive.LoadPlayModelandLoadScene("Slot1.json");
        }
#endif

        public PlaySave GetPlaySaveModel()
        {
//             if (currentFrame == null)
//             {
//                 Debug.LogWarning("CurrentFrame is null in AVGSceneManager at " + gameObject.name);
//                 return null;
//             }
// #if UNITY_EDITOR
//             currentScene = SceneManager.GetActiveScene().buildIndex;
//             currentUseExcel = currentFrame.useExcel;
//             currentScript = currentFrame.useExcel ? null : currentFrame.textAsset;
//             currentExcel = currentFrame.useExcel ? currentFrame.excelName : "";
//             currentIndex = currentFrame.CurrentIndex();
// #endif

//             return new PlaySave(
//                     currentScene = SceneManager.GetActiveScene().buildIndex,
//                     currentFrame.useExcel,
//                     currentFrame.useExcel ? null : currentFrame.textAsset,
//                     currentFrame.useExcel ? currentFrame.excelName : "",
//                     currentFrame.CurrentIndex()
//                 );
        
        // todo fix打包报错
        return null;
        }
    }
}
