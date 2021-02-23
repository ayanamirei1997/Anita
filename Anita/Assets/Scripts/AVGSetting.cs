namespace Anita
{
    public class AVGSetting : AnitaAVG
    {
        //-------------------------------------------玩家设置
        //文本显示速度
        public static float TEXT_CHANGE_SPEED = 0.2f;

        //背景音乐音量
        public static float BK_AUDIO_VOLUME = 1.0f;

        //是否打断角色语音
        public static bool BREAK_CHARACTER_VOICE = true;

        //-------------------------------------------开发者设置
        //背景音乐过渡速度
        public static float BK_AUDIO_VOLUME_FADESPEED = 0.02f;

        //角色立绘过渡速度
        public static float CH_IMAGE_FADESPEED = 0.02f;

        //角色立绘默认x偏移
        public static float CH_IMAGE_XOFFSET = 0f;

        //角色立绘默认y偏移
        public static float CH_IMAGE_YOFFSET = 20f;

        public static string EXCEL_PATH = UnityEngine.Application.dataPath + "/Excels/";

        public static SettingSave GetPlayerSettingModel()
        {
            return new SettingSave(
                TEXT_CHANGE_SPEED,
                BK_AUDIO_VOLUME,
                BREAK_CHARACTER_VOICE
                );
        }

        public static void SetPlayerSettingModel(SettingSave model)
        {
            TEXT_CHANGE_SPEED = model.textChangeSpeed;
            BK_AUDIO_VOLUME = model.bkAudioVolume;
            BREAK_CHARACTER_VOICE = model.breakCharVoice;
        }
    }
}