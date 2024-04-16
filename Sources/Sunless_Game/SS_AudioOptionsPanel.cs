using HarmonyLib;
using Sunless.Game.UI.Menus.Options;
using UnityEngine;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(AudioOptionsPanel))]
    public class SS_AudioOptionsPanel
    {
        [HarmonyPostfix]
        [HarmonyPatch(MethodType.Constructor, [typeof(GameObject)])]
        public static void SSPatch_AudioOptionsPanel(GameObject parent)
        {
            //Debug.Log($"[AudioOptionPanel] - {parent.name}");

            SS_Utility.UpdateGameObjectText(parent, "AudioOptionsPanel(Clone)/Title", "Audio settings", "声音设置");
            SS_Utility.UpdateGameObjectText(parent, "AudioOptionsPanel(Clone)/Options/SFXOptions/Label", "SFX Volume:", "音效音量：");
            SS_Utility.UpdateGameObjectText(parent, "AudioOptionsPanel(Clone)/Options/MusicOptions/Label", "Music Volume:", "音乐音量：");
            SS_Utility.UpdateGameObjectText(parent, "AudioOptionsPanel(Clone)/FooterButtons/AcceptButton/Text", "Accept", "确认");
            SS_Utility.UpdateGameObjectText(parent, "AudioOptionsPanel(Clone)/FooterButtons/CancelButton/Text", "Cancel", "取消");
        }
    }
}
